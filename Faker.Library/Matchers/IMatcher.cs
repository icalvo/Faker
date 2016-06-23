using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal interface IMatcher
    {
        EndpointMatch Match(IOwinRequest request, Endpoint endpoint);
    }
}