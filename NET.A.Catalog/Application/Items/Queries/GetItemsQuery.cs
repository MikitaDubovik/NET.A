using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Items.Queries
{
    public record GetItemsQuery : IRequest<List<ItemDto>>;

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<ItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = _context.Items.Query().ToList();
            return Task.Run(() => _mapper.Map<List<ItemDto>>(items));
        }
    }
}
