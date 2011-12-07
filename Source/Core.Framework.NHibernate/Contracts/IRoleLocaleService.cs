using System;
using Core.Framework.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Framework.NHibernate.Contracts
{
    public interface IRoleLocaleService : IDataService<RoleLocale>
    {
        RoleLocale GetLocale(long roleId, String culture);

        ICriteria GetSearchCriteria(String searchString);
    }
}
