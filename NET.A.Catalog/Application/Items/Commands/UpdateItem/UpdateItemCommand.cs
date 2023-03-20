using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommand : IRequest
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Items.FindOne(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            entity.Name = request.Name;
            _context.UpdateDate(entity);
            _context.Items.Update(entity);

            return Unit.Value;
        }
    }
}
