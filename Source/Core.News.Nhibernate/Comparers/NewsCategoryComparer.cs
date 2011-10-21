using System.Collections.Generic;
using Core.News.Nhibernate.Models;

namespace Core.News.Nhibernate.Comparers
{
    public class NewsCategoryComparer : IEqualityComparer<NewsCategory>
    {
        public bool Equals(NewsCategory x, NewsCategory y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            return false;
            if (ReferenceEquals(x, y)) return true;

            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(NewsCategory category)
        {
            if (ReferenceEquals(category, null)) return 0;

            return category.Id.GetHashCode();
        }
    }
}
