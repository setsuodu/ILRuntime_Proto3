using System;
using System.Collections.Generic;
using UnityEngine;
using IMessage = Google.Protobuf.IMessage;

namespace HotFix
{
    // 处理子线程的推送
    public class EventManager : MonoBehaviour
    {
        static EventManager _instance;
        public static EventManager Get()
        {
            return _instance;
        }

        public Queue<byte[]> queue;

        void Awake()
        {
            _instance = this;
            queue = new Queue<byte[]>();
        }

        void Update()
        {
            if (queue.Count > 0)
            {
                var data = queue.Dequeue();
                Handle(data);
            }
        }

        void Handle(byte[] buffer)
        {
            // 解析msgId
            byte msgId = buffer[0];
            byte[] body = new byte[buffer.Length - 1];
            Array.Copy(buffer, 1, body, 0, buffer.Length - 1);

            PacketType type = (PacketType)msgId;
            //Debug.Log($"PacketType={type}");
            switch (type)
            {
                case PacketType.Connected:
                    break;
                case PacketType.S2C_LoginResult:
                    {
                        S2C_Login packet = ProtobufferTool.Deserialize<S2C_Login>(body); //解包
                        Debug.Log($"[Handle:{type}] Code={packet.Code}, Nickname={packet.Nickname}");
                        NetPacketManager.Trigger(type, packet); //派发（为什么在这创建UI，会堵塞接收线程？？）
                    }
                    break;
                case PacketType.S2C_RoomInfo:
                    {
                        S2C_RoomInfo packet = ProtobufferTool.Deserialize<S2C_RoomInfo>(body); //解包
                        Debug.Log($"[Handle:{type}] RoomId={packet.Room.RoomId}, RoomName={packet.Room.RoomName}, Num={packet.Room.LimitNum}");
                        NetPacketManager.Trigger(type, packet); //派发
                    }
                    break;
                case PacketType.S2C_RoomList:
                    {
                        Debug.Log($"[Handle:{type}]");
                        S2C_GetRoomList packet = ProtobufferTool.Deserialize<S2C_GetRoomList>(body); //解包
                        Debug.Log(222);
                        Debug.Log($"[Handle:{type}] RoomCount={packet.Rooms.Count}");
                        if (packet.Rooms.Count > 0)
                        {
                            Debug.Log($"Room.0={packet.Rooms[0].RoomId}");
                        }
                        NetPacketManager.Trigger(type, packet); //派发
                    }
                    break;
                case PacketType.S2C_Chat:
                    {
                        TheMsg packet = ProtobufferTool.Deserialize<TheMsg>(body); //解包
                        Debug.Log($"[Handle:{type}] {packet.Name}说: {packet.Content}");
                        NetPacketManager.Trigger(type, packet); //派发
                    }
                    break;
                default:
                    Debug.LogError($"Handle:无法识别的消息: {type}");
                    break;
            }
            //TODO: 通过委托分发出去
        }
    }

    public class NetPacketManager
    {
        public delegate void EventHandler(PacketType t, IMessage packet);
        public static event EventHandler Event;
        public static void RegisterEvent(EventHandler action)
        {
            Event += action;
        }
        public static void UnRegisterEvent(EventHandler action)
        {
            Event -= action;
        }
        public static void Trigger(PacketType type, IMessage packet)
        {
            Event?.Invoke(type, packet);
        }
    }

    public class NetStateManager
    {
        public delegate void EventHandler(int t);
        public static event EventHandler Event;
        public static void RegisterEvent(EventHandler action)
        {
            Event += action;
        }
        public static void UnRegisterEvent(EventHandler action)
        {
            Event -= action;
        }
        public static void Trigger(int type)
        {
            Event?.Invoke(type);
        }
    }
}