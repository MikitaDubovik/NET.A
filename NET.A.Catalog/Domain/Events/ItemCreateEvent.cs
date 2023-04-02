using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ItemCreateEvent : BaseEvent
    {
        public ItemCreateEvent(Item item)
        {
            Item = item;
        }

        public Item Item { get; set; }
    }
}
