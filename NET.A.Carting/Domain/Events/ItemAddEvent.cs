using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ItemAddEvent : BaseEvent
    {
        public ItemAddEvent(Item item)
        {
            Item = item;
        }
        public Item Item { get; set; }
    }
}
