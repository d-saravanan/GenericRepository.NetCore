using System;
namespace GenericRepository
{
    /// <summary>
    /// The entity contract
    /// </summary>
    /// <typeparam name="TId">Data Type of the primary key</typeparam>
    public interface IEntity<TId> where TId : IComparable
    {
        /// <summary>
        /// The primary key
        /// </summary>
        TId Id { get; set; }
    }
}