using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix
{
    public class UI_Main : UIBase
    {
        public Button m_CloseBtn;

        void Awake()
        {
            Debug.Log("<color=yellow>UI_Main.Awake</color>");

            m_CloseBtn = transform.Find("CloseButton").GetComponent<Button>();

            m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
        }

        void OnCloseBtnClick()
        {
            Debug.Log("OnCloseBtnClick");
        }
    }
}