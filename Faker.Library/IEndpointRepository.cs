using System.Collections.Generic;

namespace Faker.Library
{
    public interface IEndpointRepository
    {
        IEnumerable<Endpoint> All();
    }
}