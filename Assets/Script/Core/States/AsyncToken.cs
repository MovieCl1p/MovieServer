using System.Collections.Generic;

namespace Core.States
{
    public class AsyncToken<T>
    {
        private readonly List<Responder<T>> _responders;
        private T _command;

        public AsyncToken(T command)
        {
            _responders = new List<Responder<T>>();
            _command = command;
        }

        public void AddResponder(Responder<T> responder)
        {
            _responders.Add(responder);
        }

        public void FireResponse()
        {
            foreach (Responder<T> responder in _responders)
            {
                responder.Result(_command);
            }

            _responders.Clear();
        }

        public void FireFault()
        {
            foreach (Responder<T> responder in _responders)
            {
                if (responder.Fault != null)
                {
                    responder.Fault(_command);
                }
            }

            _responders.Clear();
        }
    }
}
