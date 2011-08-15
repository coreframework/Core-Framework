using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Forms.NHibernate.Services
{
    public class NHibernateFormElementLocaleService : NHibernateDataService<FormElementLocale>, IFormElementLocaleService
    {
        public NHibernateFormElementLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public FormElementLocale GetLocale(long formElementId, String culture)
        {
            IQueryable<FormElementLocale> query = CreateQuery();
            return query.Where(locale => locale.FormElement.Id == formElementId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(long formId, string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<FormElementLocale>().CreateAlias("FormElement", "formElement");

            DetachedCriteria filter = DetachedCriteria.For<FormElementLocale>("filteredLocale").CreateAlias("FormElement", "filteredFormElement")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredFormElement.Id", "formElement.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter)).Add(Restrictions.Eq("formElement.Form.Id", formId));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
