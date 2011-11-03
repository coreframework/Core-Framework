using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateSectionLocaleService : NHibernateDataService<SectionLocale>, ISectionLocaleService
    {
        public NHibernateSectionLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public SectionLocale GetLocale(long sectionId, String culture)
        {
            IQueryable<SectionLocale> query = CreateQuery();
            return query.Where(locale => locale.Section.Id == sectionId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(String searchString)
        {
            ICriteria criteria = Session.CreateCriteria<SectionLocale>().CreateAlias("Section", "Section");

            DetachedCriteria filter = DetachedCriteria.For<SectionLocale>("filteredLocale").CreateAlias("Section", "filteredSection")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredSection.Id", "Section.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
