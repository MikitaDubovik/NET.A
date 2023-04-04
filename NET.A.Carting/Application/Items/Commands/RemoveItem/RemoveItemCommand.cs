using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Items.Commands.RemoveItem
{
    public record RemoveItemCommand(int CartId, int ItemId) : IRequest;

    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == request.CartId);
            if (cart == null)
            {
                throw new ArgumentException(nameof(request.CartId), "The provided Cart ID does not exist.");
            }

            var entity = _context.Items.FirstOrDefault(i => i.Id == request.ItemId && i.CartId == request.CartId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), request.ItemId);
            }

            _context.Items.Remove(entity);

            entity.AddDomainEvent(new ItemRemoveEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
