using FluentValidation;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(e => e.Name).MaximumLength(50).NotEmpty();
        }
    }
}
