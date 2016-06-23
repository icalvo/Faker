using Microsoft.Owin;

namespace Faker.Library.Matchers
{
    internal interface IMatcher
    {
        RequestMatch Match(Endpoint endpoint, Request actual);
    }
}