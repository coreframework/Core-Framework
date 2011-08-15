using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

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

        public ICriteria GetSearchCriteria(string searchString, ICorePrincipal user, int operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<FormLocale>().CreateAlias("Form", "form");

            DetachedCriteria filter = DetachedCriteria.For<FormLocale>("filteredLocale").CreateAlias("Form", "filteredForm")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredForm.Id", "form.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            //apply permissions criteria

            var formsCriteria = DetachedCriteria.For<Form>("forms");
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof(Form),
                                                                                    "forms.Id", "forms.UserId");
            if (permissionCriteria != null)
            {
                formsCriteria.Add(permissionCriteria).SetProjection(Projections.Id());
                criteria.Add(Subqueries.PropertyIn("form.Id", formsCriteria));
            }
         
            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
