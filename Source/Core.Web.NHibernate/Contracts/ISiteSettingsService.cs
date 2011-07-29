using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface ISiteSettingsService : IDataService<SiteSettings>
    {
        SiteSettings GetSettings();
    }
}
