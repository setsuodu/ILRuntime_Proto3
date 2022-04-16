using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;

namespace HotFix
{
    public class Main
    {
        #region 测试

        //public static void MyDemoAdapter(GameObject go)
        //{
        //    go.AddComponent<MyDemo>();
        //}

        //public static void YourDemoAdapter(GameObject go)
        //{
        //    go.AddComponent<YourDemo>();
        //    YourDemo loader = go.GetComponent<YourDemo>();

        //    Debug.Log("<color=red>YourDemo.YourDemoTest mb= </color>" + loader);
        //    loader.Test();
        //}

        public static void Print() 
        {
            Debug.Log("Test Print");
        }

        public void Proto()
        {
            ///*
            #region Class序列化成二进制
            //var msg = new Tutorial.TheMsg();
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
            //var msg2 = Tutorial.TheMsg.Parser.ParseFrom(bmsg);
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

        #region UI

        public static void UI_LoginAdapter(GameObject go)
        {
            go.AddComponent<View.UI_Login>();
        }

        public static void UI_RegisterAdapter(GameObject go)
        {
            go.AddComponent<View.UI_Register>();
        }

        #endregion

        #region Manager

        public static void MusicManagerAdapter(GameObject go)
        {
            go.AddComponent<MusicManager>();
        }

        public static void PanelManagerAdapter(GameObject go)
        {
            go.AddComponent<PanelManager>();
        }

        #endregion
    }
}