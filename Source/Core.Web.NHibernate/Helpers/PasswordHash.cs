using System;
using Framework.Core.DomainModel;

namespace Core.Web.NHibernate.Helpers
{
    /// <summary>
    /// Contains hashed password.
    /// </summary>
    public class PasswordHash : IEquatable<PasswordHash>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <value>Hash value formatted as a base64-encoded string.</value>
        public String Hash { get; set; }

        /// <summary>
        /// Gets or sets the salt. Salt consists of random bits that are used as one of the inputs to a key derivation function.
        /// </summary>
        /// <value>Random bits formatted as a base64-encoded string.</value>
        public String Salt { get; set; }

        #endregion

        #region Equality methods

        /// <summary>
        /// Determines whether the specified <see cref="PasswordHash"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="PasswordHash"/> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="PasswordHash"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(PasswordHash other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.Hash, Hash) && Equals(other.Salt, Salt);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(PasswordHash))
            {
                return false;
            }

            return Equals((PasswordHash) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                if (Hash != null)
                {
                    hashCode = hashCode * BaseObject.HashMultiplier ^ Hash.GetHashCode();
                }
                if (Salt != null)
                {
                    hashCode = hashCode * BaseObject.HashMultiplier ^ Salt.GetHashCode();
                }
                return hashCode;
            }
        }

        #endregion
    }
}

