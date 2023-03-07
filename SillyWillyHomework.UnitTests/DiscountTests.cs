using FluentAssertions;
using Moq;
using SillyWillyHomework.Business.Discounts;

namespace SillyWillyHomework.UnitTests
{
    public class DiscountTests
    {
        private Mock<IDiscountCalculator> discountCalculator;
        private Mock<IProductDiscountService> productDiscountService;

        public DiscountTests()
        {
            discountCalculator = new Mock<IDiscountCalculator>();
            productDiscountService = new Mock<IProductDiscountService>();
        }

        [Theory]
        [InlineData(5, 0)]
        [InlineData(10, 0.05)]
        [InlineData(51, 0.15)]
        public void CalculateDiscount_ReturnsExpectedDiscount(int amount, decimal expectedDiscount)
        {
            // Arrange
            discountCalculator.Setup(ds => ds.CalculateDiscount(amount)).Returns(expectedDiscount);

            var initializedDiscountCalculator = discountCalculator.Object;

            // Act
            var actualDiscount = initializedDiscountCalculator.CalculateDiscount(amount);

            // Assert
            actualDiscount.Should().Be(expectedDiscount);
        }


        [Theory]
        [InlineData(1, 98.99, 989.90)]
        [InlineData(5, 98.99, 494.95)]
        public void TotalPrice_DoesNotApplyDiscount_WhenAmountIsUnder10(int amount, decimal basePrice, decimal expectedPrice)
        {
            // Arrange
            productDiscountService.Setup(pdc => pdc.CalculateTotalPrice(basePrice, amount)).Returns(expectedPrice);

            var initializedproductDiscountService = productDiscountService.Object;

            // Act
            var totalPrice = initializedproductDiscountService.CalculateTotalPrice(basePrice, amount);

            // Assert
            totalPrice.Should().Be(expectedPrice);
        }

        [Theory]
        [InlineData(10, 98.99, 989.90)]
        [InlineData(25, 98.99, 2474.75)]
        public void TotalPrice_AppliesDiscount_WhenAmountIsBetween10And50(int amount, decimal basePrice, decimal expectedPrice)
        {
            // Arrange
            productDiscountService.Setup(pdc => pdc.CalculateTotalPrice(basePrice, amount)).Returns(expectedPrice);

            var initializedproductDiscountService = productDiscountService.Object;

            // Act
            var totalPrice = initializedproductDiscountService.CalculateTotalPrice(basePrice, amount);

            // Assert
            totalPrice.Should().Be(expectedPrice);
        }

        [Theory]
        [InlineData(50, 98.99, 4949.50)]
        [InlineData(100, 98.99, 9899.00)]
        public void TotalPrice_AppliesDiscount_WhenAmountIsOver50(int amount, decimal basePrice, decimal expectedPrice)
        {
            // Arrange
            productDiscountService.Setup(pdc => pdc.CalculateTotalPrice(basePrice, amount)).Returns(expectedPrice);

            var initializedproductDiscountService = productDiscountService.Object;

            // Act
            var totalPrice = initializedproductDiscountService.CalculateTotalPrice(basePrice, amount);

            // Assert
            totalPrice.Should().Be(expectedPrice);
        }
    }
}