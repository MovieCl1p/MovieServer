using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UnityUtils
{
    public interface IUpdateHandler
    {
        bool IsUpdating { get; set; }

        bool IsRegistered { get; set; }

        void OnUpdate();
    }

    public class UpdateNotifier : MonoSingleton<UpdateNotifier>
    {
        private static long _lastTickEventHappened = -1;

        private readonly List<IUpdateHandler> _handlers = new List<IUpdateHandler>();

        private static readonly Stack<DelayedProcess> DelayedProcesses = new Stack<DelayedProcess>(10);
        private static readonly List<DelayedProcess> DelayedProcessesRunning = new List<DelayedProcess>(10);

        public void Register(IUpdateHandler updateHandler)
        {
            if (updateHandler.IsUpdating)
            {
                return;
            }

            updateHandler.IsUpdating = true;

            if (updateHandler.IsRegistered)
            {
                return;
            }

#if UNITY_EDITOR
            if (_handlers.Contains(updateHandler))
            {
                throw new Exception("Should not be there!");
            }
#endif

            updateHandler.IsRegistered = true;

            _handlers.Add(updateHandler);
        }
        public void UnRegister(IUpdateHandler updateHandler)
        {
            updateHandler.IsUpdating = false;
        }

        protected override void Update()
        {
            base.Update();
            CallUpdateHappened();
        }

        /// <summary>
        /// Calls event UpdateHappened
        /// </summary>
        protected void CallUpdateHappened()
        {
            var currentFrame = Time.frameCount;
            if (_lastTickEventHappened >= currentFrame)
                return;

            _lastTickEventHappened = currentFrame;

            var lastFinishedHandler = -1;

            for (int index = 0; index < _handlers.Count; index++)
            {
                var updateHandler = _handlers[index];

                if (!updateHandler.IsUpdating)
                {
                    lastFinishedHandler = index;

                    continue;
                }

                updateHandler.OnUpdate();

                if (!updateHandler.IsUpdating)
                {
                    lastFinishedHandler = index;
                }
            }

            UnregisterDeadUpdateNotifiers(_handlers, lastFinishedHandler);
            ClearDeadUpdateNotifiers(DelayedProcessesRunning, DelayedProcessesRunning.Count - 1,
                process =>
                {
                    process.Target = null;
                    DelayedProcesses.Push(process);
                });
        }

        public void StopAllDelayedProcesses(object obj)
        {
            for (int index = 0; index < DelayedProcessesRunning.Count; index++)
            {
                var updateHandler = DelayedProcessesRunning[index];

                if (updateHandler.Target == obj)
                {
                    updateHandler.IsUpdating = false;
                }
            }
        }

        public IUpdateHandler ExecuteWithDelay(Action callback, object target, float delay)
        {
            var tween = DelayedProcesses.Pop();

            tween.Play(callback, target, delay);

            DelayedProcessesRunning.Add(tween);

            return tween;
        }

        public Coroutine ExecuteWithDelay(Action method, float delay)
        {
            return StartCoroutine(Execute(method, delay));
        }

        public Coroutine ExecuteWithDelay<T>(Action<T> method, T parameter, float delay)
        {
            return StartCoroutine(Execute(method, parameter, delay));
        }

        private void UnregisterDeadUpdateNotifiers<T>(List<T> list, int from, Action<T> onRemove = null) where T : IUpdateHandler
        {
            var removesPerFrame = 3;

            for (int index = from; index >= 0; index--)
            {
                var updateHandler = list[index];

                if (!updateHandler.IsUpdating)
                {
                    updateHandler.IsRegistered = false;

                    if (onRemove != null)
                    {
                        onRemove(updateHandler);
                    }

                    list.RemoveAt(index);

                    removesPerFrame--;

                    if (removesPerFrame <= 0)
                    {
                        break;
                    }
                }
            }
        }
        private void ClearDeadUpdateNotifiers<T>(List<T> list, int from, Action<T> onRemove = null) where T : IUpdateHandler
        {
            var removesPerFrame = 3;

            for (int index = from; index >= 0; index--)
            {
                var updateHandler = list[index];

                if (!updateHandler.IsRegistered)
                {
                    if (onRemove != null)
                    {
                        onRemove(updateHandler);
                    }

                    list.RemoveAt(index);

                    removesPerFrame--;

                    if (removesPerFrame <= 0)
                    {
                        break;
                    }
                }
            }
        }

        private IEnumerator Execute(Action method, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (method != null)
            {
                method();
            }
        }

        private IEnumerator Execute<T>(Action<T> method, T parameter, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (method != null)
            {
                method(parameter);
            }
        }
    }
}