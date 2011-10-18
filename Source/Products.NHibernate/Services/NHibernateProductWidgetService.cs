using Castle.Facilities.NHibernateIntegration;
using Framework.Facilities.NHibernate;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.NHibernate.Services
{
    public class NHibernateProductWidgetService : NHibernateDataService<ProductWidget>, IProductWidgetService
    {
        public NHibernateProductWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }
    }
}