using UnityEngine.Events;

namespace HotFix
{
    public class MyEvent<T0> : UnityEvent<T0> { }
    public class MyEvent<T0, T1> : UnityEvent<T0, T1> { }
    public class MyEvent<T0, T1, T2> : UnityEvent<T0, T1, T2> { }

    public class NetStateManager
    {
        private static MyEvent<int> eventList = new MyEvent<int>();
        public static void RegisterEvent(UnityAction<int> action)
        {
            eventList.AddListener(action);
        }
        public static void UnRegisterEvent(UnityAction<int> action)
        {
            eventList.RemoveListener(action);
        }
        public static void Trigger(int peer)
        {
            eventList.Invoke(peer);
        }
    }

    public class NetPacketManager
    {
        private static MyEvent<PacketType> eventList = new MyEvent<PacketType>();
        public static void RegisterEvent(UnityAction<PacketType> action)
        {
            eventList.AddListener(action);
        }
        public static void UnRegisterEvent(UnityAction<PacketType> action)
        {
            eventList.RemoveListener(action);
        }
        public static void Trigger(PacketType peer)
        {
            eventList.Invoke(peer);
        }
    }
}