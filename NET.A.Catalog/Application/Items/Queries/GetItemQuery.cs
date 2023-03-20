using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Items.Queries
{
    public record GetItemQuery(int Id) : IRequest<ItemDto>;

    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var category = _context.Items.FindOne(c => c.Id == request.Id);
            if (category == null)
            {
                return null;
            }

            return _mapper.Map<ItemDto>(category);
        }
    }
}
