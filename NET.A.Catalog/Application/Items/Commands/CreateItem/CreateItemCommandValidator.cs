using FluentValidation;

namespace Application.Items.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(e => e.Name).MaximumLength(50).NotEmpty();
            RuleFor(e => e.CategoryId).NotEmpty();
            RuleFor(e => e.Price).GreaterThanOrEqualTo(0).NotEmpty();
            RuleFor(e => e.Amount).GreaterThanOrEqualTo(0).NotEmpty();
        }
    }
}
