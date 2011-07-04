using System;
using Core.Framework.Permissions.Models;
using NHibernate.Criterion;

namespace Core.Framework.Permissions.Contracts
{
    public interface IPageCommonService
    {
        DetachedCriteria GetAllowedPageWidgetsCriteria(Type instanceType, int operation, ICorePrincipal user, String widgetIdentifier);
    }
}
