using Castle.Facilities.NHibernateIntegration;
using Core.ContentPages.Contracts;
using Core.ContentPages.Models;
using Framework.Facilities.NHibernate;

namespace Core.ContentPages.Services
{
    public class NHibernateContentPageService: NHibernateDataService<ContentPage>, IContentPageService
    {
        public NHibernateContentPageService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }
    }
}
