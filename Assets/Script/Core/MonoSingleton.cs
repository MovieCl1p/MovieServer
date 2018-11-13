using UnityEngine;

namespace Core
{
    public abstract class MonoSingleton : MonoScheduledBehaviour
    {
        public const string NAME = "MonoSingleton";

        private static GameObject _gameObject;

        public static GameObject GetGameObject()
        {
            if (MonoSingleton._gameObject != null)
            {
                return MonoSingleton._gameObject;
            }

            MonoSingleton._gameObject = GameObject.Find(NAME);

            if (MonoSingleton._gameObject == null)
            {
                MonoSingleton._gameObject = new GameObject(NAME);
                UnityEngine.Object.DontDestroyOnLoad(MonoSingleton._gameObject);
            }
            return MonoSingleton._gameObject;
        }
    }

    public class MonoSingleton<T> : MonoSingleton where T : MonoSingleton<T>
    {
        public bool IsOverrideOnAwake = true;

        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Initialize();
                }

                return _instance;
            }
        }

        public static void Initialize()
        {
            MonoBehaviour monoBehaviour = UnityEngine.Object.FindObjectOfType(typeof(T)) as MonoBehaviour;

            if (monoBehaviour == null)
            {
                GameObject mgGameObject = MonoSingleton.GetGameObject();

                GameObject gameObject = new GameObject(typeof(T).ToString());

                gameObject.AddComponent(typeof(T));
                gameObject.transform.parent = mgGameObject.transform;

                UnityEngine.Object.DontDestroyOnLoad(gameObject);

                _instance = gameObject.GetComponent<T>();
            }
            else
                _instance = monoBehaviour.GetComponent<T>();
        }

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
        }

        private void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}
