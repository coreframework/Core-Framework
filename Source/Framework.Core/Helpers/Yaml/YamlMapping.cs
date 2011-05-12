// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YamlMapping.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Yaml.Grammar;

namespace Framework.Core.Helpers.Yaml
{
    /// <summary>
    /// Wraps <see cref="Mapping"/> object to provide dynamic access.
    /// </summary>
    public class YamlMapping : DynamicObject, IEnumerable
    {
        #region Fields

        private readonly Dictionary<String, Object> innerValues = new Dictionary<String, Object>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlMapping"/> class.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        public YamlMapping(Mapping mapping)
        {
            foreach (var entry in mapping.Enties)
            {
                Object value;
                if (YamlDocument.TryMapValue(entry.Value, out value))
                {
                    innerValues[(entry.Key as Scalar).Text] = value;
                }
            }
        }

        #endregion

        #region DynamicObject members

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result"/>.</param>
        /// <returns>
        ///     <c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns false, the run-time binder of the language determines the behavior (in most cases, a run-time exception is thrown).
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out Object result)
        {
            if (TryGetValue(binder.Name, out result))
            {
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        /// <summary>
        /// Provides the implementation for operations that get a value by index. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for indexing operations.
        /// </summary>
        /// <param name="binder">Provides information about the operation.</param>
        /// <param name="indexes">The indexes that are used in the operation. For example, for the sampleObject[3] operation in C# (sampleObject(3) in Visual Basic), where sampleObject is derived from the DynamicObject class, <paramref name="indexes"/> first element is equal to 3.</param>
        /// <param name="result">The result of the index operation.</param>
        /// <returns>
        ///     <c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns false, the run-time binder of the language determines the behavior (in most cases, a run-time exception is thrown).
        /// </returns>
        public override bool TryGetIndex(GetIndexBinder binder, Object[] indexes, out Object result)
        {
            var key = indexes[0] as String;
            if (key != null)
            {
                if (TryGetValue(key, out result))
                {
                    return true;
                }
            }

            return base.TryGetIndex(binder, indexes, out result);
        }

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return innerValues.GetEnumerator();
        }

        #endregion

        #region Helper methods

        private bool TryGetValue(String key, out Object result)
        {
            if (innerValues.ContainsKey(key))
            {
                result = innerValues[key];
            }
            else
            {
                var normilizedKey = key.ToLowerInvariant();
                if (innerValues.ContainsKey(normilizedKey))
                {
                    result = innerValues[normilizedKey];
                }
                else
                {
                    result = null;
                }
            }

            return true;
        }

        #endregion
    }
}
