using Domain.Common;

namespace Domain.Entities
{
    public class Cart : BaseAuditableEntity
    {
        public string? Name { get; set; }

        public string? Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
