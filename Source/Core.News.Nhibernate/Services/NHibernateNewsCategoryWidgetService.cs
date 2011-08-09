using Castle.Facilities.NHibernateIntegration;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.News.NHibernate.Services
{
    class NHibernateNewsCategoryWidgetService : NHibernateDataService<NewsCategoryWidget>, INewsCategoryWidgetService
    {
        public NHibernateNewsCategoryWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
