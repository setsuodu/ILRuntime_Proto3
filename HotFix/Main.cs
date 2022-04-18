using UnityEngine;
using Google.Protobuf;

namespace HotFix
{
    public class Main
    {
        #region 测试
        public static void Print() 
        {
            Debug.Log("Test Print");
        }
        public void Proto()
        {
            ///*
            #region Class序列化成二进制
            var msg = new HotFix.TheMsg();
            msg.Name = "am the name";
            msg.Content = "haha";
            Debug.Log(string.Format("The Msg is ( Name:{0}, Num:{1} )", msg.Name, msg.Content));

            byte[] bmsg;
            using (var ms = new System.IO.MemoryStream())
            {
                msg.WriteTo(ms);
                bmsg = ms.ToArray();
            }
            #endregion

            #region 二进制反序列化成Class
            var msg2 = HotFix.TheMsg.Parser.ParseFrom(bmsg);
            Debug.Log(string.Format("The Msg2 is ( Name:{0}, Num:{1} )", msg2.Name, msg2.Content));
            #endregion
            //*/

            /*
            var msg = new HotFix.TheMsg();
            msg.Name = "am the name";
            msg.Content = "haha";
            Debug.Log(string.Format("The Msg is ( Name:{0}, Num:{1} )", msg.Name, msg.Content));

            byte[] bytes = ProtoHelper.Serialize(msg);

            var msg2 = ProtoHelper.Deserialize<HotFix.TheMsg>(bytes);
            Debug.Log(string.Format("The Msg2 is ( Name:{0}, Num:{1} )", msg2.Name, msg2.Content));
            */
        }
        #endregion

        #region Adapter

        // 所有UI注册在这里
        public static void UI_LoginAdapter(GameObject go)
        {
            //var ui = go.AddComponent<UI_Login>();
            Debug.Log($"AddComponent<UI_Login>");
        }
        public static void UI_RegisterAdapter(GameObject go)
        {
            //var ui = go.AddComponent<UI_Register>();
            Debug.Log($"AddComponent<UI_Register>");
        }
        public static void UI_MainAdapter(GameObject go)
        {
            //var ui = go.AddComponent<UI_Main>();
            Debug.Log($"AddComponent<UI_Main>");
        }

        // Manager注册在这里
        public static void MusicManagerAdapter(GameObject go)
        {
            go.AddComponent<MusicManager>();
        }
        public static void UIManagerAdapter(GameObject go)
        {
            go.AddComponent<UIManager>();
        }

        #endregion

        public static void Init()
        {
            //UIManager.Get().CreatePanel("UI_Login");
            UIManager.Get().Push<UI_Login>();
        }
    }
}