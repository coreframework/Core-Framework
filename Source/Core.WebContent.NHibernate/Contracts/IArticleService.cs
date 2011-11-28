using System;
using System.Collections;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface IArticleService : IDataService<Article>
    {
        IEnumerable<Article> GetPublishedArticles(ICorePrincipal user, Int32 operation, ICollection categories);

        ICriteria GetArticlesCriteria(ICollection categories);

        Article FindPublished(ICorePrincipal user, long id);

        Article FindPublished(ICorePrincipal user, String url);
    }
}
