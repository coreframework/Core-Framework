using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateWidgetLocaleService : NHibernateDataService<WidgetLocale>, IWidgetLocaleService
    {
        public NHibernateWidgetLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {

        }

        public WidgetLocale GetLocale(long widgetId, String culture)
        {
            IQueryable<WidgetLocale> query = CreateQuery();
            return query.Where(locale => locale.Widget.Id == widgetId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<WidgetLocale>().CreateAlias("Widget", "widget");

            DetachedCriteria filter = DetachedCriteria.For<WidgetLocale>("filteredLocale").CreateAlias("Widget", "filteredWidget")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredWidget.Id", "widget.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Or(Restrictions.Like("Title", searchString, MatchMode.Anywhere), Restrictions.Like("Description", searchString, MatchMode.Anywhere)));
            }

            return criteria.SetCacheable(true);
        }
    }
}