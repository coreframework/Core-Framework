using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.News.NHibernate.Services
{
    public class NHibernateNewsCategoryLocaleService : NHibernateDataService<NewsCategoryLocale>, INewsCategoryLocaleService
    {
        public NHibernateNewsCategoryLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public NewsCategoryLocale GetLocale(long categoryId, string culture)
        {
            IQueryable<NewsCategoryLocale> query = CreateQuery();
            return query.Where(locale => locale.Category.Id == categoryId && locale.Culture == culture).FirstOrDefault();
        }
    }
}
