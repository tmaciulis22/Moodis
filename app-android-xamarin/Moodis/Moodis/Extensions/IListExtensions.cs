using System.Collections.Generic;

namespace Moodis.Extensions
{
    public static class IListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static void AddNonNull<T>(this IList<T> list, T item)
        {
            if (item != null)
            {
                list.Add(item);
            }
        }
    }
}
