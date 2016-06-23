using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal class RegexMatcher : IMatcher
    {
        public EndpointMatch Match(IOwinRequest request, Endpoint endpoint)
        {
            var regex = endpoint.Regex();

            var requestPath = request.Path + request.QueryString;

            var match = regex.Match(requestPath);

            if (
                !match.Success ||
                (endpoint.Method != request.Method && !endpoint.Methods.Contains(request.Method)))
            {
                return null;
            }

            return new EndpointMatch(requestPath, endpoint, Replacements(endpoint, requestPath));
        }


        private static Dictionary<string, string> Replacements(Endpoint endpoint, string requestedUrl)
        {
            var regex = endpoint.Regex();
            var regexMatch = regex.Match(requestedUrl);
            var enumerable = regex.GetGroupNames()
                .Where(x => x != "0")
                .Select(x => new { Key = x, regexMatch.Groups[x].Value })
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
            return enumerable;
        }
    }
}