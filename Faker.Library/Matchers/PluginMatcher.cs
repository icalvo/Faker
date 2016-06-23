using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal class PluginMatcher : IMatcher
    {
        public EndpointMatch Match(IOwinRequest request, Endpoint endpoint)
        {
            IMatcher internalMatcher;
            switch (endpoint.Matcher)
            {
                case "simple":
                    internalMatcher = new SimpleMatcher();
                    break;
                default:
                    internalMatcher = new RegexMatcher();
                    break;
            }

            return internalMatcher.Match(request, endpoint);
        }
    }
}