// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataService.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.Services
{
    /// <summary>
    /// Since nearly all of the domain objects you create will have a type of int Id, this 
    /// data access interface leverages this assumption. If you want an entity with a type 
    /// other than int, such as string, then use <see cref="IGenericDataService{TEntity, IdT}" />.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDataService<TEntity> : IGenericDataService<TEntity, long>
    {
    }
}