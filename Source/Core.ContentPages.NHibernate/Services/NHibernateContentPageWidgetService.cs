using Castle.Facilities.NHibernateIntegration;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.ContentPages.NHibernate.Services
{
    public class NHibernateContentPageWidgetService : NHibernateDataService<ContentPageWidget>, IContentPageWidgetService
    {
        public NHibernateContentPageWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }
    }
}