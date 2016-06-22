using Microsoft.Owin;

namespace Faker.Library
{
    public interface IMatcher
    {
        EndpointMatch Match(IOwinRequest request, Endpoint endpoint);
    }
}