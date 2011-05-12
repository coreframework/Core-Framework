using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models.Widgets;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services.Widgets
{
    public class NHibernateSiteMapWidgetService : NHibernateDataService<SiteMapWidget>, ISiteMapWidgetService
    {
        #region Constructors

        public NHibernateSiteMapWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {}

        #endregion
    }
}
