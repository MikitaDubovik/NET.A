using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries
{
    public class GetItemsByCartIdQuery : IRequest<List<ItemDto>>
    {
        public int CartId { get; set; }
    }

    public class GetItemsByCartIdHandler : IRequestHandler<GetItemsByCartIdQuery, List<ItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsByCartIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> Handle(GetItemsByCartIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items.Where(i=>i.CartId == request.CartId)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }

    }
}
