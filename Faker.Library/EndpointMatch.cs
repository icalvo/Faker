namespace Faker.Library
{
    public class EndpointMatch
    {
        public EndpointMatch(string requestedUrl, Endpoint endpoint)
        {
            RequestedUrl = requestedUrl;
            Endpoint = endpoint;
        }

        public string RequestedUrl { get; }
        public Endpoint Endpoint { get; }
    }
}