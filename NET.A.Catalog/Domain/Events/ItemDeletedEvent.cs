using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ItemDeletedEvent :BaseEvent
    {
        public Item item { get; set; }

        public ItemDeletedEvent(Item item)
        {
            this.item = item;
        }
    }
}
