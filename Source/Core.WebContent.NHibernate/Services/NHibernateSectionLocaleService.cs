using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
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

        public ICriteria GetSearchCriteria(String searchString, ICorePrincipal user, int operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<SectionLocale>().CreateAlias("Section", "section");

            DetachedCriteria filter = DetachedCriteria.For<SectionLocale>("filteredLocale").CreateAlias("Section", "filteredSection")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredSection.Id", "section.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            //apply permissions criteria

            var sectionsCriteria = DetachedCriteria.For<Section>("sections");
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof(Section),
                                                                                    "sections.Id", "sections.UserId");
            if (permissionCriteria != null)
            {
                sectionsCriteria.Add(permissionCriteria).SetProjection(Projections.Id());
                criteria.Add(Subqueries.PropertyIn("section.Id", sectionsCriteria));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
