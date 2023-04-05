using Application.Common.Models;
using Application.Items.Commands.CreateItem;
using Application.Items.Commands.DeleteItem;
using Application.Items.Commands.UpdateItem;
using Application.Items.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Formatting;

namespace NET.A.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ApiControllerBase
    {
        //get/list/add/update/delete

        [HttpGet]
        public async Task<ActionResult<HttpResponseMessage>> GetItems([FromQuery] GetItemsQuery query)
        {
            var items = await Mediator.Send(query);

            var itemLinks = new List<Link>();
            foreach (var item in items.Items)
            {
                itemLinks.Add(new Link()
                {
                    Href = Url.Link("item", new { id = item.Id }),
                    Rel = "self",
                    Method = HttpMethod.Get.ToString()
                });
            }

            return CreateResponse(items, itemLinks);
        }

        [HttpGet]
        [Route("item/{id}", Name = "item")]
        public async Task<HttpResponseMessage> GetItem(int id)
        {
            var item = await Mediator.Send(new GetItemQuery(id));
            var itemLinks = CreateLinksForItem(id);
            return CreateResponse(item, itemLinks);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("item/{id}", Name = "update-item")]
        public async Task<ActionResult> Update(UpdateItemCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("item/{id}", Name = "delete-item")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteItemCommand(id));

            return NoContent();
        }


        private HttpResponseMessage CreateResponse<T>(T body, List<Link> links)
        {
            // Create a response with the customer data and hypermedia links
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<T>(body, new JsonMediaTypeFormatter())
            };
            // Convert the list of Link objects to a list of strings
            var linkHeaderValues = links.Select(l => $"<{l.Href}>; rel=\"{l.Rel}\"; method=\"{l.Method}\"");

            // Add the link header values to the response headers
            foreach (var value in linkHeaderValues)
            {
                response.Headers.Add("Link", value);
            }
            return response;
        }

        private List<Link> CreateLinksForItem(int id)
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

            return links;
        }
    }
}
