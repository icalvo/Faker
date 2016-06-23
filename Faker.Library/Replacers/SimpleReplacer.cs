namespace Faker.Library.Replacers
{
    internal class SimpleReplacer : IReplacer
    { 
        public EndpointResponse Replace(EndpointMatch match)
        {
            return new EndpointResponse
            {
                Url = match.RequestedUrl,
                Content = match.Endpoint.Content,
                ContentType = match.Endpoint.ContentType,
                StatusCode = match.Endpoint.StatusCode
            };
        }
    }
}