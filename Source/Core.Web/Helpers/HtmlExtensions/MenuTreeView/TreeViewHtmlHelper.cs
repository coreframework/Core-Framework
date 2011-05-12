using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Web.Helpers.HtmlExtensions.MenuTreeView
{
    public static class TreeViewHtmlHelper
    {
        public static string RenderTree<T>(
               this HtmlHelper htmlHelper,
               IEnumerable<T> rootLocations,
               String cssClass,
               Func<T, string> locationRenderer)
               where T : IComposite<T>
        {

            return new TreeRenderer<T>(rootLocations, cssClass,locationRenderer).Render();

        }
    }
}