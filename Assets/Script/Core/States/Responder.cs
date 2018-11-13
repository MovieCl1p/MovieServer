using System;

namespace Core.States
{
    public class Responder<T>
    {
        public readonly Action<T> Result;
        public readonly Action<T> Fault;

        public Responder(Action<T> result, Action<T> fault)
        {
            this.Result = result;
            this.Fault = fault;
        }

        public Responder(Action<T> result)
        {
            this.Result = result;
        }
    }
}
