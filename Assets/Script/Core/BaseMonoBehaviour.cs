using UnityEngine;

namespace Core
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;
        private bool _released;

        public Transform CachedTransform
        {
            get
            {
                if (_transform == null)
                    _transform = this.transform;

                return _transform;
            }
        }

        protected virtual void Start()
        {
        }

        private void OnApplicationQuit()
        {
            _released = true;
        }

        protected virtual void OnDestroy()
        {
            if (!_released)
            {
                OnReleaseResources();
            }

            _released = true;
        }

        protected virtual void OnReleaseResources()
        {
        }
    }
}
