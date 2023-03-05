using FluentValidation;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Validation.Validators
{
    public class OrderRequestDtoValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderRequestDtoValidator()
        {
            RuleFor(order => order.CustomerId).NotEqual(0);
            RuleFor(order => order.ExpectedDeliveryDate).GreaterThan(DateTime.Today);
        }
    }
}
