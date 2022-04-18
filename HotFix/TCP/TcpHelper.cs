using Debug = UnityEngine.Debug;

namespace HotFix
{
    public class TcpHelper : System.IDisposable
    {
        protected static ChatClient client;
        const string address = "127.0.0.1";
        const int port = 1111;

        public static void Connect()
        {
            // Create a new TCP chat client
            client = new ChatClient(address, port);

            // Connect the client
            client.ConnectAsync();
            Debug.Log($"connect to: {address}:{port}");
        }

        public static void Disconnect()
        {
            //if (client == null)
            //    return;
            client?.DisconnectAndStop();
        }

        public static void SendAsync(byte[] buffer)
        {
            client.SendAsync(buffer);
        }
        public static void SendAsync(string content)
        {
            client.SendAsync(content);
        }

        public void Dispose()
        {
            client.DisconnectAndStop();
        }
    }
}