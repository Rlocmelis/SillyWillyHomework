using FluentValidation;
using SillyWillyHomework.Models.Requests;

namespace SillyWillyHomework.Validation.Validators
{
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
