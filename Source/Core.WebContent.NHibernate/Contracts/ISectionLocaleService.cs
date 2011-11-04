using System;
using Core.WebContent.NHibernate.Models;
using Core.Framework.Permissions.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface ISectionLocaleService : IDataService<SectionLocale>
    {
        SectionLocale GetLocale(long sectionId, String culture);

        ICriteria GetSearchCriteria(String searchString, ICorePrincipal user, Int32 operationCode);
    }
}