using System;

namespace Core.States
{
    public sealed class NextStatePair
    {
        public readonly Type Success;
        public readonly Type Fail;
        public readonly Type Target;

        public NextStatePair(Type target, Type success, Type fail)
        {
            this.Target = target;
            this.Success = success;
            this.Fail = fail;
        }

        public NextStatePair(Type target, Type success)
        {
            this.Target = target;
            this.Success = success;
            this.Fail = success;
        }
    }
}
