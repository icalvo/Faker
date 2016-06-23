using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal class PluginMatcher : IMatcher
    {
        public RequestMatch Match(Endpoint endpoint, Request actual)
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

            return internalMatcher.Match(endpoint, actual);
        }
    }
}