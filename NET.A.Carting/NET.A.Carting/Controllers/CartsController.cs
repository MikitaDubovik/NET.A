using Application.Carts.Commands.CreateCart;
using Application.Carts.Commands.DeleteCart;
using Application.Carts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace NET.A.Carting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ApiControllerBase
    {
        //Add, remove, list

        [HttpGet]
        public async Task<ActionResult<List<CartDto>>> GetCarts([FromQuery] GetCartsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCartCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCartCommand(id));

            return NoContent();
        }
    }
}
