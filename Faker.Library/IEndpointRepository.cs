using System.Collections.Generic;

namespace Faker.UI
{
    public interface IEndpointRepository
    {
        IEnumerable<Endpoint> All();
    }
}