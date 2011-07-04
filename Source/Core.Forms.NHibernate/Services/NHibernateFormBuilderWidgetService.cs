using System;
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
    public class NHibernateFormBuilderWidgetService: NHibernateDataService<FormBuilderWidget>, IFormBuilderWidgetService
    {
        public NHibernateFormBuilderWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Helper Methods

        public ICriteria GetAllowedFormBuilderWidgets(ICorePrincipal user, int operationCode, String widgetIdentifier)
        {
            ICriteria criteria = Session.CreateCriteria<FormBuilderWidget>("formBuilderWidgets").SetCacheable(true);

            var pageCommonService = ServiceLocator.Current.GetInstance<IPageCommonService>();

            criteria.Add(Subqueries.PropertyIn("formBuilderWidgets.id", pageCommonService.GetAllowedPageWidgetsCriteria(typeof(FormBuilderWidget), operationCode, user, widgetIdentifier)));

            return criteria;
        }

        public int GetCount(ICriteria criteria)
        {
            return criteria.List().Count;
        }

        public ICriteria GetSearchCriteria(ICorePrincipal user, int operation, string searchString, String widgetIdentifier)
        {
            var criteria = GetAllowedFormBuilderWidgets(user, operation, widgetIdentifier);

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("formBuilderWidgets.Title", searchString));
            }

            return criteria;
        }

        public ICriteria GetPagedCriteria(ICriteria criteria, int page, int pageSize, String ordering, bool ascending)
        {
            criteria.SetFirstResult(page * pageSize);
            criteria.SetMaxResults(pageSize);
            criteria.AddOrder(new Order(ordering, ascending));

            return criteria;
        }
      
        #endregion
    }
}
