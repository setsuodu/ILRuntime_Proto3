using System;
using System.IO;
using Google.Protobuf;

namespace HotFix
{
    public class ProtobufferTool
    {
        // 序列化protobuf
        public static byte[] Serialize(IMessage msg)
        {
            using (MemoryStream rawOutput = new MemoryStream())
            {
                CodedOutputStream output = new CodedOutputStream(rawOutput);
                output.WriteMessage(msg);
                output.Flush();
                byte[] result = rawOutput.ToArray();
                return result;
            }
        }

        // 反序列化protobuf
        public static T Deserialize<T>(byte[] dataBytes) where T : IMessage, new()
        {
            CodedInputStream stream = new CodedInputStream(dataBytes);
            T msg = new T();
            try
            {
                stream.ReadMessage(msg);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("接收错误：" + e.ToString());

                //发过来的是utf8-string
                string str = System.Text.Encoding.UTF8.GetString(dataBytes);
                Console.WriteLine(str);
            }
            return msg;
        }
    }
}