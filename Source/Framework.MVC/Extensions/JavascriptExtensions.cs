// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JavascriptExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Framework.Mvc.Extensions
{
    /// <summary>
    /// Extensions for working with javascript.
    /// </summary>
    public static class ResourcesScriptManager
    {
        #region Fields

        /// <summary>
        /// Script manager key.
        /// </summary>
        public static readonly String ScriptManagerKey = "JavascriptManager";

        #endregion

        #region Extensions

        /// <summary>
        /// Includes the script path to collection.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="path">The script path.</param>
        /// <returns>Returns empty string.</returns>
        public static String RegisterScript(this HtmlHelper html, String path)
        {
            var scriptStorage = GetJsResourcesStorage(html.ViewContext);

            if (!scriptStorage.Contains(path))
            {
                scriptStorage.Add(path);
            }

            html.ViewContext.HttpContext.Items[ScriptManagerKey] = scriptStorage;

            return String.Empty;
        }

        /// <summary>
        /// Gets the js resources storage.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Returns js resources storage.</returns>
        public static HashSet<String> GetJsResourcesStorage(ViewContext context)
        {
            return context.HttpContext.Items[ScriptManagerKey] as HashSet<String> ??
                                new HashSet<String>();
        }

        /// <summary>
        /// Includes the scripts from storage.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>Returns scripts.</returns>
        public static String IncludeScriptsFromStorage(this HtmlHelper html)
        {
            var sb = new StringBuilder();
            var scriptStorage = GetJsResourcesStorage(html.ViewContext);
            foreach (var script in scriptStorage)
            {
                sb.Append(html.JavascriptInclude(script));
            }
            return sb.ToString();
        }

        #endregion
    }
}
