using Application.Items.Queries;

namespace Application.Carts.Queries
{
    public class CartWithItemsDto
    {
        public CartDto Cart { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
