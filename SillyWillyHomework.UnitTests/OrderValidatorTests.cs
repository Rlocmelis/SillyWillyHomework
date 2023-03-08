using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using SillyWillyHomework.Business.Discounts;
using SillyWillyHomework.Entities;
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
        private readonly OrderRequestValidator _validator;

        public OrderValidatorTests()
        {
            _validator = new OrderRequestValidator();
        }

        [Fact]
        public void Validate_ShouldBeInvalid_WhenCustomerIdIsZero()
        {
            // Arrange
            var order = new OrderRequest
            {
                CustomerId = 0,
                ExpectedDeliveryDate = DateTime.Today.AddDays(1),
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.Validate(order);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(OrderRequest.CustomerId));
        }

        [Fact]
        public void Validate_ShouldBeInvalid_WhenExpectedDeliveryDateIsToday()
        {
            // Arrange
            var order = new OrderRequest
            {
                CustomerId = 1,
                ExpectedDeliveryDate = DateTime.Today,
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.Validate(order);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors.Should().Contain(x => x.ErrorMessage.Contains("greater than"));
        }

        [Fact]
        public void Validate_ShouldBeInvalid_WhenExpectedDeliveryDateIsYesterday()
        {
            // Arrange
            var order = new OrderRequest
            {
                CustomerId = 1,
                ExpectedDeliveryDate = DateTime.Today.AddDays(-1),
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.Validate(order);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors.Should().Contain(x => x.ErrorMessage.Contains("greater than"));
        }

        [Fact]
        public void Validate_ShouldBeInvalid_WhenOrderRequestHasMoreThanOneItem()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ExpectedDeliveryDate = DateTime.Now.Date.AddDays(1),
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new OrderItemRequest
                    {
                        ProductId = 2,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.Validate(orderRequest);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Currently, only 1 item is allowed per order.");
        }

        [Fact]
        public void Validate_ShouldBeInvalid_WhenOrderRequestHasInvalidProductId()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ExpectedDeliveryDate = DateTime.Now.Date.AddDays(1),
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 2,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.Validate(orderRequest);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Currently, only orders for DNA testing kits are allowed.");
        }

        [Fact]
        public void Validate_ShouldBeInvalid_WhenOrderRequestHasInvalidProductQuantity()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ExpectedDeliveryDate = DateTime.Now.Date.AddDays(1),
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 1,
                        Quantity = 1000
                    }
                }
            };

            // Act
            var result = _validator.Validate(orderRequest);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage.Contains("must be less than"));
        }

        [Fact]
        public void Validate_ShouldBeValid_WhenOrderRequestIsValid()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ExpectedDeliveryDate = DateTime.Now.Date.AddDays(1),
                Items = new List<OrderItemRequest>
                {
                    new OrderItemRequest
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.Validate(orderRequest);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
