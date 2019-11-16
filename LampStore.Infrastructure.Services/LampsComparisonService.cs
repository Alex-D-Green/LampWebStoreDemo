using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Interfaces;
using LampStore.AppCore.Core.Utilities;
using LampStore.AppCore.Services.Interfaces;

using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("LampStore.Infrastructure.CompositionRoot")]

namespace LampStore.Infrastructure.Services
{
    internal sealed class LampsComparisonService: ILampsComparisonService
    {
        private readonly IUnitOfWork db;
        private readonly ILogger<LampsComparisonService> logger;


        public LampsComparisonService(IUnitOfWork db, ILogger<LampsComparisonService> logger)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Dispose()
        {
            db.Dispose();
        }


        public async Task<IEnumerable<Lamp>> GetAllLampsAsync()
        {
            logger.LogInformation($"Invoke {nameof(GetAllLampsAsync)}()");

            return await db.Lamps.GetAsync();
        }

        public async Task<IEnumerable<Comparison>> GetAllComparisonsAsync()
        {
            logger.LogInformation($"Invoke {nameof(GetAllComparisonsAsync)}()");

            return await db.Comparisons.GetAsync();
        }

        public async Task<Comparison> GetComparisonByIdAsync(int id)
        {
            return await db.Comparisons.GetByIdAsync(id);
        }

        public Comparison DoComparison(Lamp fst, Lamp snd)
        {
            if(fst is null)
                throw new ArgumentNullException(nameof(fst));

            if(snd is null)
                throw new ArgumentNullException(nameof(snd));


            logger.LogInformation($"Invoke {nameof(DoComparison)}()");

            var pairs = new List<ComparisonPair>();

            foreach(PropertyInfo prop in typeof(Lamp).GetProperties().Where(o => o.Name != nameof(Lamp.Id)))
            {
                object fstVal = prop.GetValue(fst);
                object sndVal = prop.GetValue(snd);

                if(!Object.Equals(fstVal, sndVal))
                    pairs.Add(new ComparisonPair(prop.Name, fstVal?.ToString(), sndVal?.ToString()));
            }

            return new Comparison(fst, snd, pairs);
        }

        public async Task SaveComparsionAsync(Comparison comparison)
        {
            if(comparison is null)
                throw new ArgumentNullException(nameof(comparison));

            if(comparison.IsIdSet())
                throw new ArgumentException($"The Id property should not be set.", nameof(comparison));


            logger.LogInformation($"Invoke {nameof(SaveComparsionAsync)}()");

            IdHolder<int> idHolder = await db.Comparisons.AddAsync(comparison);
            await db.SaveAsync();

            comparison.SetId(idHolder.Id);
        }
    }
}
