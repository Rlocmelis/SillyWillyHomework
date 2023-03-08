using Xunit.Abstractions;
using Xunit.Sdk;

namespace SillyWillyHomework.IntegrationTests.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestOrderAttribute : Attribute
    {
        public int Order { get; }

        public TestOrderAttribute(int order)
        {
            Order = order;
        }
    }
}