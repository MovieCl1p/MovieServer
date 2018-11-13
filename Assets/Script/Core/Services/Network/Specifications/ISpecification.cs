
namespace Core.Services.Network.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T o);
    }
}
