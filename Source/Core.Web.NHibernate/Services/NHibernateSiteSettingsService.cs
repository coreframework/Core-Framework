using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateSiteSettingsService : NHibernateDataService<SiteSettings>, ISiteSettingsService
    {
        #region Constructors

        public NHibernateSiteSettingsService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current site settings.
        /// </summary>
        /// <returns></returns>
        public SiteSettings GetSettings()
        {
            var query = from settings in CreateQuery()
                        select settings;

            var siteSettings = query.FirstOrDefault();

            if (siteSettings == null)
            {
                siteSettings = new SiteSettings { ShowMainMenu = true };
                Save(siteSettings);
            }

            return siteSettings;
        }

        #endregion
    }
}
