using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateWebContentWidgetCategoryService: NHibernateDataService<WebContentWidgetCategory>, IWebContentWidgetCategoryService
    {
        public NHibernateWebContentWidgetCategoryService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }
    }
}
