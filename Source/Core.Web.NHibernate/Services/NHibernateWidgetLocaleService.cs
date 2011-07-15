using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

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
    }
}