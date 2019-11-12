using System;

using LampStore.Infrastructure.EFStorage.UnitsOfWork;

using Microsoft.EntityFrameworkCore;


namespace DAL.EF
{
    /// <summary>
    /// A generic database factory.
    /// </summary>
    internal sealed class DbFactory<TDbContext>: IDbFactory<TDbContext>
        where TDbContext: DbContext
    {
        private readonly TDbContext db;


        public DbFactory(TDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public TDbContext Get()
        {
            return db;
        }
    }
}
