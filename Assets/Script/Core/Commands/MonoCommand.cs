
using Core.States;
using System;
using UnityEngine;

namespace Core.Commands
{
    public abstract class MonoCommand : MonoScheduledBehaviour
    {
        private object[] _args;

        private bool _running;
        private bool _started;
        private bool _resourcesReleased;

        private bool _success;
        private bool _hold;

        public bool IsHold
        {
            get
            {
                return _hold;
            }

            set
            {
                _hold = value;
                OnHold();
            }
        }

        protected virtual void OnHold()
        {
        }

        public bool FinishedWithSuccess
        {
            get
            {
                return _success;
            }
        }

        public bool IsRunning
        {
            get
            {
                return _running && _started;
            }
        }

        public MonoCommand()
        {
            _running = true;

            MonoLog.Log(MonoLogChannel.Core, String.Format("Created command {0}", this.GetType().Name));
        }

        protected override sealed void Start()
        {
            if (_running)
            {
                _started = true;

                MonoLog.Log(MonoLogChannel.Core, String.Format("Executing command {0}", this.GetType().Name));

                try
                {
                    OnStart(_args);
                }
                catch (Exception e)
                {
                    MonoLog.Log(MonoLogChannel.Core, String.Format("Unable to execute command {0}", this.GetType().Name), e);

                    FinishCommand(false);
                    return;
                }
            }
        }


        protected abstract void OnStart(object[] args);

        

        protected sealed override void Update()
        {
            if (!_hold)
            {
                if (_running)
                {
                    OnUpdate();
                }

                if (_running)
                {
                    base.Update();
                }
            }
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnFinishCommand(bool success = true)
        {
        }

        protected void FinishCommand(bool success = true)
        {
            if (_running)
            {
                _running = false;
                _success = success;

                MonoLog.Log(MonoLogChannel.Core, "Finishing command " + this.GetType().Name);
                OnFinishCommand(success);

                if (_started)
                {
                    OnReleaseResources();
                    _resourcesReleased = true;
                }

                UnityEngine.Object.Destroy(this);
            }
        }

        private void OnApplicationQuit()
        {
            _resourcesReleased = true;
        }

        protected override sealed void OnDestroy()
        {
            MonoLog.Log(MonoLogChannel.Core, "Command " + this.GetType().Name + " has beed destroyed");

            if (!_resourcesReleased && _started)
            {
                try
                {
                    OnReleaseResources();
                }
                catch (Exception e)
                {
                    MonoLog.Log(MonoLogChannel.Core, "Error during release command resources", e);
                }

                _resourcesReleased = true;
            }
        }

        public void Terminate()
        {
            if (_running)
            {
                FinishCommand(_started);
            }
        }

        public static TCommand ExecuteOn<TCommand>(GameObject target, object[] args) where TCommand : MonoCommand, new()
        {
            TCommand result = target.AddComponent<TCommand>();
            result._args = args;

            return result;
        }

        public static MonoCommand ExecuteOn(Type type, GameObject target, object[] args)
        {
            MonoCommand result = (MonoCommand)target.AddComponent(type);
            result._args = args;

            return result;
        }

        public static TCommand Execute<TCommand>(params object[] args) where TCommand : MonoCommand, new()
        {
            GameObject mgGameObject = MonoSingleton.GetGameObject();

            bool oneItemOnScene = false;

            if (oneItemOnScene)
            {
                TCommand command = (TCommand)FindObjectOfType(typeof(TCommand));

                if (command != null)
                {
                    MonoLog.Log(MonoLogChannel.Core, "Found existing command " + command);

                    return command;
                }
            }

            GameObject target = new GameObject(typeof(TCommand).Name);
            target.transform.parent = mgGameObject.transform;

            UnityEngine.Object.DontDestroyOnLoad(target);

            TCommand result = ExecuteOn<TCommand>(target, args);


            return result;
        }

        public static TCommand Execute<TCommand>() where TCommand : MonoCommand, new()
        {
            return Execute<TCommand>(new object[] { });
        }
    }

    public abstract class MonoCommand<T> : MonoCommand where T : MonoCommand<T>, new()
    {
        public MonoCommand()
        {
            AsyncToken = new AsyncToken<T>((T)this);
        }

        public AsyncToken<T> AsyncToken
        {
            get;
            private set;
        }

        protected sealed override void OnFinishCommand(bool success = true)
        {
            if (success)
            {
                AsyncToken.FireResponse();
            }
            else
            {
                AsyncToken.FireFault();
            }
        }
    }
}
