using System.Linq;
using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal class SimpleMatcher : IMatcher
    {
        public RequestMatch Match(Endpoint endpoint, Request actual)
        {
            if (
                actual.Url == "/" + endpoint.Request.Url.TrimStart('/') &&
                (endpoint.Request.Method == actual.Method ||
                 endpoint.Request.Methods.Contains(actual.Method)))
            {
                return new RequestMatch(endpoint, actual);
            }

            return null;
        }
    }
}