using UnityEngine;


namespace MGame.General
{
   
    public class TSingletonMono<T> : CachedMono where T : CachedMono
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        string instanceGoName = typeof(T).Name;
                        _instance = new GameObject(instanceGoName).AddComponent<T>();
                    }
                    GameObject.DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(CachedGameObject);
                _instance = null;
                return;
            }

            _instance = GetComponent<T>();

            GameObject.DontDestroyOnLoad(_instance);
            Init();
        }


        protected virtual void Init()
        {

        }

        public virtual void Dispose()
        {
            if (_instance != null)
            {
                GameObject.DestroyImmediate(_instance.CachedGameObject);
                _instance = null;
            }
        }

        public static string GetName()
        {
            return typeof(T).ToString();
        }
    }
}