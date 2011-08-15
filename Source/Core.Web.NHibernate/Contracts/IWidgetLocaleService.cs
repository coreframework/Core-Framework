using System;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Web.NHibernate.Contracts
{
    public interface IWidgetLocaleService : IDataService<WidgetLocale>
    {
        WidgetLocale GetLocale(long widgetId, String culture);

        ICriteria GetSearchCriteria(string searchString);
    }
}