using Core.Commands;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{
    public sealed class StateFlow : IStateMachineContainer
    {
        private readonly List<NextStatePair> _stateFlow;
        private readonly IStateMachineContainer _container;
        private readonly StateMachine _stateMachine;

        public StateFlow(IStateMachineContainer container, StateMachine stateMachine)
        {
            _stateFlow = new List<NextStatePair>();
            _container = container;
            _stateMachine = stateMachine;
        }

        public void Add(NextStatePair pair)
        {
            _stateFlow.Add(pair);
        }

        public void Next(StateCommand prevState)
        {
            NextStatePair pair = null;

            if (prevState != null)
            {
                pair = _stateFlow.Find(delegate (NextStatePair nextStatePair)
                {
                    return nextStatePair.Target == prevState.GetType();
                });
            }

            if (pair != null)
            {
                if (prevState.FinishedWithSuccess)
                {
                    _stateMachine.ApplyState(pair.Success, _container);
                }
                else
                {
                    _stateMachine.ApplyState(pair.Fail, _container);
                }
            }
        }

        public GameObject GameObject
        {
            get
            {
                return _container.GameObject;
            }
        }
    }
}
