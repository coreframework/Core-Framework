using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IRoleService : IDataService<Role>
    {
        int GetCount(IQueryable<Role> baseQuery);

        IQueryable<Role> GetSearchQuery(string searchString);
    }
}
