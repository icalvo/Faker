namespace Faker.Library.Replacers
{
    internal class PluginReplacer : IReplacer
    {
        public EndpointResponse Replace(RequestMatch match)
        {
            IReplacer internalReplacer;
            switch (match.Endpoint.Replacer)
            {
                case "simple":
                    internalReplacer = new SimpleReplacer();
                    break;
                default:
                    internalReplacer = new RegexReplacer();
                    break;
            }

            return internalReplacer.Replace(match);
        }
    }
}