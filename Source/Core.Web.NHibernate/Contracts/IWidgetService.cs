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
        /// <summary>
        /// Gets the installed widgets.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Widget> GetInstalledWidgets();

        /// <summary>
        /// Gets the available widgets.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<Widget> GetAvailableWidgets(ICorePrincipal user);

        /// <summary>
        /// Finds the widget by identifier.
        /// </summary>
        /// <param name="widgetIdentifier">The widget identifier.</param>
        /// <returns></returns>
        Widget FindWidgetByIdentifier(String widgetIdentifier);

        /// <summary>
        /// Determines whether [is widget enable] [the specified widget].
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <returns>
        /// 	<c>true</c> if [is widget enable] [the specified widget]; otherwise, <c>false</c>.
        /// </returns>
        bool IsWidgetEnable(Widget widget);

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
