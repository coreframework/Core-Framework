// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkResourceReader.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Microsoft.Practices.ServiceLocation;

namespace Framework.Mvc.Resources
{
    /// <summary>
    /// Read resources from <see cref="IResourceCache"/>.
    /// </summary>
    public class FrameworkResourceReader : IResourceReader
    {
        #region Fields

        private readonly IDictionary resources;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkResourceReader"/> class.
        /// </summary>
        /// <param name="scope">The scope.</param>
        public FrameworkResourceReader(String scope)
        {
            var cache = ServiceLocator.Current.GetInstance<IResourceCache>();
            resources = new Dictionary<String, String>();
            foreach (var resource in cache.Where(entry => entry.Key.StartsWith(scope)))
            {
                resources[resource.Key] = resource.Value;
            }
        }

        #endregion

        #region IResourceReader members

        /// <summary>
        /// Returns an <see cref="T:System.Collections.IDictionaryEnumerator"/> of the resources for this reader.
        /// </summary>
        /// <returns>
        /// A dictionary enumerator for the resources for this reader.
        /// </returns>
        public IDictionaryEnumerator GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Closes the resource reader after releasing any resources associated with it.
        /// </summary>
        public void Close()
        {
            // Do nothing.
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            // Do nothing.
        }

        #endregion
    }
}