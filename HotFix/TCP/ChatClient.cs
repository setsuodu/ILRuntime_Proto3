using System;
using System.Threading;
using System.Net.Sockets;
using UnityEngine;

namespace HotFix
{
    public class ChatClient : TcpClient
    {
        public ChatClient(string address, int port) : base(address, port) { }

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected()
        {
            Debug.Log($"Chat TCP client connected a new session with Id {Id}");
        }

        protected override void OnDisconnected()
        {
            Debug.Log($"Chat TCP client disconnected a session with Id {Id}");

            // Wait for a while...
            Thread.Sleep(1000);

            // Try to connect again
            if (!_stop)
                ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            //string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            //Debug.Log($"S2C: {message}({size})");

            // 解析msgId
            byte msgId = buffer[0];
            byte[] body = new byte[buffer.Length - 1];
            Array.Copy(buffer, 1, body, 0, buffer.Length - 1);

            PacketType type = (PacketType)msgId;
            Debug.Log($"msgId={msgId}");

            switch (type)
            {
                case PacketType.Connected:
                    break;
                case PacketType.C2S_LoginReq:
                    //TheMsg msg = ProtobufferTool.Deserialize<TheMsg>(body);
                    //Debug.Log($"[{type}] Name={msg.Name}, Content={msg.Content}");
                    break;
            }
            //TODO: 通过委托分发出去
        }

        protected override void OnError(SocketError error)
        {
            Debug.Log($"Chat TCP client caught an error with code {error}");
        }

        private bool _stop;
    }
}