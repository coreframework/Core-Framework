using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Framework.Facilities.NHibernate;

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
    }
}
