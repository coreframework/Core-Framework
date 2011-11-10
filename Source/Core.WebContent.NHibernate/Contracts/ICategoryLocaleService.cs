using System;
using Core.WebContent.NHibernate.Models;
using Core.Framework.Permissions.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface ICategoryLocaleService : IDataService<WebContentCategoryLocale>
    {
        WebContentCategoryLocale GetLocale(long categoryId, String culture);

        ICriteria GetSearchCriteria(String searchString, ICorePrincipal user, Int32 operationCode);
    }
}