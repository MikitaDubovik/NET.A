using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Categories.Queries
{
    public record GetCategoryQuery(int Id) : IRequest<CategoryDto>;

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _context.Categories.FindOne(c => c.Id == request.Id);
            if (category == null)
            {
                return null;
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
