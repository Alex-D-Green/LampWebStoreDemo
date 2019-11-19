using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Interfaces;


namespace LampStore.AppCore.Services.Interfaces
{
    /// <summary>
    /// Lamps comparison service.
    /// </summary>
    public interface ILampsComparisonService: IDisposable
    {
        /// <summary>
        /// Lamps repository.
        /// </summary>
        IRepository<Lamp, int> Lamps { get; }


        /// <summary>
        /// Get comparisons according to the parameters.
        /// </summary>
        Task<IEnumerable<Comparison>> GetAllComparisonsAsync(int? from = null, int? count = null,
            string sortingBy = null, SortDirection desc = SortDirection.Ascending);

        /// <summary>
        /// Get comparison by id.
        /// </summary>
        /// <returns>Comparison item or <c>null</c> if item with this id wasn't found.</returns>
        Task<Comparison> GetComparisonByIdAsync(int id);

        /// <summary>
        /// Produce new comparison item (but this item would be saved by this method).
        /// </summary>
        Comparison DoComparison(Lamp fst, Lamp snd);

        /// <summary>
        /// Save comparison.
        /// </summary>
        Task SaveComparsionAsync(Comparison comparison);

        /// <summary>
        /// Delete comparison with certain id.
        /// </summary>
        Task DeleteComparsionAsync(int id);
    }
}
