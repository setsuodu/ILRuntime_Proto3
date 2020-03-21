using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix.View
{
    public class UI_Login : UIWidget
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
        }

        void OnHelpBtnClick()
        {
            Debug.Log("[Hotfix] 关闭");
            //Client.WidgetManager.Instance.Pop(); //热更工程跨域调用Client代码
            PanelManager.Instance.ClosePanel(this);
        }

        void OnQQBtnClick()
        {
            Debug.Log("[Hotfix] QQ登录");
        }

        void OnWXBtnClick()
        {
            Debug.Log("[Hotfix] 微信登录");
        }

        void OnRegisterBtnClick()
        {
            Debug.Log("[Hotfix] 去注册");
            PanelManager.Instance.CreatePanel("UI_Register");
        }
    }
}
