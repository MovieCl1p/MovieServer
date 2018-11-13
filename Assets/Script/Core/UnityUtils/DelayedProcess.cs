using System;
using UnityEngine;

namespace Core.UnityUtils
{
    public class DelayedProcess : IUpdateHandler
    {
        protected float Delay;

        private float _progress;

        public object Target
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }

        public bool IsUpdating
        {
            get; set;
        }

        public bool IsRegistered
        {
            get; set;
        }
        
        public Action OnComplete
        {
            get; set;
        }

        public void Play(Action callback, object target, float delay)
        {
            OnComplete = callback;
            Target = target;
            Delay = delay;

            _progress = 0;

            UpdateNotifier.Instance.Register(this);
        }

        public void OnUpdate()
        {
            _progress += Time.deltaTime;

            if (_progress > Delay)
            {
                try
                {
                    OnComplete();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

                UpdateNotifier.Instance.UnRegister(this);
            }
        }
    }
}