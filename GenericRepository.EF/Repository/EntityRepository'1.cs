
namespace GenericRepository.EntityFramework
{

    /// <summary>
    /// IEntityRepository implementation for DbContext instance where the TId type is Int32.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public class EntityRepository<TEntity> : EntityRepository<TEntity, int>, IEntityRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        /// <summary>
        /// Constructs an entity repository based on the supplied context
        /// </summary>
        /// <param name="dbContext">The db context</param>
        public EntityRepository(IEntitiesContext dbContext)
            : base(dbContext)
        {

        }
    }
}