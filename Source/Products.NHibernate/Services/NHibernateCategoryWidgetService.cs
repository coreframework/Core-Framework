using Castle.Facilities.NHibernateIntegration;
using Framework.Facilities.NHibernate;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.NHibernate.Services
{
    class NHibernateCategoryWidgetService : NHibernateDataService<CategoryWidget>, ICategoryWidgetService
    {
        public NHibernateCategoryWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {
        }
    }
}
