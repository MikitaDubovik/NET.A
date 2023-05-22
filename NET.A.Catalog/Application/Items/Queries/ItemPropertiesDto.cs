using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Items.Queries
{
    public class ItemPropertiesDto : IMapFrom<Item>
    {
        public Dictionary<string, string>? Properties { get; set; }
    }
}
