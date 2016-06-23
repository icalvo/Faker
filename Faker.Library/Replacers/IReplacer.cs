namespace Faker.Library.Replacers
{
    internal interface IReplacer
    {
        EndpointResponse Replace(RequestMatch match);
    }
}