using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string? Name { get; set; }

        public string? Image { get; set; }

        public int ParentCategoryId { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = request.Name,
                Image = request.Image,
                ParentCategoryId = request.ParentCategoryId
            };

            _context.InitialDate(entity);
            _context.Categories.Insert(entity);

            return entity.Id;
        }
    }
}
