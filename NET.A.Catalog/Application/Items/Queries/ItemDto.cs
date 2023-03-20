using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Items.Queries
{
    public class ItemDto : IMapFrom<Item>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}
