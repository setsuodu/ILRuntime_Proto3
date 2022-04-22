using UnityEngine;
using UnityEngine.UI;

namespace HotFix
{
    public class UI_Login : UIBase
    {
        public Button m_HelpBtn;
        public Button m_QQBtn;
        public Button m_WXBtn;
        public Button m_LoginBtn;
        public Button m_RegisterBtn;

        void Awake()
        {
            m_HelpBtn = transform.Find("HelpButton").GetComponent<Button>();
            m_QQBtn = transform.Find("QQButton").GetComponent<Button>();
            m_WXBtn = transform.Find("WXButton").GetComponent<Button>();
            m_LoginBtn = transform.Find("LoginBtn").GetComponent<Button>();
            m_RegisterBtn = transform.Find("RegisterBtn").GetComponent<Button>();

            m_HelpBtn.onClick.AddListener(OnHelpBtnClick);
            m_QQBtn.onClick.AddListener(OnQQBtnClick);
            m_WXBtn.onClick.AddListener(OnWXBtnClick);
            m_LoginBtn.onClick.AddListener(OnLoginBtnClick);
            m_RegisterBtn.onClick.AddListener(OnRegisterBtnClick);
        }

        void Start()
        {
            NetPacketManager.RegisterEvent(OnNetCallback);
            TcpChatClient.Connect();
        }

        void OnDestroy()
        {
            NetPacketManager.UnRegisterEvent(OnNetCallback);

            m_HelpBtn.onClick.RemoveListener(OnHelpBtnClick);
            m_QQBtn.onClick.RemoveListener(OnQQBtnClick);
            m_WXBtn.onClick.RemoveListener(OnWXBtnClick);
            m_LoginBtn.onClick.RemoveListener(OnLoginBtnClick);
            m_RegisterBtn.onClick.RemoveListener(OnRegisterBtnClick);

            Debug.Log("释放临时变量");
            m_HelpBtn = null;
            m_QQBtn = null;
            m_WXBtn = null;
            m_LoginBtn = null;
            m_RegisterBtn = null;
        }

        public void OnNetCallback(PacketType type, object packet)
        {
            switch (type)
            {
                case PacketType.S2C_LoginResult:
                    {
                        UIManager.Get().Push<UI_Main>(); //成功回调中执行
                        break;
                    }
            }
        }

        void OnHelpBtnClick()
        {
            TcpChatClient.Disconnect();
        }

        void OnQQBtnClick()
        {
            Debug.Log("[Hotfix] QQ登录");
        }

        void OnWXBtnClick()
        {
            Debug.Log("[Hotfix] 微信登录");

            TcpChatClient.Disconnect();
        }

        void OnLoginBtnClick()
        {
            TcpChatClient.SendLogin("lala", "123456");
        }

        void OnRegisterBtnClick()
        {
            UIManager.Get().Push<UI_Register>();
        }
    }
}