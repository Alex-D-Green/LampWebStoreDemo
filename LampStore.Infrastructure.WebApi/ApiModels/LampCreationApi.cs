using LampStore.AppCore.Core.Entities;

namespace LampStore.Infrastructure.WebApi.ApiModels
{
    public sealed class LampCreationApi
    {
        public LampType LampType { get; set; }

        public string Manufacturer { get; set; }

        public double Cost { get; set; }

        public string ImageRef { get; set; }
    }
}
