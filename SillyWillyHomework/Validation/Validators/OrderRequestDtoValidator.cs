using FluentValidation;
using SillyWillyHomework.Models;

namespace SillyWillyHomework.Validation.Validators
{
    public class OrderRequestDtoValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestDtoValidator()
        {
            RuleFor(order => order.CustomerId).NotEqual(0);
            RuleFor(order => order.ExpectedDeliveryDate.Date).GreaterThan(DateTime.Today.Date);
            RuleFor(order => order.Items).NotEmpty();
            RuleFor(order => order.Items).Must(x => x.Count == 1)
                .WithMessage("Currently, only 1 item is allowed per order.");
        }
    }
}
