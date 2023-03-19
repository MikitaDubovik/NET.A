using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CartDeletedEvent : BaseEvent
    {
        public CartDeletedEvent(Cart item)
        {
            Item = item;
        }

        public Cart Item { get; }
    }
}
