using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CartDeleteEvent : BaseEvent
    {
        public CartDeleteEvent(Cart item)
        {
            Item = item;
        }

        public Cart Item { get; }
    }
}
