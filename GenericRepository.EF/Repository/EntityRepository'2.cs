using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.EntityFramework
{

    /// <summary>
    /// IEntityRepository implementation for DbContext instance.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TId">Type of entity Id</typeparam>
    public class EntityRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IComparable
    {

        private readonly IEntitiesContext _dbContext;
        /// <summary>
        /// Constructs an entity repository based on the supplied context
        /// </summary>
        /// <param name="dbContext">The db context</param>
        public EntityRepository(IEntitiesContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns>IQueryable collection of the entities</returns>
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets all the entities including the ones based on the supplied include properties
        /// </summary>
        /// <param name="includeProperties">The properties to include while fetching the entity data</param>
        /// <returns>The collection of querable entities</returns>
        public Task<IQueryable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return Task.FromResult(queryable);
        }

        /// <summary>
        /// Finds an collection of entities by the supplied predicate
        /// </summary>
        /// <param name="predicate">The predicate function</param>
        /// <returns>Collection of entities that matched the supplied predicate</returns>
        public Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll().Where(predicate));
        }

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        public Task<PaginatedList<TEntity>> PaginateAsync(int pageIndex, int pageSize)
        {
            var paginatedList = PaginateAsync(pageIndex, pageSize, x => x.Id);
            return paginatedList;
        }
        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        public Task<PaginatedList<TEntity>> PaginateAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector)
        {
            return PaginateAsync(pageIndex, pageSize, keySelector, null);
        }
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
        public Task<PaginatedList<TEntity>> PaginateAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var paginatedList = PaginateAsync(pageIndex, pageSize, keySelector, predicate, OrderByType.Ascending, includeProperties);
            return paginatedList;
        }
        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters in descending order
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        public Task<PaginatedList<TEntity>> PaginateDescendingAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector)
        {
            return PaginateDescendingAsync(pageIndex, pageSize, keySelector, null);
        }
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
        public Task<PaginatedList<TEntity>> PaginateDescendingAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var paginatedList = PaginateAsync(pageIndex, pageSize, keySelector, predicate, OrderByType.Descending, includeProperties);
            return paginatedList;
        }

        /// <summary>
        /// Gets a single entity based on the supplied identifier
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <returns>Entity data</returns>
        public Task<TEntity> GetSingleAsync(TId id)
        {
            IQueryable<TEntity> entities = GetAll();
            TEntity entity = Filter(entities, x => x.Id, id).FirstOrDefault();
            return Task.FromResult(entity);
        }

        /// <summary>
        /// Gets the entity data based on the supplied collection of identifiers
        /// </summary>
        /// <param name="ids">The collection of identifiers</param>
        /// <returns>collection of entities</returns>
        public Task<System.Collections.Generic.IEnumerable<TEntity>> GetByIdsAsync(TId[] ids)
        {
            IQueryable<TEntity> entities = GetAll();
            System.Collections.Generic.IEnumerable<TEntity> filteredEntities = entities.Where(e => ids.Contains(e.Id));
            return Task.FromResult(filteredEntities);
        }

        /// <summary>
        /// Gets the single entity including the ones based on the supplied include properties
        /// </summary>
        /// <param name="includeProperties">The properties to include while fetching the entity data</param>
        /// <param name="id">The identifier </param>
        /// <returns>The single entity</returns>
        public async Task<TEntity> GetSingleIncludingAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = await GetAllIncludingAsync(includeProperties);
            TEntity entity = Filter(entities, x => x.Id, id).FirstOrDefault();
            return entity;
        }
        /// <summary>
        /// Asynchronously adds an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        public Task AddAsync(TEntity entity)
        {
            _dbContext.SetAsAdded(entity);
            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// Adds a graph of related entities
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        public Task AddGraphAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// Asynchronously edits an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        public Task EditAsync(TEntity entity)
        {
            _dbContext.SetAsModified(entity);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Asynchronously delete an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.SetAsDeleted(entity);
            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// Asynchronously save an entity
        /// </summary>
        /// <returns>The number of records affected by the save operation</returns>
        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        // Privates

        private async Task<PaginatedList<TEntity>> PaginateAsync<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderByType orderByType, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable =
                 (orderByType == OrderByType.Ascending)
                    ? (await GetAllIncludingAsync(includeProperties)).OrderBy(keySelector)
                    : (await GetAllIncludingAsync(includeProperties)).OrderByDescending(keySelector);

            queryable = (predicate != null) ? queryable.Where(predicate) : queryable;
            var paginatedList = queryable.ToPaginatedList(pageIndex, pageSize);
            return paginatedList;
        }

        private IQueryable<TEntity> Filter<TProperty>(IQueryable<TEntity> dbSet,
            Expression<Func<TEntity, TProperty>> property, TProperty value)
            where TProperty : IComparable
        {
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("Property expected", "property");
            }

            Expression left = property.Body;
            Expression right = Expression.Constant(value, typeof(TProperty));
            Expression searchExpression = Expression.Equal(left, right);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(
                searchExpression, new ParameterExpression[] { property.Parameters.Single() });

            return dbSet.Where(lambda);
        }

        private enum OrderByType
        {
            Ascending,
            Descending
        }
    }
}