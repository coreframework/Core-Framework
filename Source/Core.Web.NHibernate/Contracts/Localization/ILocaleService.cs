using System;
using NHibernate;

namespace Core.Web.NHibernate.Contracts.Localization
{
    public interface ILocaleService
    {
        ICriteria GetLocaleFilter(Type localeType);
    }
}
