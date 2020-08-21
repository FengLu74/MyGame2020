

namespace MGame.General
{

    public static class UnityEngineObjectExtention
    {
        public static bool IsNull(this UnityEngine.Object o)
        {
            return o == null; // same as   System.Object.ReferenceEquals(null, o);
        }
    }
}