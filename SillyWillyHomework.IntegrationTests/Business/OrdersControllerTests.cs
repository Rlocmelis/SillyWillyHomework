using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Exceptions;
using SillyWillyHomework.IntegrationTests.Core;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services;
using SillyWillyHomework.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SillyWillyHomework.IntegrationTests.Business
{
    [Collection("TestCollection")]
    [TestCaseOrderer("SillyWillyHomework.IntegrationTests.Core.PriorityOrderer", "SillyWillyHomework.IntegrationTests.Core")]
    public class OrdersControllerTests : BaseTest
    {
        public OrdersControllerTests(IntegrationTestAppFactory<Program> factory) : base(factory)
        {
        }

        [Fact, TestPriority(1)]
        public async Task PostOrder_ShouldReturnOrder_WhenValidOrder()
        {
            var dataAccess = _scope.ServiceProvider.GetRequiredService<IOrdersService>();

            OrderRequest orderRequest = new OrderRequest()
            {
                CustomerId = 2,
                ExpectedDeliveryDate = DateTime.Now.AddDays(1),
                Items = new List<Models.Requests.OrderItemRequest> 
                { 
                    new Models.Requests.OrderItemRequest()
                    {
                        ProductId = 1,
                        Quantity = 10
                    }
                }
            };

            var preparedOrder = await dataAccess.PrepareOrder(orderRequest);

            await dataAccess.AddAsync(preparedOrder);

            var response = await _httpClient.GetAsync($"api/Orders/customer/{2}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseObject = await response.Content.ReadFromJsonAsync<OrderDto>();

            responseObject.Should().BeNull();
        }

        [Fact, TestPriority(3)]
        public async Task PostOrder_ShouldReturnCustomerNotExists_WhenNonExistingCustomer()
        {
            var dataAccess = _scope.ServiceProvider.GetRequiredService<IOrdersService>();

            OrderRequest orderRequest = new OrderRequest()
            {
                CustomerId = 3,
                ExpectedDeliveryDate = DateTime.Now.AddDays(1),
                Items = new List<Models.Requests.OrderItemRequest>
                {
                    new Models.Requests.OrderItemRequest()
                    {
                        ProductId = 1,
                        Quantity = 10
                    }
                }
            };

            var preparedOrder = await dataAccess.PrepareOrder(orderRequest);

            preparedOrder.Should().BeNull();
        }

    }
}
