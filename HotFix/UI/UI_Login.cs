using ET;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix
{
    public class UI_Login : UIBase
    {
        public Button m_HelpBtn;
        public Button m_QQBtn;
        public Button m_WXBtn;
        public Button m_RegisterBtn;

        void Awake()
        {
            m_HelpBtn = transform.Find("HelpButton").GetComponent<Button>();
            m_QQBtn = transform.Find("QQButton").GetComponent<Button>();
            m_WXBtn = transform.Find("WXButton").GetComponent<Button>();
            m_RegisterBtn = transform.Find("RegisterBtn").GetComponent<Button>();

            m_HelpBtn.onClick.AddListener(OnHelpBtnClick);
            m_QQBtn.onClick.AddListener(OnQQBtnClick);
            m_WXBtn.onClick.AddListener(OnWXBtnClick);
            m_RegisterBtn.onClick.AddListener(OnRegisterBtnClick);
        }

        void OnDestroy()
        {
            m_HelpBtn.onClick.RemoveListener(OnHelpBtnClick);
            m_QQBtn.onClick.RemoveListener(OnQQBtnClick);
            m_WXBtn.onClick.RemoveListener(OnWXBtnClick);
            m_RegisterBtn.onClick.RemoveListener(OnRegisterBtnClick);

            Debug.Log("释放临时变量");
            m_HelpBtn = null;
            m_QQBtn = null;
            m_WXBtn = null;
            m_RegisterBtn = null;
        }

        void OnHelpBtnClick()
        {
            /*
            TheMsg cmd = new TheMsg { Name = "lala", Content = "say hello" };
            byte msgId = (byte)PacketType.C2S_LoginReq;
            byte[] header = new byte[1] { msgId };
            byte[] body = ProtobufferTool.Serialize(cmd);
            byte[] buffer = new byte[header.Length + body.Length];
            Array.Copy(header, 0, buffer, 0, header.Length);
            Array.Copy(body, 0, buffer, header.Length, body.Length);
            //Debug.Log($"header:{header.Length},body:{body.Length},buffer:{buffer.Length},");
            TcpHelper.SendAsync(buffer);
            */
            TheMsgList list = new TheMsgList();
            list.Id = 12;
            list.Content.Add("hello");
            list.Content.Add("world");
            Debug.Log($"list={list.Content.Count}");
            MemoryStream memoryStream = new MemoryStream();
            ProtobufHelper.ToStream(list, memoryStream);
            byte[] body = memoryStream.ToArray();
            Debug.Log($"序列化: body={body.Length}"); //16
            FileHelper.WriteBytes(body);
            TcpHelper.SendAsync(body);

            /*
            byte msgId = (byte)PacketType.C2S_LoginReq;
            byte[] header = new byte[1] { msgId };
            //Debug.Log($"序列化: body={body.Length}");

            byte[] buffer = new byte[header.Length + body.Length];
            Array.Copy(header, 0, buffer, 0, header.Length);
            Array.Copy(body, 0, buffer, header.Length, body.Length);
            TcpHelper.SendAsync(buffer);*/
        }

        void OnQQBtnClick()
        {
            Debug.Log("[Hotfix] QQ登录");

            TcpHelper.Connect();
        }

        void OnWXBtnClick()
        {
            Debug.Log("[Hotfix] 微信登录");

            TcpHelper.Disconnect();
        }

        void OnRegisterBtnClick()
        {
            UIManager.Get().Push<UI_Register>();
        }
    }
}