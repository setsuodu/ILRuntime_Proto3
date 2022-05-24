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
            MemoryStream stream = new MemoryStream(body, 0, body.Length);
            Debug.Log($"<color=yellow>PacketType={type}</color>");
            switch (type)
            {
                case PacketType.Connected:
                    break;
                case PacketType.S2C_LoginResult:
                    {
                        var packet = ProtobufHelper.Deserialize<S2C_Login>(stream); //解包
                        NetPacketManager.Trigger(type, packet); //派发
                        break;
                    }
                case PacketType.S2C_Chat:
                    {
                        var packet = ProtobufHelper.Deserialize<TheMsg>(stream);
                        NetPacketManager.Trigger(type, packet);
                        break;
                    }
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