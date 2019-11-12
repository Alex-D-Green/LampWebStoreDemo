using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Entities;


namespace LampStore.AppCore.Services.Interfaces
{
    public interface ILampsComparisonService: IDisposable
    {
        Task<IEnumerable<Lamp>> GetAllLampsAsync();

        Task<IEnumerable<Comparison>> GetAllComparisonsAsync();

        Comparison DoComparison(Lamp fst, Lamp snd);

        Task SaveComparsionAsync(Comparison comparison);
    }
}
