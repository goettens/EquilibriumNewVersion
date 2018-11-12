using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace EquilibriumApp.Classes
{
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {

        public void AddRange(IEnumerable<T> range)
        {
            if (range == null)
                throw new ArgumentNullException("range");

            var items = range.ToList();
            int index = Items.Count;
            foreach (T item in items)
                Items.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, index));
        }
    }
}
