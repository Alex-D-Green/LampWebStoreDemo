using AutoMapper;

using LampStore.AppCore.Core.Entities;
using LampStore.Infrastructure.WebApi.ApiModels;


namespace LampStore.Infrastructure.WebApi
{
    internal sealed class WebAppMappingProfile: Profile
    {
        public WebAppMappingProfile()
        {
            CreateMap<LampCreationApi, Lamp>();
        }
    }
}
