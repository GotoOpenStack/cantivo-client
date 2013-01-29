using System;
using System.Collections.Generic;

namespace Adell.Convenience.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Run<T>(this IEnumerable<T> enumerable, Action<T> func)
        {
            foreach (T item in enumerable)
            {
                func(item);
            }
        }
    }
}
