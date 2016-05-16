using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shop.Management.App.Extensions
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var coll = new ObservableCollection<T>();
            foreach (var item in collection)
                coll.Add(item);
            return coll;
        }
    }
}