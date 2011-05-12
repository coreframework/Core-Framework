// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPersistent.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// This serves as a base interface for persistent objects.
    /// See <see cref="GenericEntity{T}"/> for default implementation.
    /// </summary>
    /// <typeparam name="TId">The type of entity identifier.</typeparam>
    public interface IPersistent<TId>
    {
        /// <summary>
        /// Gets entity unique identifier.
        /// </summary>
        /// <value>Entity unique identifier.</value>
        TId Id { get; }

        /// <summary>
        /// Determines whether this instance is transient.
        /// </summary>
        /// <remarks>
        /// Transient objects are not associated with an item already in storage. For instance, a Customer is transient if its Id is 0.
        /// </remarks>
        /// <returns>
        /// <c>true</c> if this instance is transient; otherwise, <c>false</c>.
        /// </returns>
        bool IsTransient();     
    }
}