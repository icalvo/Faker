namespace Faker.Library
{
    internal class Response
    {
        public string ContentType { get; set; } = "application/json";

        public string Content { get; set; }

        public int StatusCode { get; set; } = 200;
    }
}