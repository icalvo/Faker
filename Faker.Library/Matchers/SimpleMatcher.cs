using System.Linq;
using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal class SimpleMatcher : IMatcher
    {
        public EndpointMatch Match(IOwinRequest request, Endpoint endpoint)
        {
            var requestPath = request.Path + request.QueryString;

            if (
                requestPath == "/" + endpoint.Url.TrimStart('/') &&
                (endpoint.Method == request.Method ||
                 endpoint.Methods.Contains(request.Method)))
            {
                return new EndpointMatch(requestPath, endpoint);
            }

            return null;
        }
    }
}