using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyWillyHomework.IntegrationTests.Core
{
    public class IntegrationTestAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

    }
}
