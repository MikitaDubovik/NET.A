using Domain.Common;
using Domain.Entities;
using LiteDB;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        ILiteCollection<Category> Categories { get; }

        ILiteCollection<Item> Items { get; }

        void InitialDate(BaseAuditableEntity entry);

        void UpdateDate(BaseAuditableEntity entry);
    }
}
