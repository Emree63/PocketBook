using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
	public static class Extensions
	{
        public static IEnumerable<IGrouping<TKey, TElement>> MyGroupBy<TKey, TElement>(
            this ObservableCollection<TElement> source, Func<TElement, TKey> keySelector)
            => source.GroupBy(keySelector);



        public static IEnumerable<T> MySort<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> keySelector,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "asc":
                    return source.OrderBy(keySelector);
                case "desc":
                    return source.OrderByDescending(keySelector);
                default:
                    return source;
            }
        }
    }
}

