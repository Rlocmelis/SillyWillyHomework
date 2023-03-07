using FluentValidation;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Validation.Validators
{
    public class CustomerRequestValidator : AbstractValidator<CustomerDto>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(150);
        }
    }
}
