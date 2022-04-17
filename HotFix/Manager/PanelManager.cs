using System.Collections.Generic;
using UnityEngine;

namespace HotFix
{
    public class PanelManager : MonoBehaviour
    {
        public static PanelManager Instance;

        private Transform Parent;

        // UI存储栈
        List<UIWidget> stack = new List<UIWidget>();
        List<UIWidget> recyclePool = new List<UIWidget>();

        void Awake()
        {
            Instance = this;
            Parent = GameObject.Find("Canvas").transform;
            Debug.Log($"Parent:::{Parent.name}");
        }

        public void GetActivePanel() { }
        public void GetPanel(string className) { }

        public void CreatePanel(string className)
        {
            Debug.Log($"[Hotfix] CreatePanel: {className}");

            string fileName = $"prefabs/ui/{className.ToLower()}.unity3d";
            //GameObject go = AssetBundleManager.LoadGameObject(fileName, Parent);
            GameObject go = AssetBundleManager.LoadGameObject(fileName, GameObject.Find("Canvas").transform);
            go.name = className;
            //go.layer = LayerMask.NameToLayer("UI");
            //go.transform.parent = GameObject.Find("Canvas").transform;
            //go.transform.localPosition = Vector3.zero;
            //go.transform.localRotation = Quaternion.identity;
            //go.transform.localScale = Vector3.one;

            //if (func != null) func.Call(go);

            //Debug.Log($"create: go:{go.name}");
            //Debug.Log($"create: Parent:{Parent != null}");
        }

        public void ClosePanel(string className)
        {
            Transform panelObj = Parent.Find(className);
            if (panelObj == null) return;
            Destroy(panelObj.gameObject);
        }
        public void ClosePanel(UIWidget widget)
        {
            Debug.Log("ClosePanel...");
            Destroy(widget.gameObject);
            Debug.Log($"close: widget:{widget.name}");
            Debug.Log($"close: Parent:{Parent.name}");
        }

        public void CloseAll(string className) { }
        public void CloseAllWithout(string className) { }

        #region 测试

        public static void Test1()
        {
            Debug.Log("PanelManager中的Test1");
        }

        public void Test2()
        {
            Debug.Log("Test2");
        }

        public void Test3(string value)
        {
            Debug.Log("Test3: " + value);
        }

        #endregion
    }
}