using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using LiteDB;

namespace Infrastructure.Persistence;
public class ApplicationDbContext : IApplicationDbContext
{
    private readonly IAuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    private readonly LiteDatabase database;
    private readonly ILiteCollection<Category> _categories;
    private readonly ILiteCollection<Item> _items;

    private readonly string _path = Environment.CurrentDirectory + "\\DB.db";

    public ApplicationDbContext(IAuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

        database = new LiteDatabase(_path);

        _categories = database.GetCollection<Category>("categories");
        _items = database.GetCollection<Item>("items");
    }

    public ILiteCollection<Category> Categories => _categories;
    public ILiteCollection<Item> Items => _items;

    public void InitialDate(BaseAuditableEntity entry)
    {
        _auditableEntitySaveChangesInterceptor.InitialDate(entry);
    }

    public void UpdateDate(BaseAuditableEntity entry)
    {
        _auditableEntitySaveChangesInterceptor.UpdateDate(entry);
    }
}
