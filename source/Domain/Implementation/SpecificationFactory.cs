using Architecture.Domain.Interfaces;

namespace Architecture.Domain.Implementation;

public class SpecificationFactory : ISpecificationFactory
{
    public ISpecification<T> Create<T>()
    {
        return new NullSpecification<T>();
    }
}
