using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Web.Helpers.HtmlExtensions.MenuTreeView
{
    public static class TreeViewHtmlHelper
    {
        public static String RenderTree<T>(
               this HtmlHelper htmlHelper,
               IEnumerable<T> rootLocations,
               String cssClass,
               Func<T, String> locationRenderer)
               where T : IComposite<T>
        {

            return new TreeRenderer<T>(rootLocations, cssClass,locationRenderer).Render();

        }
    }
}