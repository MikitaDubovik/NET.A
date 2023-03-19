using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Cart> Carts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
