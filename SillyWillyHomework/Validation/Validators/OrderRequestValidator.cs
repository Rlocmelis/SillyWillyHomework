using FluentValidation;
using SillyWillyHomework.Models;
using SillyWillyHomework.Models.Requests;

namespace SillyWillyHomework.Validation.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(order => order.CustomerId).NotEqual(0);
            RuleFor(order => order.ExpectedDeliveryDate.Date).GreaterThan(DateTime.Today);
            RuleFor(order => order.Items).NotEmpty();
            RuleFor(order => order.Items).Must(x => x.Count == 1)
                .WithMessage("Currently, only 1 item is allowed per order.");

            RuleForEach(order => order.Items)
                .SetValidator(new OrderItemRequestValidator());
        }
    }

    public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
    {
        private readonly int _dnaTestingKitProductId = 1;

        public OrderItemRequestValidator()
        {
            RuleFor(Item => Item.ProductId).Equal(_dnaTestingKitProductId)
                .WithMessage("Currently, only orders for DNA testing kits are allowed.");
            RuleFor(Item => Item.Quantity).NotNull();
            RuleFor(Item => Item.Quantity).GreaterThan(0);
            RuleFor(Item => Item.Quantity).LessThan(999);
        }
    }
}
