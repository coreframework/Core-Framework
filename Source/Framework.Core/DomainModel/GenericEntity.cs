// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericEntity.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// Provides a base class for persisntence objects (objects which will be persisted to the database or other data storage).
    /// </summary>
    /// <typeparam name="TId">The type of entity identifier.</typeparam>
    /// <remarks>
    /// For a discussion of this object, see 
    /// http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
    /// </remarks>
    public abstract class GenericEntity<TId> : BaseObject, IPersistent<TId>
    {
        #region Fields

        private int? cachedHashcode;

        #endregion

        #region IGenericEntity members

        /// <summary>
        /// Gets or sets entity unique identifier.
        /// </summary>
        /// <remarks>
        /// <para>Id may be of type string, int, custom type, etc.</para>
        /// <para>Setter is protected to allow unit tests to set this property via reflection and to allow
        /// domain objects more flexibility in setting this for those objects with assigned Ids.</para>
        /// <para>This is ignored for XML serialization because it does not have a public setter (which is very much by design).</para>
        /// </remarks>
        /// <value>Entity unique identifier.</value>
        [XmlIgnore]
        public virtual TId Id { get; protected set; }

        /// <summary>
        /// Determines whether this instance is transient.
        /// </summary>
        /// <remarks>
        /// Transient objects are not associated with an item already in storage. For instance, a Customer is transient if its Id is 0.
        /// </remarks>
        /// <returns>
        /// <c>true</c> if this instance is transient; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }

        #endregion

        #region Comparison support

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Object obj)
        {
            GenericEntity<TId> compareTo = obj as GenericEntity<TId>;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (compareTo == null || !GetType().Equals(compareTo.GetInnerType()))
            {
                return false;
            }

            if (HasSameNonDefaultIdAs(compareTo))
            {
                return true;
            }

            // Since the Ids aren't the same, both of them must be transient to compare domain signatures; 
            // because if one is transient and the other is a persisted entity, then they cannot be the same object.
            return IsTransient() && compareTo.IsTransient() && HasSameObjectSignatureAs(compareTo);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        /// <remarks>
        /// If object is persisntence, only Type and Id used for hash calculation.
        /// </remarks>
        public override int GetHashCode()
        {
            if (cachedHashcode.HasValue)
            {
                return cachedHashcode.Value;
            }

            if (IsTransient())
            {
                cachedHashcode = base.GetHashCode();
            }
            else
            {
                unchecked
                {
                    // It's possible for two objects to return the same hash code based on 
                    // identically valued properties, even if they're of two different types, 
                    // so we include the object's type in the hash calculation
                    int hashCode = GetType().GetHashCode();
                    cachedHashcode = (hashCode * HashMultiplier) ^ Id.GetHashCode();
                }
            }

            return cachedHashcode.Value;
        }

        /// <summary>
        /// The property getter for SignatureProperties should ONLY compare the properties which make up the "domain signature" of the object.
        /// If you choose NOT to override this method (which will be the most common scenario), then you should decorate
        /// the appropriate property(s) with [DomainSignature] and they will be compared automatically.
        /// This is the preferred method of managing the domain signature of entity objects.
        /// </summary>
        /// <remarks>
        /// This ensures that the entity has at least one property decorated with the [DomainSignature] attribute.
        /// </remarks>
        /// <returns>
        /// Set of properties that should be included in the object signature comparison.
        /// </returns>
        protected override IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
        {
            return GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(DomainSignatureAttribute), true));
        }

        /// <summary>
        /// Determines whether self and the provided entity have the same Id values.
        /// </summary>
        /// <param name="compareTo">The compare to.</param>
        /// <returns>
        /// <c>true</c> if self and the provided entity have the same Id values; otherwise, <c>false</c>.
        /// </returns>
        private bool HasSameNonDefaultIdAs(GenericEntity<TId> compareTo)
        {
            return !IsTransient() && !compareTo.IsTransient() && Id.Equals(compareTo.Id);
        }

        #endregion
    }
}