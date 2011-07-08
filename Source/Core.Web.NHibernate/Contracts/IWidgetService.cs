using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<Widget> baseQuery);

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        IQueryable<Widget> GetSearchQuery(string searchString);
    }
}
