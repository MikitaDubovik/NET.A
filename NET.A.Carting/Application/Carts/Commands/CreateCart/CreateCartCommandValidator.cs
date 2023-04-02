using FluentValidation;

namespace Application.Carts.Commands.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator() { 
        }
    }
}
