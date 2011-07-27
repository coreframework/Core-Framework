using Castle.Facilities.NHibernateIntegration;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.News.Nhibernate.Services
{
    public class NHibernateNewsArticleWidgetService : NHibernateDataService<NewsArticleWidget>, INewsArticleWidgetService
    {
        public NHibernateNewsArticleWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }
    }
}
