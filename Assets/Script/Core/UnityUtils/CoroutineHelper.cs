using UnityEngine;

namespace Core.UnityUtils
{
    public class CoroutineHelper : MonoBehaviour
    {
        private static CoroutineHelper _instance;

        public static CoroutineHelper Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<CoroutineHelper>();
                }

                return _instance;
            }
        }

        protected void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
