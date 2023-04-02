using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CartCreateEvent : BaseEvent
    {
        public CartCreateEvent(Cart item)
        {
            Item = item;
        }

        public Cart Item { get; }
    }
}
