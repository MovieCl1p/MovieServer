
using Core.Commands;
using System;
using System.Collections.Generic;

namespace Core.States
{
    public class StateMachine : IStateMachine
    {
        public event System.Action<StateMachine> Changed;

        private IStateMachineContainer _container;
#if UNITY_EDITOR
        public string[] StateHistory;
        private List<string> _stateHistory = new List<string>();
#endif
        private readonly List<MonoCommand> _commands;

        private StateCommand _state;

        public StateCommand State
        {
            get
            {
                return _state;
            }
        }

        public StateMachine(IStateMachineContainer container)
        {
            _container = container;
            _commands = new List<MonoCommand>();
        }

        private void OnStateFinished(StateCommand state)
        {
            if (state == _state)
            {
                _state = null;

                StopAllCommands();

                _container.Next(state);
            }
        }

        private StateCommand ChangeState(Type type, object[] args)
        {
            StateCommand prevState = _state;

            _state = null;

            if (prevState != null)
            {
                prevState.Terminate();

                StopAllCommands();
            }

            _state = CreateState(type, args);

#if UNITY_EDITOR
            _stateHistory.Add(type.Name);

            StateHistory = _stateHistory.ToArray();
#endif
            _state.AsyncToken.AddResponder(new Responder<StateCommand>(OnStateFinished, OnStateFinished));

            if (Changed != null)
                Changed(this);

            return _state;
        }

        public StateCommand ApplyState(Type type, params object[] args)
        {
            return ChangeState(type, args);
        }

        public T ApplyState<T>(params object[] args) where T : StateCommand, new()
        {
            return ChangeState(typeof(T), args) as T;
        }

        private StateCommand CreateState(Type type, object[] args)
        {
            return (StateCommand)MonoCommand.ExecuteOn(type, _container.GameObject, args);
        }

        private void StopAllCommands()
        {
            int maxCalls = _commands.Count;

            while (_commands.Count > 0)
            {
                maxCalls--;

                _commands[0].Terminate();

                if (maxCalls == 0)
                {
                    break;
                }
            }

            if (_commands.Count != 0)
            {
                MonoLog.LogWarning(MonoLogChannel.Exceptions, "State machine does not finished all commands");
            }

            /*for(int i = _commands.Count-1; i >= 0; i--)
            {
                _commands[i].Terminate();
            }*/
        }

        public T Execute<T>(params object[] args) where T : StateCommand, new()
        {
            T command = MonoCommand.ExecuteOn<T>(_container.GameObject, args);

            _commands.Add(command);

            command.AsyncToken.AddResponder(new Responder<StateCommand>(delegate (StateCommand result)
            {
                _commands.Remove(result);
            },
            delegate (StateCommand result)
            {
                _commands.Remove(result);
            }
            ));

            return command;
        }

        public StateCommand Execute(Type type, params object[] args)
        {
            StateCommand command = (StateCommand)MonoCommand.ExecuteOn(type, _container.GameObject, args);

            _commands.Add(command);

            command.AsyncToken.AddResponder(new Responder<StateCommand>(delegate (StateCommand result)
            {
                _commands.Remove(result);
            }));

            return command;
        }
    }
}
