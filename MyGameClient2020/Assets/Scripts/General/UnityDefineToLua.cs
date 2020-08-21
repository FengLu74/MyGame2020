

namespace MGame.General
{
   
    public static class UnityDefineToLua
    {
        public static bool UNITY_EDITOR
        {
            get
            {
#if UNITY_EDITOR
                return true;
#else
                return false;
#endif
            }
        }

        public static bool UNITY_STANDALONE
        {
            get
            {
#if UNITY_STANDALONE
            return true;
#else
                return false;
#endif
            }
        }

        public static bool UNITY_ANDROID
        {
            get
            {
#if UNITY_ANDROID
                return true;
#else
            return false;
#endif
            }
        }

        public static bool UNITY_IOS
        {
            get
            {
#if UNITY_IOS
                return true;
#else
                return false;
#endif
            }
        }
    }
}