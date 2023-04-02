using Domain.Common;

namespace Domain.Entities
{
    public class Cart : BaseAuditableEntity
    {
        public List<Item> Items { get; set; }
    }
}
