using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ItemRemoveEvent : BaseEvent
    {
        public ItemRemoveEvent(Item item)
        {
            Item = item;
        }
        public Item Item { get; set; }
    }
}
