using System.Linq;
using Core.WebContent.NHibernate.Models;
using Framework.Core.Services;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface ICategoryService : IDataService<WebContentCategory>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<WebContentCategory> baseQuery);
    }
}
