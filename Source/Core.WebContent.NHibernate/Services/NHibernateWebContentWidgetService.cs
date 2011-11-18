using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateWebContentWidgetService: NHibernateDataService<WebContentWidget>, IWebContentWidgetService
    {
        public NHibernateWebContentWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }
    }
}
