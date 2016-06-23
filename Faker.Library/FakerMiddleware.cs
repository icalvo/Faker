using System.Linq;
using System.Threading.Tasks;
using Faker.Library.Matchers;
using Faker.Library.Replacers;
using Microsoft.Owin;

namespace Faker.Library
{
    internal class FakerMiddleware
    {
        private readonly IEndpointRepository _endpointRepo;
        private readonly IMatcher _matcher;
        private readonly IReplacer _replacer;

        public FakerMiddleware(
            IEndpointRepository endpointRepo, 
            IMatcher matcher, 
            IReplacer replacer)
        {
            _endpointRepo = endpointRepo;
            _matcher = matcher;
            _replacer = replacer;
        }

        public async Task Handle(IOwinContext context)
        {
            EndpointMatch match = _endpointRepo
                .All()
                .Select(e => _matcher.Match(context.Request, e))
                .FirstOrDefault(e => e != null);

            var response = match != null 
                ? _replacer.Replace(match) 
                : DefaultResponse();

            await CopyResponseAsync(context, response);
        }

        private static async Task CopyResponseAsync(IOwinContext context, EndpointResponse response)
        {
            context.Response.ContentType = response.ContentType;
            context.Response.StatusCode = response.StatusCode;
            if (response.Content == null)
            {
                return;
            }

            await context.Response.WriteAsync(response.Content);
        }

        private static EndpointResponse DefaultResponse()
        {
            return new EndpointResponse
            {
                ContentType = "text/plain",
                StatusCode = 404,
                Content = "FAKER did not find any valid response."
            };
        }
    }
}