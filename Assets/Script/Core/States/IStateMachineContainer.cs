using Core.Commands;
using UnityEngine;

namespace Core.States
{
    public interface IStateMachineContainer
    {
        void Next(StateCommand previousState);

        GameObject GameObject
        {
            get;
        }
    }
}
