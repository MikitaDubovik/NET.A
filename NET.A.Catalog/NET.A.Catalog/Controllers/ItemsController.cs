using Application.Common.Models;
using Application.Items.Commands.CreateItem;
using Application.Items.Commands.DeleteItem;
using Application.Items.Commands.UpdateItem;
using Application.Items.Queries;
using Microsoft.AspNetCore.Mvc;

namespace NET.A.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ApiControllerBase
    {
        //get/list/add/update/delete

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ItemDto>>> GetItems([FromQuery] GetItemsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            return await Mediator.Send(new GetItemQuery(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateItemCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteItemCommand(id));

            return NoContent();
        }
    }
}
