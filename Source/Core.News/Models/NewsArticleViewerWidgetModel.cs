using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

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
                    var newsArticleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
                    _newsArticles = (List<NewsArticle>)newsArticleService.GetAll();
                }
                return _newsArticles;
            }
        }

        /// <summary>
        /// Gets or sets the content page.
        /// </summary>
        /// <value>The content page.</value>
        [Required]
        public int ItemsOnPage { get; set; }

        #endregion

        public NewsArticleViewerWidgetModel MapFrom(NewsArticleWidget from)
        {
            Id = from.Id;
            ItemsOnPage = from.ItemsOnPage;
            return this;
        }

        public NewsArticleWidget MapTo(NewsArticleWidget to)
        {
            to.Id = Id;
            to.ItemsOnPage = ItemsOnPage;
            return to;
        }
    }
}