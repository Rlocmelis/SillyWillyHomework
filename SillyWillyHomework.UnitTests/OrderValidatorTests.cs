using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using SillyWillyHomework.Business.Discounts;
using SillyWillyHomework.Models;
using SillyWillyHomework.Models.Requests;
using SillyWillyHomework.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyWillyHomework.UnitTests
{
    public class OrderValidatorTests
    {
        private Mock<OrderRequestValidator> orderRequestValidator;
        private Mock<OrderItemRequestValidator> orderRequestItemValidator;

        public OrderValidatorTests()
        {
            orderRequestValidator = new Mock<OrderRequestValidator>();
            orderRequestItemValidator = new Mock<OrderItemRequestValidator>();
        }

        [Fact]
        public void ValidateOrderRequestDto_ShouldBeInvalid_WhenDeliveryDateNotInFuture()
        {
            // Arrange
            var orderRequestValidator = InitializeOrderRequestValidator();
            var orderRequest = InitializeOrderRequestData(DateTime.Now.Date.AddDays(-1));

            var result = orderRequestValidator.Validate(orderRequest);

            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void ValidateOrderRequestDto_ShouldBeValid_WhenDeliveryDateNotInFuture()
        {
            // Arrange
            var orderRequestValidator = InitializeOrderRequestValidator();
            var orderRequest = InitializeOrderRequestData(DateTime.Now.Date.AddDays(1));

            var result = orderRequestValidator.Validate(orderRequest);

            result.IsValid.Should().BeTrue();
        }


        #region
        private OrderRequestValidator InitializeOrderRequestValidator()
        {
            orderRequestValidator.Setup(v => v.Validate(It.IsAny<ValidationContext<OrderRequest>>()))
                            .Returns(new ValidationResult());

            var initializedOrderRequestValidator = orderRequestValidator.Object;

            return initializedOrderRequestValidator;
        }

        private OrderItemRequestValidator InitializeOrderRequestItemValidator()
        {
            orderRequestItemValidator.Setup(v => v.Validate(It.IsAny<ValidationContext<OrderItemRequest>>()))
                            .Returns(new ValidationResult());

            var initializedOrderRequestItemValidator = orderRequestItemValidator.Object;

            return initializedOrderRequestItemValidator;
        }

        private static OrderRequest InitializeOrderRequestData(DateTime expectedDeliveryDate)
        {
            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ExpectedDeliveryDate = expectedDeliveryDate,
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 100,
                        Quantity = 2
                    },
                    new OrderItemRequest
                    {
                        ProductId = 200,
                        Quantity = 1
                    }
                }
            };

            return orderRequest;
        }

        #endregion
    }
}
