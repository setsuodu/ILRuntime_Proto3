using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix.View
{
    public class UI_Register : UIWidget
    {
        public Button m_CloseBtn;

        void Awake()
        {
            Debug.Log("UI_Register.Awake");

            m_CloseBtn = transform.Find("CloseButton").GetComponent<Button>();

            m_CloseBtn.onClick.AddListener(OnRegisterBtnClick);
        }

        void OnDestroy()
        {
            m_CloseBtn.onClick.RemoveListener(OnRegisterBtnClick);

            m_CloseBtn = null;
        }

        void OnRegisterBtnClick()
        {
            //this.PopUp();
            PanelManager.Instance.ClosePanel(this);
            Debug.Log("关闭");
        }
    }
}