// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteValueDictionaryExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Framework.Mvc.Extensions
{
    /// <summary>
    /// Extends <see cref="RouteValueDictionary"/> functionality.
    /// </summary>
    public static class RouteValueDictionaryExtensions
    {
        /// <summary>
        /// Merges <paramref name="newValues"/> to <paramref name="values"/>.
        /// </summary>
        /// <param name="values">Default values.</param>
        /// <param name="newValues">Override values.</param>
        /// <returns>
        /// New instance of dictionary containing all data from <paramref name="values"/> overriden by <paramref name="newValues"/>.
        /// </returns>
        public static RouteValueDictionary Merge(this RouteValueDictionary values, IDictionary<String, Object> newValues)
        {
            var newData = new RouteValueDictionary(values);
            if (newValues != null)
            {
                foreach (var pair in newValues)
                {
                    newData[pair.Key] = pair.Value;
                }
            }
            return newData;
        }
    }
}