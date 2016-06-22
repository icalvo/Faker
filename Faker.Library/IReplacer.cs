namespace Faker.Library
{
    public interface IReplacer
    {
        EndpointResponse Replace(EndpointMatch match);
    }
}