using System;

using AutoMapper;

using DAL.EF;

using LampStore.AppCore.Core.Interfaces;
using LampStore.AppCore.Services.Interfaces;
using LampStore.Infrastructure.EFStorage;
using LampStore.Infrastructure.EFStorage.DbContexts;
using LampStore.Infrastructure.EFStorage.UnitsOfWork;
using LampStore.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LampStore.Infrastructure.CompositionRoot
{
    /// <summary>
    /// Composition root for asp.core IoC.
    /// </summary>
    ///<seealso href="https://stackoverflow.com/a/9505530/2187280"/>
    public static class IoCConfigurator
    {
        public static IServiceCollection AddFromCompositionRoot(this IServiceCollection services, IConfiguration configuration, 
            Action<IMapperConfigurationExpression> addToAutoMapper = null)
        {
            if(services is null)
                throw new ArgumentNullException(nameof(services));

            if(configuration is null)
                throw new ArgumentNullException(nameof(configuration));


            //AutoMapper configuring
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<StorageMappingProfile>();

                addToAutoMapper?.Invoke(mc); //To include other AutoMapper profiles, from the main assembly
            });

            services.AddSingleton(mappingConfig.CreateMapper());


            //The rest of the IoC entries
            string connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IDbFactory<StoreContext>, DbFactory<StoreContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILampsComparisonService, LampsComparisonService>();

            return services;
        }
    }
}
