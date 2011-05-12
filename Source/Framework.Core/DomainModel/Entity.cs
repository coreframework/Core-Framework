// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// Provides a base class for your objects which will be persisted to the database.    
    /// </summary>
    /// <remarks>
    /// <para>Long identifier covers most common cases and provides better performance for x64 processors then int.</para>
    /// <para>See <see cref="GenericEntity{T}" /> if you need custom type identifier.</para>
    /// </remarks>
    public class Entity : GenericEntity<long>
    {        
    }
}