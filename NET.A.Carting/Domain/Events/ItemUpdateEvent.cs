using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ItemUpdateEvent : BaseEvent
    {
        public ItemUpdateEvent(Item item)
        {
            Item = item;
        }
        public Item Item { get; set; }
    }
}
