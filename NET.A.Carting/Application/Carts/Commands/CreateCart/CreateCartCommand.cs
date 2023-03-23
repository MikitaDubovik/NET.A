using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Carts.Commands.CreateCart
{
    public record CreateCartCommand : IRequest<int>;

    public class CreateCardCommandHandler : IRequestHandler<CreateCartCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCardCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var entity = new Cart();

            entity.AddDomainEvent(new CartCreateEvent(entity));

            _context.Carts.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
