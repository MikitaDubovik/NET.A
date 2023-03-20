using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;
public class ApplicationDbContextInitialiser : IApplicationDbContextInitialiser
{
    private readonly ILogger _logger;
    private readonly IApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void Seed()
    {
        try
        {
            TrySeed();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public void TrySeed()
    {
        // Default data
        // Seed, if necessary
        Category category = null;
        if (_context.Categories.Count() == 0)
        {
            var ent1 = new Category
            {
                Name = "Name1",
                Image = "Image1",
            };

            _context.InitialDate(ent1);
            _context.Categories.Insert(ent1);
            category = ent1;
            var ent2 = new Category
            {
                Name = "Name2",
                Image = "Image2",
                ParentCategoryId = 1,
            };

            _context.InitialDate(ent2);
            _context.Categories.Insert(ent2);
        }

        if (_context.Items.Count() == 0)
        {
            var ent1 = new Item
            {
                Name = "Name1",
                Description = "Description1",
                Image = "Image1",
                Price = 1,
                Amount = 1,
                Category = category
            };

            _context.InitialDate(ent1);
            _context.Items.Insert(ent1);

            var ent2 = new Item
            {
                Name = "Name2",
                Description = "Description2",
                Image = "Image2",
                Price = 2,
                Amount = 2,
                Category = category
            };

            _context.InitialDate(ent2);
            _context.Items.Insert(ent2);
        }
    }
}
