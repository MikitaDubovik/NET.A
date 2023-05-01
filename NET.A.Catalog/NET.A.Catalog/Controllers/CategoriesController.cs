using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NET.A.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ApiControllerBase
    {
        //get/list/add/update/delete

        [Authorize(Roles = "Manager, Buyer")]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories([FromQuery] GetCategoriesQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize(Roles = "Manager, Buyer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            return await Mediator.Send(new GetCategoryQuery(id));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand(id));

            return NoContent();
        }
    }
}
