using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPageLocaleService : IDataService<PageLocale>
    {
        PageLocale GetLocale(long pageId, String culture);
        IList<PageLocale> GetLocales(long pageId);
        ICriteria GetSearchCriteria(string searchString);
    }
}