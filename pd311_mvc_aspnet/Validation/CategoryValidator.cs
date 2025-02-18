using FluentValidation;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Обов'язкове поле")
                .MaximumLength(100).WithMessage("Максимальна довжина 100 символів");
        }
    }
}
