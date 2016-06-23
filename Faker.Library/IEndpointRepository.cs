using System.Collections.Generic;

namespace Faker.Library
{
    internal interface IEndpointRepository
    {
        IEnumerable<Endpoint> All();
    }
}