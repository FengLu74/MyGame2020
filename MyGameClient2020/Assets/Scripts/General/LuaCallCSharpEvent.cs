

namespace MGame.General
{
    public class LuaCallCSharpEvent
    {
        public static void Broadcast(string eventName)
        {
            MessageEvent.Broadcast(eventName);
        }

        public static void Broadcast(string eventName, int value)
        {
            MessageEvent.Broadcast<int>(eventName, value);
        }

        public static void Broadcast(string eventName, string value)
        {
            MessageEvent.Broadcast<string>(eventName, value);
        }

        public static void AddListener<T>(string eventName, Callback<T> call)
        {
            MessageEvent.AddListener<T>(eventName, call);
        }
        public static void AddListener(string eventName, Callback call)
        {
            MessageEvent.AddListener(eventName, call);
        }
    }
}