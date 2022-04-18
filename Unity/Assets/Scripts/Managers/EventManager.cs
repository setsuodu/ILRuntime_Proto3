using UnityEngine.Events;
//using Code.Shared;
//using LiteNetLib;
//using LiteNetLib.Utils;

/*
public class MyEvent<T0> : UnityEvent<T0> { }
public class MyEvent<T0, T1> : UnityEvent<T0, T1> { }
public class MyEvent<T0, T1, T2> : UnityEvent<T0, T1, T2> { }
public class EventManager
{
    private static MyEvent<PacketType, INetSerializable, NetPeer> eventList = new MyEvent<PacketType, INetSerializable, NetPeer>();
    public static void RegisterEvent(UnityAction<PacketType, INetSerializable, NetPeer> action)
    {
        eventList.AddListener(action);
    }
    public static void UnRegisterEvent(UnityAction<PacketType, INetSerializable, NetPeer> action)
    {
        eventList.RemoveListener(action);
    }
    public static void Trigger(PacketType type, INetSerializable reader, NetPeer peer)
    {
        eventList.Invoke(type, reader, peer);
    }
}

public class UserEvent<T0> : UnityEvent<T0> { }
public class UserEventManager
{
    private static UserEvent<PlayerStatus> userEventList = new UserEvent<PlayerStatus>();
    public static void RegisterEvent(UnityAction<PlayerStatus> action)
    {
        userEventList.AddListener(action);
    }
    public static void UnRegisterEvent(UnityAction<PlayerStatus> action)
    {
        userEventList.RemoveListener(action);
    }
    public static void Trigger(PlayerStatus type)
    {
        userEventList.Invoke(type);
    }
}
*/