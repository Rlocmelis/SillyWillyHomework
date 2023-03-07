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
    [TestCaseOrderer("SillyWillyHomework.IntegrationTests.Core.PriorityOrderer", "SillyWillyHomework.IntegrationTests.Core")]
    public class CustomerControllerTests : BaseTest
    {
        public CustomerControllerTests(IntegrationTestAppFactory<Program> factory) : base(factory)
        {
        }

        [Fact, TestPriority(1)]
        public async Task GetCustomer_ShouldReturnCustomer_WhenCustomerExists()
        {
            var dataAccess = _scope.ServiceProvider.GetRequiredService<IBaseService<Customer, CustomerDto>>();

            var testCustomer = await dataAccess.AddAsync(new CustomerDto
            {
                Name = "Integration Test customer",
            });

            var expectedCustomer = new Customer()
            {
                Id = 2,
                Name = "Integration Test customer",
            };

            var response = await _httpClient.GetAsync($"api/Customers/{testCustomer.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseObject = await response.Content.ReadFromJsonAsync<Customer>();

            responseObject.Should().BeEquivalentTo(expectedCustomer);
        }

        [Fact, TestPriority(2)]
        public async Task GetCustomer_ShouldReturnCustomer()
        {
            var dataAccess = _scope.ServiceProvider.GetRequiredService<IBaseService<Customer, CustomerDto>>();

            var expectedCustomer = new Customer()
            {
                Id = 2,
                Name = "Integration Test customer",
            };

            var response = await _httpClient.GetAsync($"api/Customers/{expectedCustomer.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseObject = await response.Content.ReadFromJsonAsync<Customer>();

            responseObject.Should().BeEquivalentTo(expectedCustomer);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
