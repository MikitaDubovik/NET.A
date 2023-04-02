using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Categories.Queries
{
    public class CategoryDto : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string? Image { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
