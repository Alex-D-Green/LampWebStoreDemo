using System;
using System.Threading.Tasks;

using AutoMapper;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Exceptions;
using LampStore.AppCore.Core.Interfaces;
using LampStore.Infrastructure.EFStorage.DbContexts;
using LampStore.Infrastructure.EFStorage.Repositories;

using Microsoft.EntityFrameworkCore;


namespace LampStore.Infrastructure.EFStorage.UnitsOfWork
{
    /// <summary>
    /// The unit of work for all store related repositories.
    /// See also remarks on <see cref="StoreContext"/>.
    /// </summary>
    internal sealed class UnitOfWork: IUnitOfWork
    {
        private readonly StoreContext db; //Or DbContext (depends on whether the UoF is specified or not)...
        private readonly IMapper mapper;


        private IRepository<Lamp, int> lamps;
        private IRepository<Comparison, int> comparisons;


        public IRepository<Lamp, int> Lamps
        { get => lamps ?? (lamps = new LampsRepository(db, mapper)); } //Kinda lazy initialization

        public IRepository<Comparison, int> Comparisons
        { get => comparisons ?? (comparisons = new ComparisonsRepository(db, mapper)); } //Kinda lazy initialization


        public UnitOfWork(IDbFactory<StoreContext> factory, IMapper mapper)
        {
            if(factory is null)
                throw new ArgumentNullException(nameof(factory));

            db = factory.Get(); //Using of "factory" pattern

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Dispose()
        {
            db.Dispose();
        }


        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch(Exception e) when(e is DbUpdateException || e is DbUpdateConcurrencyException)
            {
                //Maybe SqlExceptions should be catched here as well...
                throw new StorageException(e.Message, e); //To decouple from ORM types
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception e) when(e is DbUpdateException || e is DbUpdateConcurrencyException)
            {
                //Maybe SqlExceptions should be catched here as well...
                throw new StorageException(e.Message, e); //To decouple from ORM types
            }
        }
    }
}
