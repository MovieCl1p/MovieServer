using System;
using System.Collections.Generic;

namespace Core.Binder
{
    public class BindField<T>
    {
        public List<Action<T>> Subscribers;

        private bool _isLocked;
        private T _oldValue;
        protected T _value;

        public virtual T Value
        {
            get { return _value; }
            set
            {
                if(_value == null)
                {
                    _value = value;
                    ExecuteBinding(_value);
                }
                else if (!_value.Equals(value))
                {
                    _value = value;
                    ExecuteBinding(_value);
                }
            }
        }

        public bool Equals(T obj)
        {
            return Value.Equals(obj);
        }

        public void Bind(Action<T> method, bool initialize = true)
        {
            if (Subscribers == null)
            {
                Subscribers = new List<Action<T>>();
            }

            Subscribers.Add(method);

            if (initialize)
            {
                ExecuteBinding(Value);
            }
        }

        public void UnBindAll()
        {
            Subscribers = null;
        }

        public void UnBind(Action<T> method)
        {
            if (Subscribers == null || Subscribers.Count == 0)
            {
                return;
            }

            Subscribers.Remove(method);
        }

        protected virtual void ExecuteBinding(T newValue)
        {
            if (Subscribers == null || Subscribers.Count == 0)
                return;

            for (int i = Subscribers.Count - 1; i >= 0; i--)
            {
                Subscribers[i].Invoke(_isLocked ? _oldValue : newValue);
            }
        }

        public void Retain()
        {
            _isLocked = true;
            _oldValue = Value;
        }

        public void Release()
        {
            _isLocked = false;
            ExecuteBinding(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
