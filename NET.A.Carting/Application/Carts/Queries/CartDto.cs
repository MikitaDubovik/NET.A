using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Carts.Queries
{
    public class CartDto : IMapFrom<Cart>
    {
        public string? Name { get; set; }

        public string? Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
