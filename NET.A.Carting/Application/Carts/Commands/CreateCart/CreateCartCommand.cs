using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Carts.Commands.CreateCart
{
    public class CreateCartCommand : IRequest<int>
    {
        public string? Name { get; set; }

        public string? Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

    }

    public class CreateCardCommandHandler: IRequestHandler<CreateCartCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCardCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var entity = new Cart
            {
                Name = request.Name,
                Image = request.Image,
                Price = request.Price,
                Quantity = request.Quantity
            };

            entity.AddDomainEvent(new CartCreateEvent(entity));

            _context.Carts.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
