using System;
using System.Linq;
using Core.Forms.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Forms.NHibernate.Contracts
{
    public interface IFormElementService : IDataService<FormElement>
    {

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<FormElement> baseQuery);

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        IQueryable<FormElement> GetSearchQuery(long formId,string searchString);

        /// <summary>
        /// Gets the last order number.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        Int32 GetLastOrderNumber(long? formId);
    }
}
