namespace Faker.Library.Replacers
{
    internal class SimpleReplacer : IReplacer
    { 
        public EndpointResponse Replace(RequestMatch match)
        {
            return new EndpointResponse
            {
                Url = match.Actual.Url,
                Content = match.Endpoint.Response.Content,
                ContentType = match.Endpoint.Response.ContentType,
                StatusCode = match.Endpoint.Response.StatusCode
            };
        }
    }
}