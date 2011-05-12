// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseObject.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// Provides a standard base class for facilitating comparison of objects.
    /// </summary>
    /// <remarks>
    /// For a discussion of the implementation of Equals/GetHashCode, see 
    /// http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
    /// and http://groups.google.com/group/sharp-architecture/browse_thread/thread/f76d1678e68e3ece?hl=en for 
    /// an in depth and conclusive resolution.
    /// </remarks>
    public abstract class BaseObject
    {
        #region Fields

        /// <summary>
        /// To help ensure hashcode uniqueness, a carefully selected random number multiplier is used within the calculation. 
        /// Goodrich and Tamassia's Data Structures and Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number of collissions.
        /// </summary>
        /// <remarks>
        /// See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/ for more information.
        /// </remarks>
        public const int HashMultiplier = 31;

        /// <summary>
        /// This static member caches the domain signature properties to avoid looking them up for each instance of the same type.
        /// </summary>
        [ThreadStatic]
        private static Dictionary<Type, IEnumerable<PropertyInfo>> signaturePropertiesDictionary;

        #endregion

        #region Comparison support

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Object obj) 
        {
            BaseObject compareTo = obj as BaseObject;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            return compareTo != null && GetInnerType().Equals(compareTo.GetInnerType()) && HasSameObjectSignatureAs(compareTo);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <remarks>
        /// This is used to provide the hashcode identifier of an object using the signature 
        /// properties of the object; although it's necessary for NHibernate's use, this can 
        /// also be useful for business logic purposes and has been included in this base 
        /// class, accordingly.  Since it is recommended that GetHashCode change infrequently, 
        /// if at all, in an object's lifetime, it's important that properties are carefully
        /// selected which truly represent the signature of an object.
        /// </remarks>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() 
        {
            unchecked
            {
                int hashCode;

                IEnumerable<PropertyInfo> signatureProperties = GetSignatureProperties();

                if (signatureProperties.Any()) 
                {
                    // It's possible for two objects to return the same hash code based on 
                    // identically valued properties, even if they're of two different types, 
                    // so we include the object's type in the hash calculation
                    hashCode = GetType().GetHashCode();

                    foreach (PropertyInfo property in signatureProperties)
                    {
                        Object value = property.GetValue(this, null);

                        if (value != null)
                        {
                            hashCode = (hashCode * HashMultiplier) ^ value.GetHashCode();
                        }
                    }
                }
                else
                {
                    // If no properties were flagged as being part of the signature of the object,
                    // then simply return the hashcode of the base object as the hashcode.
                    hashCode = base.GetHashCode();
                }

                return hashCode;
            }
        }

        /// <summary>
        /// You may override this method to provide your own comparison routine.
        /// </summary>
        /// <param name="compareTo">The <see cref="BaseObject"/> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="BaseObject"/> has the same signature properties values as this instance; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasSameObjectSignatureAs(BaseObject compareTo) 
        {
            IEnumerable<PropertyInfo> signatureProperties = GetSignatureProperties();

            foreach (PropertyInfo property in signatureProperties) 
            {
                Object valueOfThisObject = property.GetValue(this, null);
                Object valueToCompareTo = property.GetValue(compareTo, null);

                if (valueOfThisObject == null && valueToCompareTo == null)
                {
                    continue;
                }

                if ((valueOfThisObject == null ^ valueToCompareTo == null) || (!valueOfThisObject.Equals(valueToCompareTo))) 
                {
                    return false;
                }
            }

            // If we've gotten this far and signature properties were found, then we can
            // assume that everything matched; otherwise, if there were no signature 
            // properties, then simply return the default bahavior of Equals
            return signatureProperties.Any() || base.Equals(compareTo);
        }

        /// <summary>
        /// <para>Gets the signature properties.</para>
        /// </summary>
        /// <remarks>
        /// Static dictionary used to reduce performance. So properties will be retrieved 
        /// only once per class for each thread after application start.
        /// </remarks>
        /// <returns>Set of properties that should be included in the object signature comparison.</returns>
        public virtual IEnumerable<PropertyInfo> GetSignatureProperties() 
        {
            IEnumerable<PropertyInfo> properties;

            // Init the signaturePropertiesDictionary here due to reasons described at 
            // http://blogs.msdn.com/jfoscoding/archive/2006/07/18/670497.aspx
            if (signaturePropertiesDictionary == null)
            {
                signaturePropertiesDictionary = new Dictionary<Type, IEnumerable<PropertyInfo>>();
            }

            // TryGetValue method used to reduce performance instead of 2 method calls (ContainsKey, GetValue).
            if (!signaturePropertiesDictionary.TryGetValue(GetType(), out properties))
            {
                properties = GetTypeSpecificSignatureProperties();
                signaturePropertiesDictionary[GetType()] = properties;
            }

            return properties;
        }
        
        /// <summary>
        /// Enforces the template method pattern to have child objects determine which specific
        /// properties should and should not be included in the object signature comparison.  
        /// </summary>
        /// <remarks>
        /// BaseObject already takes care of performance caching, so this method
        /// shouldn't worry about caching, just return set of properties.
        /// </remarks>
        /// <returns>Set of properties that should be included in the object signature comparison.</returns>
        protected abstract IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties();

        /// <summary>
        /// When NHibernate proxies objects, it masks the type of the actual entity object.
        /// This wrapper burrows into the proxied object to get its actual type.
        /// Although this assumes NHibernate is being used, it doesn't require any NHibernate
        /// related dependencies and has no bad side effects if NHibernate isn't being used.
        /// </summary>
        /// <remarks>Related discussion is at http://groups.google.com/group/sharp-architecture/browse_thread/thread/ddd05f9baede023a .</remarks>
        /// <returns>Type of inner object for proxied objects and type for simple objects.</returns>
        protected virtual Type GetInnerType() 
        {
            return GetType();
        }

        #endregion
    }
}
