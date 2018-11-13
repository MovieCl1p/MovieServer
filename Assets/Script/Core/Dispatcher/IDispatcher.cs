using System;

namespace Core.Dispatcher
{
    public interface IDispatcher
    {

        void AddListener(Enum eventId, Action callback);

        void RemoveListener(Enum eventId, Action callback);
        
        void Dispatch(Enum enumName);
    }
}
