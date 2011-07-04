using System;
using Core.Forms.NHibernate.Models;
using Core.Framework.Permissions.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Forms.NHibernate.Contracts
{
    public interface IFormBuilderWidgetService : IDataService<FormBuilderWidget>
    {
        ICriteria GetAllowedFormBuilderWidgets(ICorePrincipal user, int operationCode, String widgetIdentifier);

        int GetCount(ICriteria criteria);

        ICriteria GetSearchCriteria(ICorePrincipal user, int operation, string searchString, String widgetIdentifier);

        ICriteria GetPagedCriteria(ICriteria criteria, int page, int pageSize, String ordering, bool ascending);
    }
}
