using System;

namespace Core.Services.Network.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        private Func<T, bool> _expression;

        public Specification(Func<T, bool> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                this._expression = expression;
            }
        }

        public bool IsSatisfiedBy(T o)
        {
            return _expression(o);
        }
    }
}
