using FluentValidation;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Validation.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(150);
        }
    }
}
