using System;
using System.Collections;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Models;
using Framework.Core.Services;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface IArticleService : IDataService<Article>
    {
        IEnumerable<Article> GetPublishedArticles(ICorePrincipal user, Int32 operation, ICollection categories);
    }
}
