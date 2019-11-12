using System.Runtime.CompilerServices;

using AutoMapper;

using LampStore.AppCore.Core.Entities;
using LampStore.Infrastructure.EFStorage.Entities;

[assembly: InternalsVisibleTo("LampStore.Infrastructure.CompositionRoot")]

namespace LampStore.Infrastructure.EFStorage
{
    internal sealed class StorageMappingProfile: Profile
    {
        public StorageMappingProfile()
        {
            CreateMap<Lamp, LampEF>();
            CreateMap<LampEF, Lamp>();

            CreateMap<Comparison, ComparisonEF>();
            CreateMap<ComparisonEF, Comparison>();

            CreateMap<ComparisonPair, ComparisonPairEF>();
            CreateMap<ComparisonPairEF, ComparisonPair>();
        }
    }
}
