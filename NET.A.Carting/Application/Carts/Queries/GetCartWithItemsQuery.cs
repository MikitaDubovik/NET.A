using Application.Common.Interfaces;
using Application.Items.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Carts.Queries
{
    public class GetCartWithItemsQuery : IRequest<CartWithItemsDto>
    {
        public int CartId { get; set; }
    }

    public class GetCartWithItemsQueryHandler : IRequestHandler<GetCartWithItemsQuery, CartWithItemsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCartWithItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartWithItemsDto> Handle(GetCartWithItemsQuery request, CancellationToken cancellationToken)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == request.CartId);
            if (cart == null)
            {
                throw new ArgumentException(nameof(cart), "The cart with provided ID does not exist.");
            }

            var items = await _context.Items.Where(i => i.CartId == request.CartId)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);

            var cartDto = _mapper.Map<CartDto>(cart);
            cartDto.Quantity = items.Count;
            var cartWithItems = new CartWithItemsDto
            {
                Cart = cartDto,
                Items = items
            };

            return cartWithItems;
        }
    }
}
