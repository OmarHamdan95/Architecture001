namespace Architecture.Domain.Interfaces;

public interface ISpecificationFactory
{
    ISpecification<T> Create<T>();
}
