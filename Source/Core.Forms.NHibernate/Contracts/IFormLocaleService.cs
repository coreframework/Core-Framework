using System;
using Core.Forms.NHibernate.Models;
using Core.Framework.Permissions.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Forms.NHibernate.Contracts
{
    public interface IFormLocaleService : IDataService<FormLocale>
    {
        FormLocale GetLocale(long formId, String culture);

        ICriteria GetSearchCriteria(string searchString, ICorePrincipal user, Int32 operationCode);
    }
}