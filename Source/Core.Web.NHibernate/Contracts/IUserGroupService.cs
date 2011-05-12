using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IUserGroupService : IDataService<UserGroup>
    {
        int GetCount(IQueryable<UserGroup> baseQuery);
        IQueryable<UserGroup> GetSearchQuery(string searchString);
    }
}
