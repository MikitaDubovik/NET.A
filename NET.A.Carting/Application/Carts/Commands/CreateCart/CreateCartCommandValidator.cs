using FluentValidation;

namespace Application.Carts.Commands.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator() { 
            RuleFor(e => e.Name).MaximumLength(50).NotEmpty();
            RuleFor(e => e.Price).NotEmpty();
        }
    }
}
