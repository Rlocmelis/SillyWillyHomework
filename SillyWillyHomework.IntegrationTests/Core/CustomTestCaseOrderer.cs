using Xunit.Abstractions;
using Xunit.Sdk;

namespace SillyWillyHomework.IntegrationTests.Core
{
    public class CustomTestCaseOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(x => GetOrder(x));
        }

        private static int GetOrder(ITestCase testCase)
        {
            var testMethod = testCase.TestMethod.Method;
            var orderAttribute = testMethod.GetCustomAttributes(typeof(TestOrderAttribute)).FirstOrDefault();

            var orderValue = int.MaxValue;
            if (orderAttribute != null)
            {
                orderValue = orderAttribute.GetNamedArgument<int>("Order");
            }

            return orderValue;
        }
    }
}
