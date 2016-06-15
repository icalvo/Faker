using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Faker.UI
{
    public class FakerMiddleware
    {
        private readonly IEndpointRepository _repository;

        public FakerMiddleware(IEndpointRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(IOwinContext context)
        {
            var r = _repository.All().Select(e => e.TryMatch(context.Request)).FirstOrDefault(x => x != null) ?? new EndpointResponse
            {
                ContentType = "text/plain", StatusCode = 404, Content = "FAKER did not find any valid response."
            };
            context.Response.ContentType = r.ContentType;
            context.Response.StatusCode = r.StatusCode;
            if (r.Content == null)
            {
                return;
            }

            await context.Response.WriteAsync(r.Content);
        }
    }
}