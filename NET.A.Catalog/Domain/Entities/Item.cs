using Domain.Common;

namespace Domain.Entities
{
    public class Item : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}
