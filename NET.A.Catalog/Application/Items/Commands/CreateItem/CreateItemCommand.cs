using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Items.Commands.CreateItem
{
    public class CreateItemCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var category = _context.Categories.FindOne(x => x.Id == request.CategoryId);
            var entity = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                Price = request.Price,
                Amount = request.Amount,
                Category = category,
            };

            _context.InitialDate(entity);
            _context.Items.Insert(entity);

            return entity.Id;
        }
    }
}
