using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Carts.Queries
{
    public record GetCartsQuery : IRequest<List<CartDto>>
    {
    }

    public class GetCartsQueryHandler: IRequestHandler<GetCartsQuery, List<CartDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCartsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CartDto>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Carts.ProjectTo<CartDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
