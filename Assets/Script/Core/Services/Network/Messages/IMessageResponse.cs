using Core.Services.Network.Specifications;

namespace Core.Services.Network.Messages
{
    public interface IMessageResponseHandler<T>
    {
        void SetSuccessor(IMessageResponseHandler<T> handler);

        void HandleRequest(T o);

        void SetSpecification(ISpecification<T> specification);
    }
}
