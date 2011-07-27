using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using Framework.Facilities.NHibernate;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.NHibernate.Services
{
    public class NHibernateProductLocaleService : NHibernateDataService<ProductLocale>, IProductLocaleService
    {
        public NHibernateProductLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public ProductLocale GetLocale(long productId, string culture)
        {
            IQueryable<ProductLocale> query = CreateQuery();
            return query.Where(locale => locale.Product.Id == productId && locale.Culture == culture).FirstOrDefault();
        }
    }
}
