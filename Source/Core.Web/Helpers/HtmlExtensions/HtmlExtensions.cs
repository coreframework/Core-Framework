using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate.Contracts;
using Framework.Core;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Helpers.HtmlExtensions
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality.
    /// </summary>
    public static class HtmlExtensions
    {
        #region Fields

        private const String OuterPluginTypeName = "outer";

        #endregion

        #region Extensions

        /// <summary>
        /// Sites the title.
        /// </summary>
        /// <param name="helper">The html helper.</param>
        /// <returns>Site title.</returns>
        public static String SiteTitle(this HtmlHelper helper)
        {
            var settingsService = ServiceLocator.Current.GetInstance<ISiteSettingsService>();

            var settings = settingsService.GetSettings();

            if (settings!=null && !String.IsNullOrEmpty(settings.WebsiteName))
            {
                return String.Format(" - {0}",settings.WebsiteName);
            }
            return String.Empty;
        }

        public static String OperationCheckbox(this HtmlHelper helper, String name, long roleId, Int32 operationKey, IEnumerable<IPermissionModel> objectPermissions)
        {
            using (var stringWriter = new StringWriter())
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, name);
                writer.AddAttribute(HtmlTextWriterAttribute.Value, String.Format("{0}_{1}",roleId,operationKey));
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");

                foreach (var objectPermission in objectPermissions)
                {
                    if ((objectPermission.Permissions & operationKey)==operationKey)
                    {
                         writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");

                        if (objectPermission.EntityId==null)
                            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                    }  
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();

                return stringWriter.ToString();
            }
        }

        public static String PluginsScripts(this HtmlHelper html, IEnumerable<ICorePlugin> plugins)
        {
            var sb = new StringBuilder();

            var externalScripts = new List<String>();

            foreach (var plugin in plugins)
            {
                //get plugin inner scripts package path
                var path = AssetsHelper.GetPluginInnerJsVirtualPath(plugin, ApplicationUtility.Path, html.ViewContext.HttpContext.Request.PhysicalApplicationPath);

                if (!String.IsNullOrEmpty(path))
                    sb.Append(AssetsExtensions.JavascriptHelper(html.ViewContext.HttpContext, path));

                if (!String.IsNullOrEmpty(plugin.CssJsConfigPath) && !String.IsNullOrEmpty(plugin.JsPack))
                {
                    externalScripts.AddRange(AssetsHelper.GetPluginJsPackFiles(plugin.JsPack, Path.Combine(plugin.PluginDirectory, plugin.CssJsConfigPath),
                                                                               OuterPluginTypeName));
                  
                }
            }

            externalScripts.Distinct();

            //add external scripts
            foreach (var item in externalScripts)
            {
                sb.Append(html.JavascriptInclude(item));
            }

            return sb.ToString();
        }

        #endregion
    }
}