using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IWidgetService : IDataService<Widget>
    {
        IEnumerable<Widget> GetInstalledWidgets();

        IEnumerable<Widget> GetAvailableWidgets(ICorePrincipal user);

        Widget FindWidgetByIdentifier(String widgetIdentifier);
    }
}
