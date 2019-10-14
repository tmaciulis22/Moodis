using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Extensions
{
    public static class IListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if(list == null || list.Count == 0)
            {
                return true;
            }
            return false;
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
