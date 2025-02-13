namespace ShopPromotion.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ItemsTable = new MetroFramework.Controls.MetroGrid();
            listingActionColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            openLinkColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            lastPromotionDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            PromotionStatusColumn = new System.Windows.Forms.DataGridViewImageColumn();
            etsyLinkInfoBindingSource = new System.Windows.Forms.BindingSource(components);
            Button_RunPromotion = new MetroFramework.Controls.MetroButton();
            Button_CheckLocation = new MetroFramework.Controls.MetroButton();
            Button_KeyWordPromotion = new MetroFramework.Controls.MetroButton();
            CurrentIP = new MetroFramework.Controls.MetroLink();
            RumModeLabel = new MetroFramework.Controls.MetroLabel();
            RunModeComboBox = new MetroFramework.Controls.MetroComboBox();
            SiteModeComboBox = new MetroFramework.Controls.MetroComboBox();
            LabelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)ItemsTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)etsyLinkInfoBindingSource).BeginInit();
            SuspendLayout();
            // 
            // ItemsTable
            // 
            ItemsTable.AllowUserToOrderColumns = true;
            ItemsTable.AllowUserToResizeRows = false;
            ItemsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ItemsTable.AutoGenerateColumns = false;
            ItemsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            ItemsTable.BackgroundColor = System.Drawing.Color.White;
            ItemsTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            ItemsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            ItemsTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(17, 17, 17);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            ItemsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            ItemsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ItemsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { listingActionColumn, linkDataGridViewTextBoxColumn, openLinkColumn, lastPromotionDateColumn, noteDataGridViewTextBoxColumn, PromotionStatusColumn });
            ItemsTable.DataSource = etsyLinkInfoBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(17, 17, 17);
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            ItemsTable.DefaultCellStyle = dataGridViewCellStyle5;
            ItemsTable.EnableHeadersVisualStyles = false;
            ItemsTable.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            ItemsTable.GridColor = System.Drawing.Color.White;
            ItemsTable.Location = new System.Drawing.Point(18, 70);
            ItemsTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemsTable.Name = "ItemsTable";
            ItemsTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(17, 17, 17);
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            ItemsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            ItemsTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ItemsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ItemsTable.Size = new System.Drawing.Size(894, 449);
            ItemsTable.TabIndex = 0;
            ItemsTable.CellContentClick += ItemsTable_CellContentClick;
            ItemsTable.RowPostPaint += ItemsTable_RowPostPaint;
            // 
            // listingActionColumn
            // 
            listingActionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            listingActionColumn.DataPropertyName = "ItemAction";
            listingActionColumn.FillWeight = 75F;
            listingActionColumn.HeaderText = "Действие";
            listingActionColumn.Name = "listingActionColumn";
            listingActionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            listingActionColumn.Width = 160;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            linkDataGridViewTextBoxColumn.FillWeight = 54.90196F;
            linkDataGridViewTextBoxColumn.HeaderText = "Ссылка на товар";
            linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            // 
            // openLinkColumn
            // 
            openLinkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            openLinkColumn.FillWeight = 75F;
            openLinkColumn.HeaderText = "Перейти по ссылке";
            openLinkColumn.Name = "openLinkColumn";
            openLinkColumn.Text = "Открыть";
            openLinkColumn.ToolTipText = "Открыть в браузере ссылку на товар";
            openLinkColumn.UseColumnTextForButtonValue = true;
            openLinkColumn.Width = 70;
            // 
            // lastPromotionDateColumn
            // 
            lastPromotionDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            lastPromotionDateColumn.DataPropertyName = "DateLastPromotion";
            lastPromotionDateColumn.FillWeight = 153.1002F;
            lastPromotionDateColumn.HeaderText = "Последнее продвижение";
            lastPromotionDateColumn.Name = "lastPromotionDateColumn";
            lastPromotionDateColumn.ToolTipText = "Дата последнего продвижения товара";
            lastPromotionDateColumn.Width = 125;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            noteDataGridViewTextBoxColumn.FillWeight = 28.35188F;
            noteDataGridViewTextBoxColumn.HeaderText = "Заметка";
            noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            noteDataGridViewTextBoxColumn.ToolTipText = "Любая заметка об этом элементе";
            // 
            // PromotionStatusColumn
            // 
            PromotionStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            PromotionStatusColumn.Description = "Состояние каждого листинга";
            PromotionStatusColumn.HeaderText = "Статус";
            PromotionStatusColumn.Name = "PromotionStatusColumn";
            PromotionStatusColumn.Width = 45;
            // 
            // etsyLinkInfoBindingSource
            // 
            etsyLinkInfoBindingSource.DataSource = typeof(Promotion.Interfaces.ListingInfo);
            // 
            // Button_RunPromotion
            // 
            Button_RunPromotion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            Button_RunPromotion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Button_RunPromotion.Location = new System.Drawing.Point(328, 525);
            Button_RunPromotion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_RunPromotion.Name = "Button_RunPromotion";
            Button_RunPromotion.Size = new System.Drawing.Size(585, 59);
            Button_RunPromotion.TabIndex = 2;
            Button_RunPromotion.Text = "Запустить продвижение";
            Button_RunPromotion.UseSelectable = true;
            Button_RunPromotion.Click += Button_AddItemsToCard_Click;
            // 
            // Button_CheckLocation
            // 
            Button_CheckLocation.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Button_CheckLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Button_CheckLocation.Location = new System.Drawing.Point(18, 594);
            Button_CheckLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_CheckLocation.Name = "Button_CheckLocation";
            Button_CheckLocation.Size = new System.Drawing.Size(298, 46);
            Button_CheckLocation.TabIndex = 1;
            Button_CheckLocation.Text = "Проверить текущий ip и местоположение";
            Button_CheckLocation.UseSelectable = true;
            Button_CheckLocation.Click += Button_CheckLocation_Click;
            // 
            // Button_KeyWordPromotion
            // 
            Button_KeyWordPromotion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            Button_KeyWordPromotion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Button_KeyWordPromotion.BackColor = System.Drawing.Color.White;
            Button_KeyWordPromotion.ForeColor = System.Drawing.SystemColors.ControlText;
            Button_KeyWordPromotion.Location = new System.Drawing.Point(328, 594);
            Button_KeyWordPromotion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeyWordPromotion.Name = "Button_KeyWordPromotion";
            Button_KeyWordPromotion.Size = new System.Drawing.Size(584, 46);
            Button_KeyWordPromotion.TabIndex = 0;
            Button_KeyWordPromotion.Text = "Продвижение по ключевым словам";
            Button_KeyWordPromotion.UseSelectable = true;
            Button_KeyWordPromotion.UseVisualStyleBackColor = false;
            Button_KeyWordPromotion.Click += Button_KeyWordPromotion_Click;
            // 
            // CurrentIP
            // 
            CurrentIP.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            CurrentIP.AutoSize = true;
            CurrentIP.Location = new System.Drawing.Point(618, 37);
            CurrentIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CurrentIP.Name = "CurrentIP";
            CurrentIP.Size = new System.Drawing.Size(294, 27);
            CurrentIP.TabIndex = 5;
            CurrentIP.Text = "Текущий IP:";
            CurrentIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            CurrentIP.UseSelectable = true;
            CurrentIP.Click += CurrentIP_Click;
            // 
            // RumModeLabel
            // 
            RumModeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RumModeLabel.AutoSize = true;
            RumModeLabel.Location = new System.Drawing.Point(70, 525);
            RumModeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            RumModeLabel.Name = "RumModeLabel";
            RumModeLabel.Size = new System.Drawing.Size(189, 19);
            RumModeLabel.TabIndex = 6;
            RumModeLabel.Text = "Режим запуска продвижения";
            // 
            // RunModeComboBox
            // 
            RunModeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            RunModeComboBox.FormattingEnabled = true;
            RunModeComboBox.ItemHeight = 23;
            RunModeComboBox.Location = new System.Drawing.Point(19, 555);
            RunModeComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RunModeComboBox.Name = "RunModeComboBox";
            RunModeComboBox.Size = new System.Drawing.Size(297, 29);
            RunModeComboBox.TabIndex = 7;
            RunModeComboBox.UseSelectable = true;
            // 
            // SiteModeComboBox
            // 
            SiteModeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            SiteModeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            SiteModeComboBox.FormattingEnabled = true;
            SiteModeComboBox.ItemHeight = 23;
            SiteModeComboBox.Location = new System.Drawing.Point(210, 28);
            SiteModeComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SiteModeComboBox.MinimumSize = new System.Drawing.Size(81, 0);
            SiteModeComboBox.Name = "SiteModeComboBox";
            SiteModeComboBox.Size = new System.Drawing.Size(150, 29);
            SiteModeComboBox.TabIndex = 8;
            SiteModeComboBox.UseSelectable = true;
            // 
            // LabelStatus
            // 
            LabelStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LabelStatus.Location = new System.Drawing.Point(18, 646);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new System.Drawing.Size(894, 15);
            LabelStatus.TabIndex = 9;
            LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            ClientSize = new System.Drawing.Size(926, 675);
            Controls.Add(LabelStatus);
            Controls.Add(Button_CheckLocation);
            Controls.Add(Button_KeyWordPromotion);
            Controls.Add(SiteModeComboBox);
            Controls.Add(RunModeComboBox);
            Controls.Add(RumModeLabel);
            Controls.Add(Button_RunPromotion);
            Controls.Add(CurrentIP);
            Controls.Add(ItemsTable);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainForm";
            Padding = new System.Windows.Forms.Padding(23, 69, 23, 23);
            Style = MetroFramework.MetroColorStyle.Green;
            Text = "Продвижение";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)ItemsTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)etsyLinkInfoBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MetroFramework.Controls.MetroGrid ItemsTable;
        private MetroFramework.Controls.MetroButton Button_RunPromotion;
        private MetroFramework.Controls.MetroButton Button_CheckLocation;
        private MetroFramework.Controls.MetroLink CurrentIP;
        private System.Windows.Forms.BindingSource etsyLinkInfoBindingSource;
        private MetroFramework.Controls.MetroButton Button_KeyWordPromotion;
        private System.Windows.Forms.DataGridViewComboBoxColumn listingActionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn openLinkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastPromotionDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn PromotionStatusColumn;
        private MetroFramework.Controls.MetroLabel RumModeLabel;
        private MetroFramework.Controls.MetroComboBox RunModeComboBox;
        private MetroFramework.Controls.MetroComboBox SiteModeComboBox;
        private System.Windows.Forms.Label LabelStatus;
    }
}

