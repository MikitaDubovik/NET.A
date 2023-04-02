using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Cart> Carts { get; }

        DbSet<Item> Items { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
