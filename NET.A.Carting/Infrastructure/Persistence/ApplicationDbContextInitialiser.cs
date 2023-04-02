using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Carts.Any())
        {
            _context.Carts.Add(new Cart
            {
            });

            _context.Carts.Add(new Cart
            {
            });

            await _context.SaveChangesAsync();
        }

        if (!_context.Items.Any())
        {
            _context.Items.Add(new Item
            {
                Name = "Item1",
                Image = "Image1",
                Price = 1,
                CartId = 1,
            });

            _context.Items.Add(new Item
            {
                Name = "Item2",
                Image = "Image2",
                Price = 10,
                CartId = 2,
            });

            await _context.SaveChangesAsync();
        }
    }
}
