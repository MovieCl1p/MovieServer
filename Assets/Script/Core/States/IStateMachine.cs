using Core.Commands;
using System;

namespace Core.States
{
    public interface IStateMachine
    {
        StateCommand State
        {
            get;
        }

        StateCommand ApplyState(Type type, params object[] args);

        T ApplyState<T>(params object[] args) where T : StateCommand, new();

        T Execute<T>(params object[] args) where T : StateCommand, new();

        StateCommand Execute(Type type, params object[] args);
    }
}
