using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using HotFix;
using ET;

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
            Debug.Log($"Handle.PacketType={type}");
            switch (type)
            {
                case PacketType.Connected:
                    break;
                case PacketType.S2C_LoginResult:
                    {
                        Debug.Log($"登录成功: 123");
                        MemoryStream stream = new MemoryStream(body, 0, body.Length); //解包
                        S2C_Login packet = ProtobufHelper.FromStream(typeof(S2C_Login), stream) as S2C_Login;
                        Debug.Log($"[Handle:{type}] Code={packet.Code}, Nickname={packet.Nickname}");
                        NetPacketManager.Trigger(type, packet); //派发（为什么在这创建UI，会堵塞接收线程？？）
                    }
                    break;
                case PacketType.S2C_RoomInfo:
                    {
                        //S2C_RoomInfo packet = ProtobufferTool.Deserialize<S2C_RoomInfo>(body); //解包
                        MemoryStream stream = new MemoryStream(body, 0, body.Length); //解包
                        S2C_RoomInfo packet = ProtobufHelper.FromStream(typeof(S2C_RoomInfo), stream) as S2C_RoomInfo;
                        Debug.Log($"[Handle:{type}] RoomId={packet.Room.RoomID}, RoomName={packet.Room.RoomName}, Num={packet.Room.LimitNum}");
                        NetPacketManager.Trigger(type, packet); //派发
                    }
                    break;
                case PacketType.S2C_RoomList:
                    {
                        Debug.Log($"[Handle:{type}]");
                        //S2C_GetRoomList packet = ProtobufferTool.Deserialize<S2C_GetRoomList>(body); //解包
                        MemoryStream stream = new MemoryStream(body, 0, body.Length); //解包
                        S2C_GetRoomList packet = ProtobufHelper.FromStream(typeof(S2C_GetRoomList), stream) as S2C_GetRoomList;
                        Debug.Log(222);
                        Debug.Log($"[Handle:{type}] RoomCount={packet.Rooms.Count}");
                        if (packet.Rooms.Count > 0)
                        {
                            Debug.Log($"Room.0={packet.Rooms[0].RoomID}");
                        }
                        NetPacketManager.Trigger(type, packet); //派发
                    }
                    break;
                case PacketType.S2C_Chat:
                    {
                        //MemoryStream stream = new MemoryStream(body, 0, body.Length); //解包
                        //TheMsg packet = ProtobufHelper.FromStream(typeof(TheMsg), stream) as TheMsg;
                        //Debug.Log($"[Handle:{type}] {packet.Name}说: {packet.Content}");
                        //NetPacketManager.Trigger(type, packet); //派发
                    }
                    break;
                default:
                    Debug.LogError($"Handle:无法识别的消息: {type}");
                    break;
            }
        }
    }

    public class NetPacketManager
    {
        public delegate void EventHandler(PacketType t, object packet);
        public static event EventHandler Event;
        public static void RegisterEvent(EventHandler action)
        {
            Event += action;
        }
        public static void UnRegisterEvent(EventHandler action)
        {
            Event -= action;
        }
        public static void Trigger(PacketType type, object packet)
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