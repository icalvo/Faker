namespace Faker.Library
{
    internal class RequestMatch
    {
        public RequestMatch(Endpoint endpoint, Request actual)
        {
            Actual = actual;
            Endpoint = endpoint;
        }

        public Endpoint Endpoint { get; }
        public Request Actual { get; }

    }
}