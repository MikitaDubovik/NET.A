using Application.Carts.Commands.CreateCart;
using Application.Carts.Commands.DeleteCart;
using Application.Carts.Queries;
using Application.Items.Commands.AddItem;
using Application.Items.Commands.RemoveItem;
using Application.Items.Queries;
using Microsoft.AspNetCore.Mvc;

namespace NET.A.Carting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ApiControllerBase
    {
        //Get list of items of the cart object.
        //Add item to cart.
        //Remove item from the cart.

        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> GetItemsByCartId([FromQuery] GetItemsByCartIdQuery query)
        {
            return await Mediator.Send(query);
        }


        [HttpPost]
        public async Task<ActionResult<int>> AddItem(AddItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveItem(int id)
        {
            await Mediator.Send(new RemoveItemCommand(id));

            return NoContent();
        }
    }
}
