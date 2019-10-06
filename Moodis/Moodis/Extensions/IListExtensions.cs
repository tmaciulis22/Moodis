using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Extensions
{
    public static class IListExtensions
    {
        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            foreach (T t in list)
                action(t);
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list.Count == 0 || list == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
