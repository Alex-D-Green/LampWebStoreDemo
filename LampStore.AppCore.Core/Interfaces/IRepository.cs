using System.Collections.Generic;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Utilities;


namespace LampStore.AppCore.Core.Interfaces
{
    /// <summary>
    /// Generic repository for <typeparamref name="TEntity"/> entities.
    /// </summary>
    /// <remarks>This pattern is used to uncouple DBMS from the "client" code.
    /// <para>Most of the methods are asynchronous but the interface could have their synchronous pairs.</para>
    /// <para>This pattern could be implemented quite differently depending on our goals, priorities, its supposed usage etc.
    /// Here I implemented it in a way to maximize app layers separation and encapsulation but it could cost us some 
    /// performance.
    /// For instance, methods here return IEnumerable and detached entities which is good for separation but
    /// IQueryable could give us ability fetch from DB fields that needed indeed, perform any kinds of sorting, 
    /// filtration etc. and using lazy loading.
    /// So it's just one of possible implementations.</para>
    /// </remarks>
    public interface IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct
    {
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Get entities according to the parameters.
        /// </summary>
        /// <remarks>
        /// Could constrain the output amount of entities (could be used for pagination).
        /// Could perform sorting according to the given direction and entity's property (defined by an expression).
        /// </remarks>
        Task<IEnumerable<TEntity>> GetAsync(int? from = null, int? count = null,
            string sortingBy = null, SortDirection desc = SortDirection.Ascending);

        Task<IdHolder<TKey>> AddAsync(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);

        Task DeleteByIdAsync(TKey id);
    }
}
