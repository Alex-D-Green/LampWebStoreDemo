using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Exceptions;
using LampStore.AppCore.Core.Interfaces;
using LampStore.AppCore.Core.Utilities;
using LampStore.Infrastructure.EFStorage.DbContexts;
using LampStore.Infrastructure.EFStorage.Entities;

using Microsoft.EntityFrameworkCore;


namespace LampStore.Infrastructure.EFStorage.Repositories
{
    internal sealed class ComparisonsRepository: IRepository<Comparison, int>
    {
        private readonly StoreContext db;
        private readonly IMapper mapper;


        public ComparisonsRepository(StoreContext db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IEnumerable<Comparison>> GetAsync(int? from = null, int? count = null,
            string sortingBy = null, SortDirection desc = SortDirection.Ascending)
        {
            if(from < 0)
                throw new ArgumentOutOfRangeException(nameof(from));

            if(count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));


            IQueryable<ComparisonEF> query = db.Comparisons.Include(o => o.FirstLamp)
                                                           .Include(o => o.SecondLamp)
                                                           .Include(o => o.Comparisons)
                                                           .AsNoTracking();

            if(sortingBy != null)
            {
                //Here probably needed to do something more "elegant"...

                Expression<Func<ComparisonEF, object>> expr = null;

                switch(sortingBy)
                {
                    case nameof(Comparison.Id):
                        expr = o => o.Id;
                        break;
                    case nameof(Comparison.FirstLamp):
                        expr = o => o.FirstLamp.Id;
                        break;
                    case nameof(Comparison.SecondLamp):
                        expr = o => o.SecondLamp.Id;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(sortingBy));
                }

                query = desc == SortDirection.Ascending ? query.OrderBy(expr) : query.OrderByDescending(expr);
            }

            if(from.HasValue)
                query = query.Skip(from.Value);

            if(count.HasValue)
                query = query.Take(count.Value);

            return mapper.Map<IEnumerable<Comparison>>(await query.ToArrayAsync());
        }

        public async Task<Comparison> GetByIdAsync(int id)
        {
            ComparisonEF ret = await db.Comparisons.Include(o => o.FirstLamp)
                                                   .Include(o => o.SecondLamp)
                                                   .Include(o => o.Comparisons)
                                                   .AsNoTracking()
                                                   .FirstOrDefaultAsync(o => o.Id == id);

            if(ret == null)
                return null;

            return mapper.Map<Comparison>(ret);
        }

        public async Task<IdHolder<int>> AddAsync(Comparison item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            var comp = mapper.Map<ComparisonEF>(item);

            await db.Comparisons.AddAsync(comp);

            if(comp.FirstLamp.Id != 0)
                db.Entry(comp.FirstLamp).State = EntityState.Unchanged;
            
            if(comp.SecondLamp.Id != 0)
                db.Entry(comp.SecondLamp).State = EntityState.Unchanged;

            return new IdHolder<int>(comp, nameof(ComparisonEF.Id));
        }

        public void Delete(Comparison item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            db.Comparisons.Remove(mapper.Map<ComparisonEF>(item));
        }

        public async Task DeleteByIdAsync(int id)
        {
            ComparisonEF item = await db.Comparisons.FindAsync(id) ?? 
                throw new ItemNotFoundStorageException($"Item with id = {id} not found.");

            db.Comparisons.Remove(item);
        }

        public void Update(Comparison item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            db.Comparisons.Update(mapper.Map<ComparisonEF>(item));
        }

        //TODO: Wrap up all possible DB exception in ComparisonsRepository into StorageException.
    }
}
