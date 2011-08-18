// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Framework.MVC.Extensions;

namespace Framework.MVC.Helpers
{
    /// <summary>
    /// Provides methods for resources retrieving.
    /// </summary>
    public class ResourceHelper
    {
        #region Fields

        /// <summary>
        /// Resources scope separator.
        /// </summary>
        public const String ScopeSeparator = ".";

        /// <summary>
        /// Slash separator.
        /// </summary>
        public const char SlashSeparator = '\\';

        /// <summary>
        /// View scope key.
        /// </summary>
        public const String ViewScopeKey = "ViewScopeKey";

        /// <summary>
        /// Areas constant.
        /// </summary>
        public const String Areas = "Areas";

        /// <summary>
        /// Areas built in framework.
        /// </summary>
        public static String[] MainAreas = new[] { "admin", "navigation", "views" };

        private const String Models = "Models";

        private const String Controllers = "Controllers";

        private const String ErrorMessages = "ErrorMessages";

        #endregion

        /// <summary>
        /// Gets resource string using specified scope, key and culture.
        /// </summary>
        /// <remarks>
        /// <paramref name="translationMissing"/> handler will be called if resource can not be found.
        /// </remarks>
        /// <param name="context">The http context.</param>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The localization scope.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="translationMissing">Translation missing fallback.</param>
        /// <returns>Localized string or <paramref name="translationMissing"/> result.</returns>
        public static String GetResourceString(HttpContextBase context, String key, String scope, CultureInfo culture, Func<String, String, String, String> translationMissing)
        {
            if (culture == null)
            {
                culture = Thread.CurrentThread.CurrentUICulture;
            }

            var result = context.GetGlobalResourceObject(scope, key, culture) as String;
            if (result == null && translationMissing != null)
            {
                result = translationMissing(key, scope, culture.Name);
            }

            return result;
        }

        /// <summary>
        /// Translates property name using model scope.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="propertyName">Name of the proprty.</param>
        /// <returns>
        /// Localized string or <c>null</c>.
        /// </returns>
        public static String TranslatePropertyName(HttpContextBase context, Type modelType, String propertyName)
        {
            return GetResourceString(context, propertyName, GetModelScope(modelType), null, null);
        }

        /// <summary>
        /// Translates validation error message.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="validatorKey">The validator key.</param>
        /// <returns>Localized string or <c>null</c>.</returns>
        public static String TranslateErrorMessage(HttpContextBase context, Type modelType, String propertyName, String validatorKey)
        {
            var message = GetResourceString(context, validatorKey, GetModelSpecificMessagesScope(modelType, propertyName), null, null);
            if (String.IsNullOrEmpty(message))
            {
                message = GetResourceString(context, validatorKey, GetCommonMessagesScope(), null, null);
            }
            return message;
        }

        /// <summary>
        /// Translates the error message and replace params.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="validatorKey">The validator key.</param>
        /// <param name="validationParams">The validation params.</param>
        /// <returns>Localized string or <c>null</c>.</returns>
        public static String TranslateErrorMessage(HttpContextBase context, Type modelType, String propertyName, String validatorKey, String[] validationParams)
        {
            var message = GetResourceString(context, validatorKey, GetModelSpecificMessagesScope(modelType, propertyName), null, null);
            if (String.IsNullOrEmpty(message))
            {
                message = GetResourceString(context, validatorKey, GetCommonMessagesScope(), null, null);
            }
            var propertyNameText = TranslatePropertyName(context, modelType, propertyName);
            return String.Format(message, propertyNameText, validationParams);
        }

        /// <summary>
        /// Generates resource key from <paramref name="chains"/>.
        /// </summary>
        /// <param name="chains">The chains.</param>
        /// <returns>Resource key generated from chains specified.</returns>
        public static String Combine(params String[] chains)
        {
            return String.Join(ScopeSeparator, chains);
        }

        /// <summary>
        /// Generates resource key from <paramref name="chains"/>.
        /// </summary>
        /// <param name="chains">The chains.</param>
        /// <returns>Resource key generated from chains specified.</returns>
        public static String Combine(IEnumerable<String> chains)
        {
            return String.Join(ScopeSeparator, chains);
        }

        /// <summary>
        /// Builds model localization scope by it's namespace and type ([AreaName.]Models.{Namespace}.{TypeName}).
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>Model localization scope.</returns>
        public static String GetModelScope(Type modelType)
        {
            // Adds namespace and class name to scope.
            var chains = modelType.Namespace.Split(Type.Delimiter).ToList();
            chains.Add(modelType.Name);

            // Removes redudant chains.
            if (chains.Contains(Areas) || chains.Contains(Models))
            {
                var modelScope = chains.SkipWhile(x => !Areas.Equals(x) && !Models.Equals(x));
                if (modelScope.Count() > 0 && Areas.Equals(modelScope.First()))
                {
                    modelScope = modelScope.Skip(1);
                }

                chains = modelScope.ToList();
            }

            // Get real Area name
            var assembly = Assembly.GetAssembly(modelType);
            if (assembly.Location.Contains(Areas.ToUpper()))
            {
                var pathItems = assembly.Location.Split(SlashSeparator).SkipWhile(x => !Areas.ToUpper().Equals(x));

                if (pathItems.Count() > 1)
                {
                    chains.Insert(0, pathItems.Skip(1).First());
                }
            }

            return Combine(chains);
        }

        /// <summary>
        /// Gets the model specific error messages localization scope.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>ErrorMessages localization scope.</returns>
        public static String GetModelSpecificMessagesScope(Type modelType, String propertyName)
        {
            return Combine(new[] { GetModelScope(modelType), propertyName, ErrorMessages });
        }

        /// <summary>
        /// Gets the common error messages localization scope.
        /// </summary>
        /// <returns>ErrorMessages localization scope.</returns>
        public static String GetCommonMessagesScope()
        {
            return Combine(new[] { Models, ErrorMessages });
        }

        /// <summary>
        /// Builds controller localization scope by it's namespace and type ([AreaName.]Controllers.{ControllerName}).
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>Controller localization scope.</returns>
        public static String GetControllerScope(Controller controller)
        {
            return GetControllerScope(controller.AreaName(), controller.ControllerName());
        }

        /// <summary>
        /// Gets the controller scope.
        /// </summary>
        /// <param name="areaName">Name of the area.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>Controller localization scope.</returns>
        public static String GetControllerScope(String areaName, String controllerName)
        {
            var chains = new List<String>();            
            if (!String.IsNullOrEmpty(areaName))
            {
                chains.Add(areaName);
            }
            chains.Add(Controllers);
            if (!String.IsNullOrEmpty(controllerName))
            {
                chains.Add(controllerName);
            }

            return Combine(chains);
        }

        /// <summary>
        /// Builds view localization scope by view path (removes leading Areas directory).
        /// </summary>
        /// <param name="viewPath">The view path.</param>
        /// <returns>View localization scope.</returns>
        public static String GetViewScope(String viewPath)
        {
            var chains = PathHelper.SplitPath(PathHelper.TrimVirtualPathStart(viewPath));
            chains = chains.SkipWhile(x => Areas.Equals(x));

            return Path.GetFileNameWithoutExtension(Combine(chains));
        }

        /// <summary>
        /// Builds view localization scope by view path (removes leading Areas directory).
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <returns>View localization scope.</returns>
        public static String GetViewScope(ViewContext viewContext)
        {
            var scope = String.Empty;

            if (viewContext.ViewData.ContainsKey(ViewScopeKey))
            {
                scope = viewContext.ViewData[ViewScopeKey] as String;
            }
            else
            {
                var viewPage = viewContext.View as WebFormView;
                if (viewPage != null)
                {
                    scope = GetViewScope(viewPage.ViewPath);
                    viewContext.ViewData[ViewScopeKey] = scope;
                }
            }

            return scope;
        }

        /// <summary>
        /// Builds partial view localization scope by view path (removes leading Areas directory).
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="partialViewName">Name of partial view.</param>
        /// <returns>View localization scope.</returns>
        public static String GetPartialViewScope(ViewContext viewContext, String partialViewName)
        {
            var scope = String.Empty;
            var viewPath = GetPartialViewPath(viewContext, partialViewName);

            if (!String.IsNullOrEmpty(viewPath))
            {
                scope = GetViewScope(viewPath);
            }

            return scope;
        }

        private static String GetPartialViewPath(ViewContext viewContext, String partialViewName)
        {
            var viewPath = String.Empty;
            if (VirtualPathUtility.IsAppRelative(partialViewName))
            {
                viewPath = partialViewName;
            }
            else
            {
                var viewResult = ViewEngines.Engines.FindPartialView(viewContext, partialViewName);
                if (viewResult.View != null)
                {
                    var viewPage = viewResult.View as WebFormView;
                    if (viewPage != null)
                    {
                        viewPath = viewPage.ViewPath;
                    }
                }
            }
            return viewPath;
        }
    }
}