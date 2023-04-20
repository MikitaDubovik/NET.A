using Application.Common.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public double Price { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == command.Id);
            if (item == null)
            {
                return -1;
            }

            item.Price = command.Price;
            item.Name = command.Name;
            item.Image = command.Image;

            item.AddDomainEvent(new ItemUpdateEvent(item));

            _context.Items.Update(item);

            await _context.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
