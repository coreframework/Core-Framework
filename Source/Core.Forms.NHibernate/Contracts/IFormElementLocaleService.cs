using System;
using Core.Forms.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Forms.NHibernate.Contracts
{
    public interface IFormElementLocaleService : IDataService<FormElementLocale>
    {
        /// <summary>
        /// Gets the locale.
        /// </summary>
        /// <param name="formElementId">The form element id.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        FormElementLocale GetLocale(long formElementId, String culture);

        /// <summary>
        /// Gets the search criteria.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        ICriteria GetSearchCriteria(long formId, string searchString);
    }
}