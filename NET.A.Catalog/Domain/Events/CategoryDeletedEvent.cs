using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CategoryDeletedEvent : BaseEvent
    {
        public CategoryDeletedEvent(Category item)
        {
            Item = item;
        }

        public Category Item { get; }
    }
}
