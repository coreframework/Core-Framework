using System;
using Core.Profiles.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Profiles.NHibernate.Contracts
{
    public interface IProfileTypeLocaleService : IDataService<ProfileTypeLocale>
    {
        ProfileTypeLocale GetLocale(long profileTypeId, String culture);

        ICriteria GetSearchCriteria(String searchString);
    }
}