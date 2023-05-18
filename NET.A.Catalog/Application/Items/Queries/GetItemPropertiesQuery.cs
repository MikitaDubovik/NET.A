using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Items.Queries
{
    public record GetItemPropertiesQuery(int Id) : IRequest<ItemPropertiesDto>;

    public class GetItemPropertiesQueryHandler : IRequestHandler<GetItemPropertiesQuery, ItemPropertiesDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemPropertiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemPropertiesDto> Handle(GetItemPropertiesQuery request, CancellationToken cancellationToken)
        {
            var category = _context.Items.FindOne(c => c.Id == request.Id);
            if (category == null)
            {
                return null;
            }

            return _mapper.Map<ItemPropertiesDto>(category);
        }
    }
}
