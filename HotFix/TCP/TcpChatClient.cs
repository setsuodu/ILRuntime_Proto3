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
            Debug.Log($"OnReceived buffer={buffer.Length}, offset={offset}, size={size}");
            try
            {
                MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length);
                var obj = ProtobufHelper.FromStream(typeof(TheMsgList), stream) as TheMsgList;
                Debug.Log($"�����л�: obj={obj.Content[1]}");
            }
            catch (Exception e)
            {
                Debug.LogError($"error: {e.ToString()}");
            }
            return;

            //string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            //Debug.Log($"S2C: {message}({size})");

            // ����msgId
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
            //TODO: ͨ��ί�зַ���ȥ
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
            Debug.Log("�ر�����");
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

        public static void Send(PacketType msgId, IMessage cmd)
        {
            /*
            byte[] header = new byte[1] { (byte)msgId };
            byte[] body = ProtobufferTool.Serialize(cmd);
            byte[] buffer = new byte[header.Length + body.Length];
            System.Array.Copy(header, 0, buffer, 0, header.Length);
            System.Array.Copy(body, 0, buffer, header.Length, body.Length);
            //Debug.Log($"[Send] header:{header.Length},body:{body.Length},buffer:{buffer.Length},");
            client.Send(buffer);*/
        }
        public static void SendAsync(PacketType msgId, IMessage cmd)
        {
            /*
            byte[] header = new byte[1] { (byte)msgId };
            byte[] body = ProtobufferTool.Serialize(cmd);
            byte[] buffer = new byte[header.Length + body.Length];
            System.Array.Copy(header, 0, buffer, 0, header.Length);
            System.Array.Copy(body, 0, buffer, header.Length, body.Length);
            Debug.Log($"[SendAsync] header:{header.Length},body:{body.Length},buffer:{buffer.Length},");
            client.SendAsync(buffer);*/
        }
        public static void SendAsync(byte[] buffer)
        {
            //��ʱ�ģ�Ҫɾ��
        }

        public static void SendLogin(string usr, string pwd)
        {
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pwd))
            {
                Debug.LogError($"�û��������벻��Ϊ��"); //TODO: Toast
                return;
            }
            if (pwd.Length < 6)
            {
                Debug.LogError($"���볤�ȹ���"); //TODO: Toast
                return;
            }
            //TODO: ������/�ͻ��˹��ù���˫����֤...
            /*
            C2S_Login cmd = new C2S_Login { Username = usr, Password = pwd };
            SendAsync(PacketType.C2S_LoginReq, cmd);*/
        }
        public static void SendChat(string message)
        {
            if (message.Length <= 0)
            {
                Debug.LogError($"����Ϊ��"); //TODO: Toast
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
                Debug.LogError($"������������3����:{name.Length}"); //TODO: Toast
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