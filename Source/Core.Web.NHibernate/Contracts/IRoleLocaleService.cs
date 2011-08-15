using System;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Web.NHibernate.Contracts
{
    public interface IRoleLocaleService : IDataService<RoleLocale>
    {
        RoleLocale GetLocale(long roleId, String culture);

        ICriteria GetSearchCriteria(string searchString);
    }
}