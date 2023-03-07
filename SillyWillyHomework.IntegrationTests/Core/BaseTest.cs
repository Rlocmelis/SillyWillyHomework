﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyWillyHomework.IntegrationTests.Core
{
    public class BaseTest : IClassFixture<IntegrationTestAppFactory<Program>>, IDisposable
    {
        protected readonly IntegrationTestAppFactory<Program> _factory;
        protected readonly HttpClient _httpClient;
        protected readonly IServiceScope _scope;

        public BaseTest(IntegrationTestAppFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            _scope = _factory.Services.CreateScope();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _scope.Dispose();
        }
    }

}
