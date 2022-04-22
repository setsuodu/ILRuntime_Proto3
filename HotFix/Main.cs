using System.IO;
using UnityEngine;
using ET;

namespace HotFix
{
    public class Main
    {
        #region 测试
        public static void Proto()
        {
            Debug.Log("测试Proto!");
            TheMsgList list = new TheMsgList();
            list.Id = 123;
            list.Content.Add("111");
            list.Content.Add("abc");
            list.Content.Add("xyz");
            Debug.Log($"list={list.Content.Count}");
            byte[] bytes2 = ProtobufHelper.ToBytes(list);
            Debug.Log($"序列化: bytes2={bytes2.Length}"); //17


            MemoryStream stream = new MemoryStream(bytes2, 0, bytes2.Length);
            var obj = ProtobufHelper.FromStream(typeof(TheMsgList), stream) as TheMsgList;
            Debug.Log($"反序列化: obj={obj.Content[1]}");
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
        public static void UIManagerAdapter(GameObject go)
        {
            go.AddComponent<UIManager>();
        }
        public static void EventManagerAdapter(GameObject go)
        {
            go.AddComponent<EventManager>();
        }

        #endregion

        public static void Init()
        {
            UIManager.Get().Push<UI_Login>();
        }
    }
}