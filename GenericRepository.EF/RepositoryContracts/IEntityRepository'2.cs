using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.EntityFramework
{
    /// <summary>
    /// Entity Framework interface implementation for IRepository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TId">Type of entity Id</typeparam>
    public interface IEntityRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IComparable
    {
        /// <summary>
        /// Gets all the entities including the ones based on the supplied include properties
        /// </summary>
        /// <param name="includeProperties">The properties to include while fetching the entity data</param>
        /// <returns>The collection of querable entities</returns>
        Task<IQueryable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// Gets the single entity including the ones based on the supplied include properties
        /// </summary>
        /// <param name="includeProperties">The properties to include while fetching the entity data</param>
        /// <param name="id">The identifier </param>
        /// <returns>The single entity</returns>
        Task<TEntity> GetSingleIncludingAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <param name="includeProperties">The properties to include</param>
        /// <param name="predicate">The predicate function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters in descending order
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateDescendingAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters in descending order
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <param name="includeProperties">The properties to include</param>
        /// <param name="predicate">The predicate function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateDescendingAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Asynchronously adds an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Adds a graph of related entities
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task AddGraphAsync(TEntity entity);

        /// <summary>
        /// Asynchronously edits an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task EditAsync(TEntity entity);

        /// <summary>
        /// Asynchronously delete an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Asynchronously save an entity
        /// </summary>
        /// <returns>The number of records affected by the save operation</returns>
        Task<int> SaveAsync();
    }
}