using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository
{
    /// <summary>
    /// The Repository contract for the entity with the key type of TId
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TId">the type of the primary key in the entity</typeparam>
    public interface IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IComparable
    {
        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns>IQueryable collection of the entities</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Finds the entities that match the given predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>An IQueryable collection of matched entities</returns>
        Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets the paginated list of entitybased on the page index and the number of records per page
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>An IQueryable collection of matched entities</returns>
        Task<PaginatedList<TEntity>> PaginateAsync(int pageIndex, int pageSize);

        /// <summary>
        /// Gets a single entity based on the supplied id
        /// </summary>
        /// <param name="id">The unique identifier for the entity record</param>
        /// <returns>The entity</returns>
        Task<TEntity> GetSingleAsync(TId id);

        /// <summary>
        /// Gets the entities that match the given set of ids
        /// </summary>
        /// <param name="ids">The collection of identifiers</param>
        /// <returns>A collection of entities</returns>
        Task<System.Collections.Generic.IEnumerable<TEntity>> GetByIdsAsync(TId[] ids);
    }
}