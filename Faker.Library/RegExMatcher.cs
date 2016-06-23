using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Owin;

namespace Faker.Library
{
    public class RegExMatcher : IMatcher
    {
        public EndpointMatch Match(IOwinRequest request, Endpoint endpoint)
        {
            var requestPath = request.Path + request.QueryString;

            if (
                Regex.IsMatch(requestPath, endpoint.Url) &&
                (endpoint.Method == request.Method ||
                 endpoint.Methods.Contains(request.Method)))
            {
                return new EndpointMatch(requestPath, endpoint);
            }

            return null;
        }
    }
}