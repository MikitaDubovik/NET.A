using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CategoryCreateEvent : BaseEvent
    {
        public CategoryCreateEvent(Category item)
        {
            Item = item;
        }

        public Category Item { get; }
    }
}
