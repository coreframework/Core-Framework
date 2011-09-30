using System;
using System.Linq;
using Core.ContentPages.NHibernate.Models;
using Framework.Core.Services;

namespace Core.ContentPages.NHibernate.Contracts
{
    public interface IContentPageService : IDataService<ContentPage>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<ContentPage> baseQuery);

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        IQueryable<ContentPage> GetSearchQuery(String searchString);        
    }
}
