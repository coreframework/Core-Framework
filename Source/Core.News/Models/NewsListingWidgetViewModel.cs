using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Models
{
    public class NewsListingWidgetViewModel : IMappedModel<NewsListingWidget, NewsListingWidgetViewModel>
    {
        #region Fields

        private INewsArticleService newsArticleService;
        private INewsDetailsWidgetService newsDetailsWidgetService;

        #endregion

        #region Properties

        public long Id { get; set; }

        public IEnumerable<NewsArticle> NewsArticles { get; set; }

        public int TotalItems { get; set; }

        [Required]
        public int ItemsOnPage { get; set; }

        [Required]
        public bool ShowPaginator { get; set; }

        public int CurrentPage { get; set; }

        public String Url { get; set; }

        public NewsDetailsLinkMode LinkMode { get; set; }

        #endregion

        #region Constructors

        public NewsListingWidgetViewModel()
        {
            newsArticleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
            newsDetailsWidgetService = ServiceLocator.Current.GetInstance<INewsDetailsWidgetService>();
        }

        #endregion

        public NewsListingWidgetViewModel MapFrom(NewsListingWidget from)
        {
            Id = from.Id;
            ItemsOnPage = from.ItemsOnPage;
            ShowPaginator = from.ShowPaginator;
            Url = from.Url;
            LinkMode = newsDetailsWidgetService.LinkMode;
            NewsArticles = newsArticleService.GetForListingWidget(from, CurrentPage);
            TotalItems = newsArticleService.GetCountForListingWidget(from);

            return this;
        }

        public NewsListingWidget MapTo(NewsListingWidget to)
        {
            return to;
        }
    }
}