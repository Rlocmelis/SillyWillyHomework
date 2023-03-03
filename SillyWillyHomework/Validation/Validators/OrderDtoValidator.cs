using FluentValidation;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Validation.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(order => order.CustomerId).NotEqual(0);
        }
    }
}
