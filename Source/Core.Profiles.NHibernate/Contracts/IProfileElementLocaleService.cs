using System;
using Core.Profiles.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Profiles.NHibernate.Contracts
{
    public interface IProfileElementLocaleService : IDataService<ProfileElementLocale>
    {
        ProfileElementLocale GetLocale(long profileElementId, String culture);

        ICriteria GetSearchCriteria(String searchString);
    }
}