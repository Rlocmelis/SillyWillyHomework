using FluentValidation;
using SillyWillyHomework.Models.OrderRequests;

namespace SillyWillyHomework.Validation.Validators
{
    public class OrderRequestItemDtoValidator : AbstractValidator<OrderRequestItemDto>
    {
        private readonly int _dnaTestingKitProductId = 1;

        public OrderRequestItemDtoValidator()
        {
            RuleFor(Item => Item.ProductId).Equal(_dnaTestingKitProductId)
                .WithMessage("Orders with DNA testing kits are only allowed currently.");
            RuleFor(Item => Item.Quantity).NotNull();
            RuleFor(Item => Item.Quantity).GreaterThan(0);
            RuleFor(Item => Item.Quantity).LessThan(999);
        }
    }
}
