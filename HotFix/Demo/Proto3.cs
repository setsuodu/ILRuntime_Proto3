using UnityEngine;

namespace HotFix
{
    public class Proto3
    {
        public static void Test1()
        {
            Debug.Log("Proto3中的Test1");

            TheMsg msg = new TheMsg();
            msg.Name = "lala";
            msg.Content = "haha";
            Debug.Log($"Name={msg.Name}，Content={msg.Content}");

            byte[] body = ProtobufferTool.Serialize(msg);
            Debug.Log($"body={body.Length}");
        }
    }
}