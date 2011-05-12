// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenericDataService.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides a standard interface for data-access.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of entity identifier.</typeparam>
    public interface IGenericDataService<TEntity, TId> : IService
    {
        /// <summary>
        /// Finds entity by id specified.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Entity or null if a record is not found matching the provided Id.</returns>
        TEntity Find(TId id);

        /// <summary>
        /// Retrieves all items of a given type from repository.
        /// </summary>
        /// <returns>Not null collection of <typeparamref name="TEntity"/>.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Retrieves items of <typeparamref name="TEntity"/> from repository paged.
        /// </summary>
        /// <param name="page">Zero-based index of page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Not nullable collection of <typeparamref name="TEntity"/>.</returns>
        IEnumerable<TEntity> GetPaged(int page, int pageSize);

        /// <summary>
        /// Saves specified entity to repository.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns><c>true</c> if instance instance has been saved successfully; otherwise, <c>false</c>.</returns>
        bool Save(TEntity entity);

        /// <summary>
        /// Removes specified entity from repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns><c>true</c> if instance instance has been deleted successfully; otherwise, <c>false</c>.</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Deletes all entities of type <typeparamref name="TEntity"/> from repository.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Creates the LINQ query used to evaluate an expression tree.
        /// </summary>
        /// <returns>Query object used to evaluate an expression tree.</returns>
        IQueryable<TEntity> CreateQuery();
    }
}