using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Items.Queries
{
    public record GetItemsQuery : IRequest<PaginatedList<ItemDto>>
    {
        public int CategoryId { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, PaginatedList<ItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            return _context.Items.Find(x => x.CategoryId == request.CategoryId)
                                 .AsQueryable()
                                 .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                                 .PaginatedList(request.PageNumber, request.PageSize);
        }
    }
}
