using System;
using Core.Profiles.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Profiles.NHibernate.Contracts
{
    public interface IProfileHeaderLocaleService : IDataService<ProfileHeaderLocale>
    {
        ProfileHeaderLocale GetLocale(long profileHeaderId, String culture);
    }
}