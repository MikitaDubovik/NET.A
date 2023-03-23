﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Items.Commands.RemoveItem
{
    public record RemoveItemCommand(int Id) : IRequest;

    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Items
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }

            _context.Items.Remove(entity);

            entity.AddDomainEvent(new ItemRemoveEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
