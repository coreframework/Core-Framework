using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Web.Helpers
{
    public static class CommonHelper
    {
        public static void Update<TSource>(this IEnumerable<TSource> outer, Action<TSource> updator)
        {
            foreach (var item in outer)
            {
                updator(item);
            }
        }
    }
}