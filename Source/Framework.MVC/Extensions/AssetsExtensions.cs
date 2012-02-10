// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssetsExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Framework.Plugins.Web;
using Framework.Core;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using Environment = Framework.Core.Configuration.Environment;

namespace Framework.Mvc.Extensions
{
    /// <summary>
    /// Adds methods for resource managing to <see cref="HtmlHelper"/>.
    /// </summary>
    public static class AssetsExtensions
    {
        #region Fields

        /// <summary>
        /// Virtual path for directory with css files (~/Content/Css/).
        /// </summary>
        public static readonly String CssPath = "~/Content/Css/";

        /// <summary>
        /// Virtual path for directory with javascript files (~/Scripts/).
        /// </summary>
        public static readonly String JavascriptPath = "~/Scripts/";

        /// <summary>
        /// Path for asset packager config.
        /// </summary>
        public static readonly String AssetPackagesConfigPath = "~/Config/asset_packages.yml";

        /// <summary>
        /// Virtual path for directory with css files (~/Content/Css/).
        /// </summary>
        public static readonly String PluginCssPath = "~/Content/Css/Plugin/";

        private const String TimeSpampFormat = "yyyyMMddhhmmss";

        private const String CssExtension = ".css";

        #endregion

        #region Extension methods

        /// <summary>
        /// Generates HTML markup for including specified css-file.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="css">The css file path relative to <see cref="CssPath"/>.</param>
        /// <returns>Css include tag.</returns>
        public static MvcHtmlString CssInclude(this HtmlHelper html, String css)
        {
            return MvcHtmlString.Create(CssHelper(html.ViewContext.HttpContext, css, null));
        }

        /// <summary>
        /// Generates HTML markup for including specified css-file.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="css">The css file path relative to <see cref="CssPath"/>.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Css include tag.</returns>
        public static MvcHtmlString CssInclude(this HtmlHelper html, String css, Object htmlAttributes)
        {
            return MvcHtmlString.Create(CssHelper(html.ViewContext.HttpContext, css, new RouteValueDictionary(htmlAttributes)));
        }

        /// <summary>
        /// Generates HTML markup for including css-files specified in assets config (<see cref="AssetPackagesConfigPath"/>).
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="packageName">Name of the package specified in assets config.</param>
        /// <returns>HTML markup for including css-file specified in assets config.</returns>
        public static MvcHtmlString CssPackInclude(this HtmlHelper html, String packageName)
        {
            return MvcHtmlString.Create(CssPackHelper(html.ViewContext.HttpContext, packageName, null));
        }

        /// <summary>
        /// Generates HTML markup for including css-files specified in assets config (<see cref="AssetPackagesConfigPath"/>).
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="packageName">Name of the package specified in assets config.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup for including css-file specified in assets config.</returns>
        public static MvcHtmlString CssPackInclude(this HtmlHelper html, String packageName, Object htmlAttributes)
        {
            return MvcHtmlString.Create(CssPackHelper(html.ViewContext.HttpContext, packageName, new RouteValueDictionary(htmlAttributes)));
        }

        /// <summary>
        /// Generates HTML markup for including specified javascript-file.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="javascript">The javascript file path relative to <see cref="JavascriptPath"/>.</param>
        /// <returns>Javascript include tag.</returns>
        public static MvcHtmlString JavascriptInclude(this HtmlHelper html, String javascript)
        {
            return MvcHtmlString.Create(JavascriptHelper(html.ViewContext.HttpContext, javascript, null));
        }

        /// <summary>
        /// Generates HTML markup for including specified javascript-file.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="javascript">The javascript file path relative to <see cref="JavascriptPath"/>.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Javascript include tag.</returns>
        public static MvcHtmlString JavascriptInclude(this HtmlHelper html, String javascript, Object htmlAttributes)
        {
            return MvcHtmlString.Create(JavascriptHelper(html.ViewContext.HttpContext, javascript, new RouteValueDictionary(htmlAttributes)));
        }

        /// <summary>
        /// Generates HTML markup for including javascript-files specified in assets config (<see cref="AssetPackagesConfigPath"/>).
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="packageName">Name of the package specified in assets config.</param>
        /// <returns>HTML markup for including javascript-file specified in assets config.</returns>
        public static MvcHtmlString JavascriptPackInclude(this HtmlHelper html, String packageName)
        {
            return MvcHtmlString.Create(JavascriptPackHelper(html.ViewContext.HttpContext, packageName, null));
        }

        /// <summary>
        /// Generates HTML markup for including javascript-files specified in assets config (<see cref="AssetPackagesConfigPath"/>).
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="packageName">Name of the package specified in assets config.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup for including javascript-file specified in assets config.</returns>
        public static MvcHtmlString JavascriptPackInclude(this HtmlHelper html, String packageName, Object htmlAttributes)
        {
            return MvcHtmlString.Create(JavascriptPackHelper(html.ViewContext.HttpContext, packageName, new RouteValueDictionary(htmlAttributes)));
        }

        /// <summary>
        /// CSSs the widget pack helper.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup for including css-file specified in plugins.</returns>
        public static String CssPluginPackHelper(HttpContext context, IEnumerable<ICorePlugin> plugins, String fileName, RouteValueDictionary htmlAttributes)
        {
            if (String.IsNullOrEmpty(fileName) || plugins == null || !plugins.Any())
            {
                return String.Empty;
            }
            var cssServerPath = context.Server.MapPath(PluginCssPath);
            var file = Math.Abs(fileName.GetHashCode()) + CssExtension;
            return AssetsHelper.BuildPluginsCssPack(plugins, context.Request.PhysicalApplicationPath, cssServerPath, file);
        }

        /// <summary>
        /// CSSs the widget pack helper.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup for including css-file specified in plugins.</returns>
        public static String CssPluginPackHelper(HttpContextBase context, IEnumerable<ICorePlugin> plugins, String fileName, RouteValueDictionary htmlAttributes)
        {
            if (String.IsNullOrEmpty(fileName) || plugins == null || !plugins.Any())
            {
                return String.Empty;
            }

            var cssServerPath = context.Server.MapPath(PluginCssPath);
            var file = Math.Abs(fileName.GetHashCode()) + CssExtension;
            var filePath = AssetsHelper.BuildPluginsCssPack(plugins, context.Request.PhysicalApplicationPath, cssServerPath, file);
            return filePath;
        }

        /// <summary>
        /// CSSs the helper.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="path">The Css href path.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Full css link.</returns>
        public static String CssHelper(HttpContext context, String path, RouteValueDictionary htmlAttributes)
        {
            var builder = new TagBuilder("link");
            builder.Attributes["href"] = path;
            builder.Attributes["rel"] = "stylesheet";
            builder.Attributes["type"] = "text/css";
            builder.Attributes["media"] = "screen, projection";
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(htmlAttributes, true);
            }
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        /// <summary>
        /// Generates javascript link.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="path">The javascript file path.</param>
        /// <returns>Full javascript link.</returns>
        public static String JavascriptHelper(HttpContextBase context, String path)
        {
            var builder = new TagBuilder("script");
            if (Environment.Development.Equals(GetEnvironment()))
            {
                path = String.Format("{0}?{1}", path, GetTimeStamp(context, path));
            }
            builder.Attributes["src"] = path;
            builder.Attributes["type"] = "text/javascript";
            return builder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region Helper members

        private static String CssHelper(HttpContextBase context, String css, RouteValueDictionary htmlAttributes)
        {
            var builder = new TagBuilder("link");
            var path = ToAbsolutePath(css, CssPath);
            if (Environment.Development.Equals(GetEnvironment()))
            {
                path = String.Format("{0}?{1}", path, GetTimeStamp(context, path));
            }
            builder.Attributes["href"] = path;
            builder.Attributes["rel"] = "stylesheet";
            builder.Attributes["type"] = "text/css";
            builder.Attributes["media"] = "screen, projection";
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(htmlAttributes, true);
            }
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        private static String JavascriptHelper(HttpContextBase context, String javascript, RouteValueDictionary htmlAttributes)
        {
            var builder = new TagBuilder("script");
            var path = ToAbsolutePath(javascript, JavascriptPath);
            if (Environment.Development.Equals(GetEnvironment()))
            {
                path = String.Format("{0}?{1}", path, GetTimeStamp(context, path));
            }
            builder.Attributes["src"] = path;
            builder.Attributes["type"] = "text/javascript";
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(htmlAttributes, true);
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Generates HTML markup for including css-files specified in assets config (<see cref="AssetPackagesConfigPath"/>).
        /// </summary>
        /// <param name="context">Current http context.</param>
        /// <param name="packageName">Name of the package specified in assets config.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup for including css-file specified in assets config.</returns>
        private static String CssPackHelper(HttpContextBase context, String packageName, RouteValueDictionary htmlAttributes)
        {
            var builder = new StringBuilder();
            var environment = GetEnvironment();
            var configPath = context.Server.MapPath(AssetPackagesConfigPath);

            if (Environment.Development.Equals(environment))
            {
                foreach (var file in AssetsHelper.GetCssPackFiles(environment, packageName, configPath))
                {
                    builder.Append(CssHelper(context, file, htmlAttributes));
                }
            }
            else
            {
                var cssServerPath = context.Server.MapPath(CssPath);
                var packageFileName = AssetsHelper.BuildCssPack(environment, packageName, cssServerPath, configPath);
                builder.Append(CssHelper(context, VirtualPathUtility.Combine(CssPath, packageFileName), htmlAttributes));
            }
            return builder.ToString();
        }

        private static String JavascriptPackHelper(HttpContextBase context, String packageName, RouteValueDictionary htmlAttributes)
        {
            var builder = new StringBuilder();
            var environment = GetEnvironment();
            var configPath = context.Server.MapPath(AssetPackagesConfigPath);

            if (Environment.Development.Equals(environment))
            {
                foreach (var file in AssetsHelper.GetJavascriptPackFiles(environment, packageName, configPath))
                {
                    builder.Append(JavascriptHelper(context, file, htmlAttributes));
                }
            }
            else
            {
                var javascriptServerPath = context.Server.MapPath(JavascriptPath);
                var packageFileName = AssetsHelper.BuildJavascriptPack(environment, packageName, javascriptServerPath, configPath);
                builder.Append(JavascriptHelper(context, VirtualPathUtility.Combine(JavascriptPath, packageFileName), htmlAttributes));
            }
            return builder.ToString();
        }

        private static Environment GetEnvironment()
        {
            return ServiceLocator.Current.GetInstance<IApplication>().Environment;
        }

        private static String ToAbsolutePath(String path, String directoryPath)
        {
            var result = path;
            if (!VirtualPathUtility.IsAbsolute(result))
            {
                if (!VirtualPathUtility.IsAppRelative(result))
                {
                    result = VirtualPathUtility.Combine(directoryPath, result);
                }
                result = VirtualPathUtility.ToAbsolute(result);
            }
            return result;
        }

        private static String GetTimeStamp(HttpContextBase context, String path)
        {
            DateTime lastModification = DateTime.UtcNow;
            var filePath = context.Server.MapPath(VirtualPathUtility.ToAppRelative(path));
            if (File.Exists(filePath))
            {
                lastModification = File.GetLastWriteTimeUtc(filePath);
            }
            return lastModification.ToString(TimeSpampFormat);
        }

        #endregion
    }
}