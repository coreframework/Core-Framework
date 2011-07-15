using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageLocaleService : NHibernateDataService<PageLocale>, IPageLocaleService
    {
        public NHibernatePageLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {}

        public PageLocale GetLocale(long pageId, String culture)
        {
            IQueryable<PageLocale> query = CreateQuery();
            return query.Where(locale => locale.Page.Id == pageId && locale.Culture == culture).FirstOrDefault();
        }

        public IList<PageLocale> GetLocales(long pageId)
        {
            IQueryable<PageLocale> query = CreateQuery();
            return query.Where(locale => locale.Page.Id == pageId).ToList();
        }
    }
}