using System;
using System.Threading;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using ET;

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
            //Debug.Log($"Client.OnReceived length={buffer.Length}, offset={offset}, size={size}");

            // 这里是异步线程中，需要通过Update推送到主线程。
            Array.Resize<byte>(ref buffer, (int)size); //8192裁剪
            EventManager.Get().queue.Enqueue(buffer);
        }

        protected override void OnError(SocketError error)
        {
            Debug.Log($"Chat TCP client caught an error with code {error}");
        }

        private bool _stop;
    }

    public class TcpChatClient
    {
        protected static ChatClient client;
        const string address = "127.0.0.1";
        const int port = 1111;

        public static void Dispose()
        {
            Debug.Log("关闭网络");
            client?.DisconnectAndStop();
            client = null;
        }
        public static void Connect()
        {
            // Create a new TCP chat client
            client = new ChatClient(address, port);

            // Connect the client
            client.ConnectAsync();
            Debug.Log($"connect to: {address}:{port}");
        }
        public static void Reconnect()
        {
            client.Reconnect();
            Debug.Log($"reconnect to: {address}:{port}");
        }
        public static void Disconnect()
        {
            //if (client == null)
            //    return;
            client?.DisconnectAndStop();
        }

        public static void Send(PacketType msgId, object cmd)
        {
            byte[] header = new byte[1] { (byte)msgId };
            byte[] body = ProtobufHelper.ToBytes(cmd);
            byte[] buffer = new byte[header.Length + body.Length];
            System.Array.Copy(header, 0, buffer, 0, header.Length);
            System.Array.Copy(body, 0, buffer, header.Length, body.Length);
            //Debug.Log($"[Send] header:{header.Length},body:{body.Length},buffer:{buffer.Length},");
            client.Send(buffer);
        }
        public static void SendAsync(PacketType msgId, object cmd)
        {
            byte[] header = new byte[1] { (byte)msgId };
            byte[] body = ProtobufHelper.ToBytes(cmd);
            byte[] buffer = new byte[header.Length + body.Length];
            System.Array.Copy(header, 0, buffer, 0, header.Length);
            System.Array.Copy(body, 0, buffer, header.Length, body.Length);
            Debug.Log($"[SendAsync] header:{header.Length},body:{body.Length},buffer:{buffer.Length},");
            client.SendAsync(buffer);
        }
        public static void SendAsync(byte[] buffer)
        {
            //临时的，要删除
        }

        public static void SendLogin(string usr, string pwd)
        {
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pwd))
            {
                Debug.LogError($"用户名或密码不能为空"); //TODO: Toast
                return;
            }
            if (pwd.Length < 6)
            {
                Debug.LogError($"密码长度过短"); //TODO: Toast
                return;
            }
            //TODO: 服务器/客户端共用规则，双边验证...
            
            C2S_Login cmd = new C2S_Login { Username = usr, Password = pwd };
            SendAsync(PacketType.C2S_LoginReq, cmd);
        }
        public static void SendChat(string message)
        {
            if (message.Length <= 0)
            {
                Debug.LogError($"内容为空"); //TODO: Toast
                return;
            }
            /*
            TheMsg cmd = new TheMsg { Name = "lala", Content = message };
            SendAsync(PacketType.C2S_Chat, cmd);*/
        }
        public static bool SendCreateRoom(string name, string pwd, int num)
        {
            if (name.Length < 3)
            {
                Debug.LogError($"房间名称至少3个字:{name.Length}"); //TODO: Toast
                return false;
            }
            /*
            C2S_CreateRoom cmd = new C2S_CreateRoom { RoomName = name, RoomPwd = pwd, LimitNum = num };
            SendAsync(PacketType.C2S_CreateRoom, cmd);*/
            return true;
        }
        public static void SendJoinRoom(int roomId, string pwd)
        {
            /*
            C2S_JoinRoom cmd = new C2S_JoinRoom { RoomId = roomId, RoomPwd = pwd };
            SendAsync(PacketType.C2S_LeaveRoom, cmd);*/
        }
        public static void SendLeaveRoom()
        {
            /*
            Empty cmd = new Empty();
            SendAsync(PacketType.C2S_LeaveRoom, cmd);*/
        }
        public static void SendGetRoomList()
        {
            /*
            Empty cmd = new Empty();
            SendAsync(PacketType.C2S_RoomList, cmd);*/
        }
    }
}