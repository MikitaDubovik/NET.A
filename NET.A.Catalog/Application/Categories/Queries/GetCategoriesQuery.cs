using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Categories.Queries
{
    public record GetCategoriesQuery : IRequest<List<CategoryDto>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _context.Categories.Query().ToList();
            return Task.Run(() => _mapper.Map<List<CategoryDto>>(categories));
        }
    }
}
