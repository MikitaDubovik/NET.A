using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Carts.Commands.DeleteCart
{
    public record DeleteCartCommand(int Id) : IRequest;

    public class DeleteCartCommandHandler: IRequestHandler<DeleteCartCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Carts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Cart), request.Id);
            }

            _context.Carts.Remove(entity);

            entity.AddDomainEvent(new CartDeleteEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
