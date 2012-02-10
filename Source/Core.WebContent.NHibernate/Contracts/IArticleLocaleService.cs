using System;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface IArticleLocaleService : IDataService<ArticleLocale>
    {
        ArticleLocale GetLocale(long categoryId, String culture);

        ICriteria GetSearchCriteria(String searchString, ICorePrincipal user, Int32 operationCode);
    }
}