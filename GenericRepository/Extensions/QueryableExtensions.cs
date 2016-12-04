using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace GenericRepository
{
    /// <summary>
    /// The extensions for the IQueryable types
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryableExtensions
    {
        /// <summary>
        /// Gets a paginated list fomr the given queryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PaginatedList<T> ToPaginatedList<T>(
            this IQueryable<T> query, int pageIndex, int pageSize)
        {
            int totalCount = query.Count();
            var collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedList<T>(collection, pageIndex, pageSize, totalCount);
        }
    }
}