using Application.Carts.Queries;
using Application.Items.Queries;
using Microsoft.AspNetCore.Mvc;

namespace NET.A.Carting.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CartsController : ApiControllerBase
    {
        [Route("carts-with-items")]
        [MapToApiVersion("2.0")]
        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> GetCartWithItems([FromQuery] GetItemsByCartIdQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
