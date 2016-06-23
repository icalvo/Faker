using System.Text.RegularExpressions;

namespace Faker.Library
{
    internal class EndpointMatch<T> : EndpointMatch
    {
        public T Match { get; set; }

        public EndpointMatch(
            string requestedUrl, Endpoint endpoint,
            T match) : base(requestedUrl, endpoint)
        {
            Match = match;
        }
    }
}