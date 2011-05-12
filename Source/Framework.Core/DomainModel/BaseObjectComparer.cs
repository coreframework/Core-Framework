// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseObjectComparer.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// Provides a comparer for supporting LINQ methods such as Intersect, Union and Distinct.
    /// This may be used for comparing objects of type <see cref="BaseObject"/> and anything
    /// that derives from it, such as <see cref="Entity"/>.
    /// </summary>
    /// <remarks>
    /// Microsoft decided that set operators such as Intersect, Union and Distinct should
    /// not use the IEqualityComparer's Equals() method when comparing objects, but should instead
    /// use IEqualityComparer's GetHashCode() method.
    /// </remarks>
    public class BaseObjectComparer : IEqualityComparer<BaseObject>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare..</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        /// <c>true</c> if the specified objects are equal; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(BaseObject x, BaseObject y)
        {
            // While SQL would return false for the following condition, returning true when 
            // comparing two null values is consistent with the C# language
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null ^ y == null)
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which a hash code is to be returned.</param>
        /// <returns>
        /// A hash code for specified object, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(BaseObject obj)
        {
            return obj.GetHashCode();
        }
    }
}