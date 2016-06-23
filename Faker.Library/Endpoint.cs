namespace Faker.Library
{
    internal class Endpoint
    {
        public Request Request { get; set; }
        public Response Response { get; set; } = new Response();

        public string Matcher { get; set; }

        public string Replacer { get; set; }

    }
}