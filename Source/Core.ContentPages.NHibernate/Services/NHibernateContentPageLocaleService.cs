using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.ContentPages.NHibernate.Services
{
    public class NHibernateContentPageLocaleService : NHibernateDataService<ContentPageLocale>, IContentPageLocaleService
    {
        public NHibernateContentPageLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public ContentPageLocale GetLocale(long contentPageId, String culture)
        {
            IQueryable<ContentPageLocale> query = CreateQuery();
            return query.Where(locale => locale.ContentPage.Id == contentPageId && locale.Culture == culture).FirstOrDefault();
        }
    }
}
