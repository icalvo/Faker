using System.Net.Http;

namespace Faker.Tests
{
    public class ResponseAndResult
    {
        public ResponseAndResult(HttpResponseMessage response)
        {
            Response = response;
            Result = response.Content.ReadAsStringAsync().Result;
        }

        public HttpResponseMessage Response { get; }
        public string Result { get; }
    }
}