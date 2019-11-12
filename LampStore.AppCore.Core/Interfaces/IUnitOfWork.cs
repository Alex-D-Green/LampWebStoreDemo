using System;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Entities;


namespace LampStore.AppCore.Core.Interfaces
{
    /// <summary>
    /// The unit of work interface.
    /// </summary>
    /// <remarks>This pattern is used to share DB context between several repositories.</remarks>
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Lamp, int> Lamps { get; }

        IRepository<Comparison, int> Comparisons { get; }


        void Save();

        Task SaveAsync();
    }
}
