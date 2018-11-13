using Core.Services.Network.Specifications;

namespace Core.Services.Network.Messages
{
    public class MessageResponseHandler<T> : IMessageResponseHandler<T>
    {
        private IMessageResponseHandler<T> successor;
        private string name;
        private ISpecification<T> specification;

        public bool CanHandle(T o)
        {
            return this.specification.IsSatisfiedBy(o);
        }

        public void SetSuccessor(IMessageResponseHandler<T> handler)
        {
            this.successor = handler;
        }

        public void HandleRequest(T o)
        {
            if (CanHandle(o))
            {
                Process(o);
            }
            else
            {
                this.successor.HandleRequest(o);
            }
        }

        public void SetSpecification(ISpecification<T> specification)
        {
            this.specification = specification;
        }

        public virtual void Process(T o)
        {
        }
    }
}