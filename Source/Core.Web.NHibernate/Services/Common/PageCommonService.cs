using System;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace Core.Web.NHibernate.Services.Common
{
    public class PageCommonService : IPageCommonService
    {
        public DetachedCriteria GetAllowedPageWidgetsCriteria(Type instanceType, int operation, ICorePrincipal user, String widgetIdentifier)
        {
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var pageWidgetsQuery = DetachedCriteria.For<PageWidget>("pageWidgets").CreateAlias("Widget", "widget", JoinType.InnerJoin)
                .Add(Restrictions.Eq("widget.Identifier", widgetIdentifier))
                .SetProjection(Projections.Property("pageWidgets.InstanceId"));

            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operation, instanceType,
                                                                                    "pageWidgets.Id",
                                                                                    "pageWidgets.UserId");
            if (permissionCriteria != null)
                pageWidgetsQuery.Add(permissionCriteria);
            return pageWidgetsQuery;
        }
    }
}
