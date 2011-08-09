using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace Core.News.Models
{
    public class NewsArticleViewerWidgetModel : IMappedModel<NewsArticleWidget, NewsArticleViewerWidgetModel>
    {
        #region Fields

        private List<NewsArticle> _newsArticles;

        #endregion

        #region Properties

        public long Id { get; set; }


        /// <summary>
        /// Gets the content pages.
        /// </summary>
        /// <value>The content pages.</value>
        public List<NewsArticle> NewsArticles
        {
            get
            {
                if (_newsArticles == null)
                {
                    _newsArticles = new List<NewsArticle>();
                    if (NewsCategories != null)
                    {
                        var newsArticleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
                        var tempArticles = (List<NewsArticle>) newsArticleService.GetAll();
                        foreach (var article in tempArticles)
                        {
                            foreach (var category in NewsCategories)
                            {
                                if(article.Categories.Contains(category) && !_newsArticles.Contains(article))
                                    _newsArticles.Add(article);
                            }
                        }
                    }
                }
                return _newsArticles;
            }
        }

        public List<NewsCategory> NewsCategories { get; set; }

        /// <summary>
        /// Gets or sets the content page.
        /// </summary>
        /// <value>The content page.</value>
        [Required]
        public int ItemsOnPage { get; set; }

        [Required]
        public bool ShowPaginator { get; set; }

        public int TotalItemsCount
        {
            get
            {
                return NewsArticles.Count;
            }
        }

        public int CurrentPage { get; set; }

        #endregion

        public NewsArticleViewerWidgetModel MapFrom(NewsArticleWidget from)
        {
            Id = from.Id;
            ItemsOnPage = from.ItemsOnPage;
            ShowPaginator = from.ShowPaginator;
            NewsCategories = from.Categories.ToList();
            return this;
        }

        public NewsArticleWidget MapTo(NewsArticleWidget to)
        {
            to.Id = Id;
            to.ItemsOnPage = ItemsOnPage;
            to.ShowPaginator = ShowPaginator;
            to.Categories = NewsCategories;
            return to;
        }
    }
}