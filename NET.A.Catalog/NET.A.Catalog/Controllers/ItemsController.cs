using Application.Common.Models;
using Application.Items.Commands.CreateItem;
using Application.Items.Commands.DeleteItem;
using Application.Items.Commands.UpdateItem;
using Application.Items.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace NET.A.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ApiControllerBase
    {
        //get/list/add/update/delete

        [Authorize(Roles = "Manager, Buyer")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ItemDto>>> GetItems([FromQuery] GetItemsQuery query)
        {
            var items = await Mediator.Send(query);

            foreach (var item in items.Items)
            {
                item.Links = CreateLinksForItem(item.Id);
            }

            return items;
        }

        [Authorize(Roles = "Manager, Buyer")]
        [HttpGet]
        [Route("item/{id}", Name = "item")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await Mediator.Send(new GetItemQuery(id));
            item.Links = CreateLinksForItem(id);
            return item;
        }

        [Authorize(Roles = "Manager, Buyer")]
        [HttpGet]
        [Route("item-properties/{id}", Name = "item-properties")]
        public async Task<ActionResult<ItemPropertiesDto>> GetItemProperties(int id)
        {
            return await Mediator.Send(new GetItemPropertiesQuery(id));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        [Route("item", Name = "update-item")]
        public async Task<ActionResult> Update(UpdateItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete]
        [Route("item/{id}", Name = "delete-item")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteItemCommand(id));

            return NoContent();
        }

        private IEnumerable<string> CreateLinksForItem(int id)
        {
            var links = new List<Link>
            {
                new Link()
                {
                    Href = Url.Link("item", new { id }),
                    Rel = "self",
                    Method = HttpMethod.Get.ToString()
                },

                new Link()
                {
                    Href = Url.Link("update-item", new { id }),
                    Rel = "self",
                    Method = HttpMethod.Put.ToString()
                },

                new Link()
                {
                    Href = Url.Link("delete-item", new { id }),
                    Rel = "self",
                    Method = HttpMethod.Delete.ToString()
                }
            };

            return links.Select(l => $"<{l.Href}>; rel=\"{l.Rel}\"; method=\"{l.Method}\"");
        }
    }
}
