using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SillyWillyHomework.IntegrationTests.Core;

namespace SillyWillyHomework.IntegrationTests.Business
{
    [Collection("TestCollection")]
    public class CustomerControllerTests : BaseTest
    {
        public CustomerControllerTests(IntegrationTestAppFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        [TestOrder(1)]
        public async Task GetCustomer_ShouldReturnCustomer_WhenCustomerExists()
        {
            var dataAccess = _scope.ServiceProvider.GetRequiredService<IBaseService<Customer, CustomerDto>>();

            var testCustomer = await dataAccess.AddAsync(new CustomerDto
            {
                Name = "Integration Test customer",
            });

            var response = await _httpClient.GetAsync($"api/Customers/{testCustomer.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [TestOrder(2)]
        public async Task GetCustomer_ShouldReturn404_WhenCustomerDoesNotExist()
        {
            var dataAccess = _scope.ServiceProvider.GetRequiredService<IBaseService<Customer, CustomerDto>>();

            var nonExistingCustomer = new Customer()
            {
                Id = 999,
                Name = "Integration Test customer",
            };

            var response = await _httpClient.GetAsync($"api/Customers/{nonExistingCustomer.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
