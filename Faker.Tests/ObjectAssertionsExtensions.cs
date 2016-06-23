using System.Net;
using FluentAssertions;

namespace Faker.Tests
{
    public static class ObjectAssertionsExtensions
    {
        public static void ShouldNotMatchAnyRule(this ResponseAndResult actual)
        {
            actual.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actual.Result.Should().Be("FAKER did not find any valid response.");
        }
    }
}