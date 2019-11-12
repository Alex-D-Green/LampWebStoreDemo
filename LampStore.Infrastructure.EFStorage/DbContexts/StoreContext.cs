using LampStore.AppCore.Core.Entities;
using LampStore.Infrastructure.EFStorage.Entities;

using Microsoft.EntityFrameworkCore;


namespace LampStore.Infrastructure.EFStorage.DbContexts
{
    /// <summary>
    /// DB context to interact with DB through EF Core.
    /// </summary>
    /// <remarks>Actually EF DbContext implements UnitOfWork and Repository patterns already, 
    /// but in case if we want to uncouple EF itself...
    /// </remarks>
    internal sealed class StoreContext: DbContext
    {
        public DbSet<LampEF> Lamps { get; set; }
        
        public DbSet<ComparisonEF> Comparisons { get; set; }

        public DbSet<ComparisonPairEF> ComparisonPairs { get; set; }


        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Add logging for the DB context to see SQL commands in Debug output
            //optionsBuilder.UseLoggerFactory(new LoggerFactory().AddDebug())
            //              .EnableSensitiveDataLogging();

            //TODO: Replace "in code" logger factory in StoreContext by using DI.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComparisonEF>().HasMany(o => o.Comparisons)
                                               .WithOne()
                                               .OnDelete(DeleteBehavior.Cascade);

            #region The initial filling of DB

            int lampId = 0;

            modelBuilder.Entity<LampEF>().HasData(
                new LampEF {
                    Id = ++lampId,
                    LampType = LampType.LED,
                    Manufacturer = "Philips",
                    Cost = 16,
                    ImageRef = "https://avatars.mds.yandex.net/get-mpic/1767083/img_id4205890578895788084.jpeg/9hq"
                },

                new LampEF {
                    Id = ++lampId,
                    LampType = LampType.Fluorescent,
                    Manufacturer = "OSRAM",
                    Cost = 2,
                    ImageRef = "https://avatars.mds.yandex.net/get-mpic/1045304/img_id688239816713013197.jpeg/9hq"
                },

                new LampEF {
                    Id = ++lampId,
                    LampType = LampType.LED,
                    Manufacturer = "OSRAM",
                    Cost = 4.5,
                    ImageRef = "https://avatars.mds.yandex.net/get-mpic/1045304/img_id5760657839941547295.jpeg/9hq"
                },

                new LampEF {
                    Id = ++lampId,
                    LampType = LampType.Fluorescent,
                    Manufacturer = "Camelion",
                    Cost = 2.4,
                    ImageRef = "https://avatars.mds.yandex.net/get-mpic/1045304/img_id4656031640183289008.jpeg/9hq"
                },

                new LampEF {
                    Id = ++lampId,
                    LampType = LampType.LED,
                    Manufacturer = "Gauss",
                    Cost = 2.7,
                    ImageRef = "https://avatars.mds.yandex.net/get-mpic/1045304/img_id4827177099789801154.jpeg/9hq"
                },

                new LampEF {
                    Id = ++lampId,
                    LampType = LampType.Incandescent,
                    Manufacturer = "Philips",
                    Cost = 0.5,
                    ImageRef = "https://avatars.mds.yandex.net/get-mpic/1045304/img_id2657193580316822485.jpeg/9hq"
                }
            );

            #endregion
        }
    }
}
