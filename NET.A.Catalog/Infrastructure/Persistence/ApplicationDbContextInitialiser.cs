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
        if (!_context.Categories.Any())
        {
            _context.Categories.Add(new Category
            {
                Name = "Name1",
                Image = "Image1",
            });

            _context.Categories.Add(new Category
            {
                Name = "Name2",
                Image = "Image2",
                ParentCategoryId = 1,
            });

            await _context.SaveChangesAsync();
        }

        if (!_context.Items.Any())
        {
            _context.Items.Add(new Item
            {
                Name = "Name1",
                Description = "Description1",
                Image = "Image1",
                CategoryId = 1,
                Price = 1,
                Amount = 1
            });

            _context.Items.Add(new Item
            {
                Name = "Name2",
                Description = "Description2",
                Image = "Image2",
                CategoryId = 2,
                Price = 2,
                Amount = 2
            });

            await _context.SaveChangesAsync();
        }
    }
}
