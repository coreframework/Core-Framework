using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models.Widgets;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services.Widgets
{
    public class NHibernateListMenuWidgetService : NHibernateDataService<ListMenuWidget>, IListMenuWidgetService
    {
        #region Constructors

        public NHibernateListMenuWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {}

        #endregion
    }
}
