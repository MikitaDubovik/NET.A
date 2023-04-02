using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Items.Commands.AddItem
{
    public class AddItemCommand : IRequest<int>
    {
        public string? Name { get; set; }

        public string? Image { get; set; }

        public double Price { get; set; }

        public int CartId { get; set; }
    }

    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public AddItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddItemCommand command, CancellationToken cancellationToken)
        {
            var entity = new Item
            {
                Name = command.Name,
                Image = command.Image,
                Price = command.Price,
                CartId = command.CartId
            };

            entity.AddDomainEvent(new ItemAddEvent(entity));

            _context.Items.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
