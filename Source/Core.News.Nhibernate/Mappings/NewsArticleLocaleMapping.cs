﻿using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsArticleLocaleMapping : ClassMap<NewsArticleLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleLocaleMapping"/> class.
        /// </summary>
        public NewsArticleLocaleMapping()
        {
            Cache.Region("NewsArticleLocales").ReadWrite();
            Table("NewsArticleLocales");
            Id(newsArticleLocale => newsArticleLocale.Id);
            References(newsArticleLocale => newsArticleLocale.NewsArticle).Column("NewsArticleId").LazyLoad().Not.Nullable();
            Map(newsArticleLocale => newsArticleLocale.Culture).Length(5);
            Map(newsArticleLocale => newsArticleLocale.Title).Length(255);
            Map(newsArticleLocale => newsArticleLocale.Summary).Length(1024);
            Map(newsArticleLocale => newsArticleLocale.Content);
        }
    }
}
