using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Forms.NHibernate.Services
{
    public class NHibernateFormLocaleService : NHibernateDataService<FormLocale>, IFormLocaleService
    {
        public NHibernateFormLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public FormLocale GetLocale(long formId, String culture)
        {
            IQueryable<FormLocale> query = CreateQuery();
            return query.Where(locale => locale.Form.Id == formId && locale.Culture == culture).FirstOrDefault();
        }
    }
}
