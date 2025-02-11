using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using ShopPromotion.Promotion.Interfaces;
using MetroFramework.Controls;

namespace ShopPromotion.UI
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private ListSortDirection? _sortDirection;
        private PropertyDescriptor _sortProperty;

        protected override bool IsSortedCore => _sortDirection != null;
        protected override bool SupportsSortingCore => true;
        protected override ListSortDirection SortDirectionCore => _sortDirection ?? ListSortDirection.Ascending;
        protected override PropertyDescriptor SortPropertyCore => _sortProperty;

        public SortableBindingList()
        {}

        public SortableBindingList(IList<T> list)
            : base(list)
        {}

        public void Sort(PropertyDescriptor property, ListSortDirection direction) => this.ApplySortCore(property, direction);

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            _sortProperty = property;
            _sortDirection = direction;

            if (!(Items is List<T> items))
                return;

            items.Sort((left, right) =>
            {
                var compareRes = Compare(left == null ? null : _sortProperty.GetValue(left),
                                            right == null ? null : _sortProperty.GetValue(right));
                if (_sortDirection == ListSortDirection.Descending)
                    return -compareRes;
                return compareRes;
            });

            ResetBindings();
        }

        private static int Compare(object lhs, object rhs)
        {
            if (lhs == null)
                return rhs == null ? 0 : -1;
            if (rhs == null)
                return 1;
            if (lhs is IComparable comparable)
                return comparable.CompareTo(rhs);
            return lhs.Equals(rhs) ? 0 : string.Compare(lhs.ToString(), rhs.ToString(), StringComparison.Ordinal);
        }

        protected override void RemoveSortCore()
        {
            _sortDirection = null;
        }
    }
}