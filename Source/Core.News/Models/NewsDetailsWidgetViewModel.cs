using System;
using Core.News.Helpers;
using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;

namespace Core.News.Models
{
    public class NewsDetailsWidgetViewModel : IMappedModel<NewsArticle, NewsDetailsWidgetViewModel>
    {
        public String Title { get; set; }

        public String Summary { get; set; }

        public String Content { get; set; }

        public String LastModifiedDate { get; set; }

        public NewsDetailsWidgetViewModel MapFrom(NewsArticle from)
        {
            Title = from.Title;
            Summary = from.Summary;
            Content = from.Content;
            LastModifiedDate = from.LastModifiedDate.ToString(NewsConstants.DateFormat);

            return this;
        }

        public NewsArticle MapTo(NewsArticle to)
        {
            return to;
        }
    }
}