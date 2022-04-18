using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix
{
    public class UI_Register : UIBase
    {
        public Button m_CloseBtn;

        void Awake()
        {
            m_CloseBtn = transform.Find("CloseButton").GetComponent<Button>();

            m_CloseBtn.onClick.AddListener(OnRegisterBtnClick);

            //TheMsg msg = new TheMsg
            //{
            //    Name = "Jack",
            //    Content = "Update",
            //};
            //Debug.Log($"Test proto: {msg.Name}, {msg.Content}");
        }

        void OnDestroy()
        {
            m_CloseBtn.onClick.RemoveListener(OnRegisterBtnClick);

            m_CloseBtn = null;
        }

        void OnRegisterBtnClick()
        {
            //this.PopUp(); //报错
            //UIManager.Get().ClosePanel(this);
            UIManager.Get().Pop(this);
        }
    }
}