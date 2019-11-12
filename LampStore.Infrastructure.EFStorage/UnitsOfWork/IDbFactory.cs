using Microsoft.EntityFrameworkCore;


namespace LampStore.Infrastructure.EFStorage.UnitsOfWork
{
    /// <summary>
    /// This abstraction is used here to maintain the ability to supersede DbContext.
    /// </summary>
    public interface IDbFactory<TDbContext>
        where TDbContext: DbContext
    {
        TDbContext Get();
    }
}
