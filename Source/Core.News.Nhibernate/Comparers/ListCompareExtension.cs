using System.Collections.Generic;
using System.Linq;

namespace Core.News.Nhibernate.Comparers
{
    public static class ListCompareExtension
    {
        public static IEnumerable<T> Intersection<T>(IEnumerable<T> firstHashset, IEnumerable<T> second)
        {
            foreach (var tmp in second)
            {
                if (firstHashset.Contains(tmp)) { yield return tmp; }
            }
        }
    }
}
