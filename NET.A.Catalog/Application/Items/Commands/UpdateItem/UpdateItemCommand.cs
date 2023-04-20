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

        public string? Image { get; set; }

        public double Price { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMessageProducer _messageProducer;

        public UpdateItemCommandHandler(IApplicationDbContext context, IMessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
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

            _messageProducer.SendMessage(request);

            return Unit.Value;
        }
    }
}
