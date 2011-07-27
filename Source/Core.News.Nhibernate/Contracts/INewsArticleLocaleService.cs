using System;
using Core.News.Nhibernate.Models;
using Framework.Core.Services;

namespace Core.News.Nhibernate.Contracts
{
    public interface INewsArticleLocaleService : IDataService<NewsArticleLocale>
    {
        NewsArticleLocale GetLocale(long newsArticleId, String culture);
    }
}
