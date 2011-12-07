using System;
using System.Linq;
using Core.Framework.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Framework.NHibernate.Contracts
{
    public interface IUserGroupService : IDataService<UserGroup>
    {
        int GetCount(IQueryable<UserGroup> baseQuery);
        IQueryable<UserGroup> GetSearchQuery(String searchString);
    }
}
