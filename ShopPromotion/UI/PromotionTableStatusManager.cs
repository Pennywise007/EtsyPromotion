using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ShopPromotion.Promotion.Interfaces;
using MetroFramework.Controls;

namespace ShopPromotion.UI
{
    public class PromotionTableStatusManager<TListingInfoType>
        where TListingInfoType : class
    {
        private bool _firstDataBinding = true;
        private readonly MetroGrid _itemsTable;
        private readonly DataGridViewComboBoxColumn _actionColumn;
        private readonly DataGridViewImageColumn _statusColumn;

        private int ActionColumnInd => _itemsTable.Columns.IndexOf(_actionColumn);
        private int StatusColumnInd => _itemsTable.Columns.IndexOf(_statusColumn);

        private enum Status
        {
            eNone = 0,
            eWarning,
            eSkip,
            eDone,
            eRunning
        }
        private readonly Dictionary<Status, Bitmap> _iconsByStatus = new Dictionary<Status, Bitmap>();

        public PromotionTableStatusManager(IPromotionWorker<TListingInfoType> promotionWorker, MetroGrid itemsTable,
                                           DataGridViewComboBoxColumn actionColumn, DataGridViewImageColumn statusColumn)
        {
            promotionWorker.WhenStart += OnStartPromotion;
            promotionWorker.WhenFinish += OnFinishPromotion;
            promotionWorker.WhenFinishListingPromotion += OnFinishListingPromotion;
            promotionWorker.WhenErrorDuringListingPromotion += OnErrorDuringListingPromotion;

            _itemsTable = itemsTable;
            _actionColumn = actionColumn;
            _statusColumn = statusColumn;

            _iconsByStatus.Add(Status.eNone, null);
            _iconsByStatus.Add(Status.eDone, Properties.Resources.done);
            _iconsByStatus.Add(Status.eSkip, Properties.Resources.skip);
            _iconsByStatus.Add(Status.eWarning, Properties.Resources.warning);
            _iconsByStatus.Add(Status.eRunning, Properties.Resources.running);

            // disabling default cross picture
            _statusColumn.DefaultCellStyle.NullValue = null;
            // equivalent DefaultValuesNeeded, init default status cell value
            InstallStatusCellInfo(_statusColumn.CellTemplate, Status.eNone);

            _itemsTable.CellValueChanged += CellValueChanged;
            _itemsTable.DataBindingComplete += DataBindingComplete;
            _itemsTable.DefaultValuesNeeded += DefaultValuesNeeded;

            InitializeStatuses(false);
        }

        private void OnStartPromotion(object sender, EventArgs e)
        {
            _itemsTable.Invoke(new MethodInvoker(() =>
            {
                InitializeStatuses(true);
            }));
        }

        private void OnFinishPromotion(object sender, string error)
        {
            _itemsTable.Invoke(new MethodInvoker(() =>
            {
                // resetting all running statuses, promotion can be interrupted
                var actionColumnIndex = ActionColumnInd;
                var statusColumnIndex = StatusColumnInd;
                for (int elementIndex = 0, count = _itemsTable.RowCount; elementIndex < count; ++elementIndex)
                {
                    var statusCell = GetStatusCell(elementIndex, statusColumnIndex);
                    if (statusCell.Value == _iconsByStatus[Status.eRunning])
                    {
                        InstallStatusCellInfo(statusCell, InitializeStatusByAction(GetActionFromCell(elementIndex, actionColumnIndex), false));
                    }
                }
            }));
        }

        /// <summary> Handler an error during the listing promotion </summary>
        private void OnErrorDuringListingPromotion(object sender, ErrorDuringListingPromotion errorInfo)
        {
            _itemsTable.Invoke(new MethodInvoker(() =>
            {
                InstallStatusCellInfo(errorInfo.ElementIndex, Status.eWarning, errorInfo.ErrorMessage);
                _itemsTable.Refresh();
            }));
        }

        /// <summary> Handler of finishing promotion an listing </summary>
        private void OnFinishListingPromotion(object sender, PromotionDone promotionDone)
        {
            _itemsTable.Invoke(new MethodInvoker(() =>
            {
                InstallStatusCellInfo(promotionDone.ElementIndex, Status.eDone, promotionDone.Date);
                _itemsTable.Refresh();
            }));
        }

        private void DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // Init status for new columns
            var status = InitializeStatusByAction(GetActionFromCell(e.Row.Index), false);
            InstallStatusCellInfo(e.Row.Cells[StatusColumnInd], status);
        }

        /// <summary>
        /// We need this function to react on first completed data binding because we need to init statuses on start
        /// We can not init them in constructor because they frayed by the _statusColumn.DefaultCellStyle.NullValue
        /// </summary>
        private void DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!_firstDataBinding)
                return;

            _firstDataBinding = false;
            InitializeStatuses(false);
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _itemsTable.RowCount)
            {
                Debug.Assert(false, $"Strange row index {e.RowIndex}");
                return;
            }

            if (e.ColumnIndex < 0 || e.ColumnIndex >= _itemsTable.Columns.Count)
            {
                Debug.Assert(false, $"Strange column index {e.ColumnIndex}");
                return;
            }

            if (e.ColumnIndex == ActionColumnInd)
                InstallStatusCellInfo(e.RowIndex, InitializeStatusByAction(GetActionFromCell(e.RowIndex), false));
        }

        private void InitializeStatuses(bool onStartPromotion)
        {
            var actionColumnIndex = ActionColumnInd;
            var statusColumnIndex = StatusColumnInd;
            for (int elementIndex = 0, count = _itemsTable.RowCount; elementIndex < count; ++elementIndex)
            {
                var status = InitializeStatusByAction(GetActionFromCell(elementIndex, actionColumnIndex), onStartPromotion);
                InstallStatusCellInfo(GetStatusCell(elementIndex, statusColumnIndex), status);
            }
            _itemsTable.Refresh();

            Console.Error.WriteLine("InitializeStatuses ended");
        }

        private ListingInfo.ListingAction? GetActionFromCell(int index, int? actionsColumnIndex = null)
        {
            var row = _itemsTable.Rows[index];
            var actionCell = row.Cells[actionsColumnIndex ?? ActionColumnInd];
            return (ListingInfo.ListingAction?) actionCell.Value;
        }

        private DataGridViewCell GetStatusCell(int index, int? statusColumnIndex = null)
        {
            var row = _itemsTable.Rows[index];
            return row.Cells[statusColumnIndex ?? StatusColumnInd];
        }

        private void InstallStatusCellInfo(DataGridViewCell cell, Status status, string warningStatusText = null)
        {
            if (status == Status.eWarning && cell.Value == _iconsByStatus[Status.eWarning] && !string.IsNullOrEmpty(cell.ToolTipText))
            {
                // add extra error text to current
                cell.ToolTipText += "\n\n" + warningStatusText;
                return;
            }

            cell.Value = _iconsByStatus[status];
            cell.ToolTipText = GetTooltipTextByStatus(status, warningStatusText);
        }

        private void InstallStatusCellInfo(int elementIndex, Status status, string warningStatusText = null)
        {
            InstallStatusCellInfo(GetStatusCell(elementIndex), status, warningStatusText);
        }

        private Status InitializeStatusByAction(ListingInfo.ListingAction? action, bool onStartPromotion)
        {
            if (action == null)
                return Status.eNone;

            switch (action)
            {
                case ListingInfo.ListingAction.Skip:
                    return Status.eSkip;
                case ListingInfo.ListingAction.AddToCard:
                case ListingInfo.ListingAction.Preview:
                case ListingInfo.ListingAction.SearchOnly:
                    return onStartPromotion ? Status.eRunning : Status.eNone;
                default:
                    Trace.Assert(false, $"SetupStatus, не известное действие у листинга {action.ToString()}");
                    return Status.eNone;
            }
        }

        /// <summary>
        /// Generate tooltip text from status
        /// </summary>
        /// <param name="status">Element status</param>
        /// <param name="statusHelpText">for eWarning contains an error message, for eDone contains date, null otherwise</param>
        /// <returns></returns>
        private string GetTooltipTextByStatus(Status status, string statusHelpText)
        {
            switch (status)
            {
                case Status.eNone:
                    Debug.Assert(string.IsNullOrEmpty(statusHelpText), $"GetTooltipTextByStatus, есть сообщение для статуса {status.ToString()}!");
                    return "Запустите продвижение для изменения статуса";
                case Status.eWarning:
                    Trace.Assert(!string.IsNullOrEmpty(statusHelpText), $"GetTooltipTextByStatus, устанавливаем статус {status.ToString()} без сообщения!");
                    return statusHelpText;
                case Status.eRunning:
                    Debug.Assert(string.IsNullOrEmpty(statusHelpText), $"GetTooltipTextByStatus, есть сообщение для статуса {status.ToString()}!");
                    return "Выполняется продвижение, подождите...";
                case Status.eSkip:
                    Debug.Assert(string.IsNullOrEmpty(statusHelpText), $"GetTooltipTextByStatus, есть сообщение для статуса {status.ToString()}!");
                    return "Листинг пропускается при продвижении";
                case Status.eDone:
                    return $"Продвижение листинга выполнено {statusHelpText}";
                default:
                    Trace.Assert(false, $"GetTooltipTextByStatus, не известный статус у листинга {status.ToString()}");
                    return "Не известный статус, обратитесь к администратору";
            }
        }
    }
}