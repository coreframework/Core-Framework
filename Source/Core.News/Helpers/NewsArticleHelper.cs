using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.News.Nhibernate.Contracts;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Helpers
{
    public static class NewsArticleHelper
    {
        /// <summary>
        /// Updates the categories to product assignment.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="selids">The selected ids.</param>
        /// <returns></returns>
        public static bool UpdateCategoriesToProductAssignment(NewsArticle article, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var newsArticleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
            var categoryService = ServiceLocator.Current.GetInstance<INewsCategoryService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = article.Categories.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var category in noselected)
            {
                article.Categories.Remove(category);
            }

            foreach (var selid in selids)
            {
                string selid1 = selid;
                if (!article.Categories.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        article.Categories.Add(categoryService.Find(selectedID));
                    }
                }
            }

            return newsArticleService.Save(article);
        }
    }
}