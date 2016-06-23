using System.Collections.Generic;

namespace Faker.Library
{
    internal class EndpointMatch
    {
        public EndpointMatch(string requestedUrl, Endpoint endpoint,
            Dictionary<string, string> replacements = null)
        {
            RequestedUrl = requestedUrl;
            Endpoint = endpoint;
            Replacements = replacements ??
                new Dictionary<string, string>();
        }

        public string RequestedUrl { get; }
        public Endpoint Endpoint { get; }
        public Dictionary<string, string> Replacements { get; set; }
    }
}