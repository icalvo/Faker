using System.Linq;
using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal class RegexMatcher : IMatcher
    {
        public RequestMatch Match(Endpoint endpoint, Request actual)
        {
            var regex = endpoint.Request.Url.Regex();
            var match = regex.Match(actual.Url);

            if (
                !match.Success ||
                (endpoint.Request.Method != actual.Method && !endpoint.Request.Methods.Contains(actual.Method)))
            {
                return null;
            }

            return new RequestMatch(endpoint, actual);
        }
    }
}