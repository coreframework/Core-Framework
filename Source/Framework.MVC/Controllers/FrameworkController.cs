// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkController.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Framework.Core.Controllers;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;

namespace Framework.Mvc.Controllers
{
    /// <summary>
    /// Provides controller basic functionality.
    /// </summary>
    public class FrameworkController : BaseController
    {
        #region Localization

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        protected String Translate(String key, String scope)
        {
            return HttpContext.Translate(key, scope);
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        protected String Translate(String key, IEnumerable<String> scope)
        {
            return HttpContext.Translate(key, scope);
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> specified.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>String localized for current thread culture.</returns>
        protected String Translate(String key)
        {
            var scope = String.Empty;
            if (key.StartsWith(ResourceHelper.ScopeSeparator))
            {
                key = key.Remove(0, ResourceHelper.ScopeSeparator.Length);
                scope = ResourceHelper.GetControllerScope(this);
            }

            return Translate(key, scope);
        }

        #endregion
    }
}