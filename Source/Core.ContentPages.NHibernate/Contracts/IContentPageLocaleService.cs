using System;
using Core.ContentPages.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.ContentPages.NHibernate.Contracts
{
    public interface IContentPageLocaleService : IDataService<ContentPageLocale>
    {
        ContentPageLocale GetLocale(long contentPageId, String culture);

        ICriteria GetSearchCriteria(String searchString);
    }
}