namespace Faker.Library.Replacers
{
    internal interface IReplacer
    {
        EndpointResponse Replace(EndpointMatch match);
    }
}