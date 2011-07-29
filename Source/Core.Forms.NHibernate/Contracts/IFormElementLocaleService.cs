using System;
using Core.Forms.NHibernate.Models;
using Framework.Core.Services;

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
    }
}