using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPageLocaleService : IDataService<PageLocale>
    {
        PageLocale GetLocale(long pageId, String culture);
        IList<PageLocale> GetLocales(long pageId);
    }
}