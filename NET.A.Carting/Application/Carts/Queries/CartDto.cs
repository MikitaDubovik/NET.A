using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Carts.Queries
{
    public class CartDto : IMapFrom<Cart>
    {
        public int Quantity { get; set; }
    }
}
