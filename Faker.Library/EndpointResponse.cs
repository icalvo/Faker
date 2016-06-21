namespace Faker.UI
{
    public class EndpointResponse
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
    }
}