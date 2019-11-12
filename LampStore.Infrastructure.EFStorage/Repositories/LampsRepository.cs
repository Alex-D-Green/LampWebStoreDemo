﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Exceptions;
using LampStore.AppCore.Core.Interfaces;
using LampStore.Infrastructure.EFStorage.DbContexts;
using LampStore.Infrastructure.EFStorage.Entities;

using Microsoft.EntityFrameworkCore;


namespace LampStore.Infrastructure.EFStorage.Repositories
{
    internal sealed class LampsRepository: IRepository<Lamp, int>
    {
        private readonly StoreContext db;
        private readonly IMapper mapper;


        public LampsRepository(StoreContext db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IEnumerable<Lamp>> GetAsync(int? from = null, int? count = null,
            string sortingBy = null, SortDirection desc = SortDirection.Ascending)
        {
            if(from < 0)
                throw new ArgumentOutOfRangeException(nameof(from));

            if(count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));


            IQueryable<LampEF> query = db.Lamps.AsNoTracking();

            if(sortingBy != null)
            {
                //Here probably needed to do something more "elegant"...

                Expression<Func<LampEF, object>> expr = null;

                switch(sortingBy)
                {
                    case nameof(Lamp.Id):           expr = o => o.Id; break;
                    case nameof(Lamp.Manufacturer): expr = o => o.Manufacturer; break;
                    case nameof(Lamp.LampType):     expr = o => o.LampType; break;
                    case nameof(Lamp.Cost):         expr = o => o.Cost; break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(sortingBy));
                }

                query = desc == SortDirection.Ascending ? query.OrderBy(expr) : query.OrderByDescending(expr);
            }

            if(from.HasValue)
                query = query.Skip(from.Value);

            if(count.HasValue)
                query = query.Take(count.Value);

            return mapper.Map<IEnumerable<Lamp>>(await query.ToArrayAsync());
        }

        public async Task<Lamp> GetByIdAsync(int id)
        {
            LampEF ret = await db.Lamps.FindAsync(id);

            if(ret == null)
                return null;

            return mapper.Map<Lamp>(ret);
        }

        public async Task AddAsync(Lamp item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            await db.Lamps.AddAsync(mapper.Map<LampEF>(item));
        }

        public void Delete(Lamp item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            db.Lamps.Remove(mapper.Map<LampEF>(item));
        }

        public async Task DeleteByIdAsync(int id)
        {
            LampEF item = await db.Lamps.FindAsync(id) ?? throw new StorageException($"Item with id = {id} not found.");

            db.Lamps.Remove(item);
        }

        public void Update(Lamp item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            db.Lamps.Update(mapper.Map<LampEF>(item));
        }

        //TODO: Wrap up all possible DB exception in GenericRepository into StorageException.
    }
}
