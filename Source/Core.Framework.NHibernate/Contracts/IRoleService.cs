using System;
using System.Linq;
using Core.Framework.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Framework.NHibernate.Contracts
{
    public interface IRoleService : IDataService<Role>
    {
        int GetCount(IQueryable<Role> baseQuery);

        IQueryable<Role> GetSearchQuery(String searchString);
    }
}
