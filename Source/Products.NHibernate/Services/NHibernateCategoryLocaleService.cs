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
    public class NHibernateCategoryLocaleService : NHibernateDataService<CategoryLocale>, ICategoryLocaleService
    {
        public NHibernateCategoryLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public CategoryLocale GetLocale(long categoryId, string culture)
        {
            IQueryable<CategoryLocale> query = CreateQuery();
            return query.Where(locale => locale.Category.Id == categoryId && locale.Culture == culture).FirstOrDefault();
        }
    }
}
