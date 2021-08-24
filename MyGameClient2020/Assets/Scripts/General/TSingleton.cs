using Common.Msic;

namespace Common.General
{
    public class TSingleton<T> where T : class
    {
        static T s_instance = null;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = GeneralMsic.CallNonPublicConstructor<T>(null);
                return s_instance;
            }
        }

        public static void Dispose()
        {
            if (s_instance != null)
            {
                GeneralMsic.CallNonPublicMethod(s_instance, "OnDispose", null);
                s_instance = null;
            }
        }

        protected virtual void OnDispose() { }
    }
}