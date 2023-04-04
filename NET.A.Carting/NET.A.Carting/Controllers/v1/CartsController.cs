using Application.Carts.Queries;
using Application.Items.Commands.AddItem;
using Application.Items.Commands.RemoveItem;
using Application.Items.Queries;
using Microsoft.AspNetCore.Mvc;

namespace NET.A.Carting.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CartsController : ApiControllerBase
    {
        //Get list of items of the cart object.
        //Add item to cart.
        //Remove item from the cart.

        [HttpGet]
        [Route("items")]
        public async Task<ActionResult<List<ItemDto>>> GetItemsByCartId([FromQuery] GetItemsByCartIdQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("carts-with-items")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CartWithItemsDto>> GetCartWithItems([FromQuery] GetCartWithItemsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddItem(AddItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveItem(int cartId, int itemId)
        {
            await Mediator.Send(new RemoveItemCommand(cartId, itemId));

            return NoContent();
        }
    }
}
