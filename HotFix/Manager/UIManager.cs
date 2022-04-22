using System.Collections.Generic;
using UnityEngine;

namespace HotFix
{
    public class UIManager : MonoBehaviour
    {
        static UIManager _instance;
        public static UIManager Get()
        {
            return _instance;
        }

        public Transform Parent;

        // UI存储栈
        public Dictionary<string, UIBase> stack;
        public Dictionary<string, UIBase> recyclePool;

        void Awake()
        {
            _instance = this;
            Parent = GameObject.Find("Canvas").transform;
            stack = new Dictionary<string, UIBase>();
            recyclePool = new Dictionary<string, UIBase>();
        }

        public void GetActivePanel() { }
        public void GetPanel(string className) { }

        public void CreatePanel(string className)
        {
            Debug.Log($"[Hotfix] CreatePanel={className}");

            string fileName = $"ui/{className.ToLower()}";
            GameObject prefab = Client.ResManager.LoadPrefab(fileName);
            GameObject go = Instantiate(prefab, Parent);
            go.name = className;
            //go.layer = LayerMask.NameToLayer("UI");

            //var script = go.GetComponent<Client.ILWidget>();
            //stack.Add(script);
            //Debug.Log($"Parent={Parent.name}");
        }
        public T Push<T>() where T : UIBase
        {
            string scriptName = typeof(T).ToString().Split('.')[1];
            //Debug.Log($"Push<{scriptName}>");
            UIBase ui = null;
            if (stack.TryGetValue(scriptName, out ui))
            {
                return ui.GetComponent<T>();
            }
            if (recyclePool.TryGetValue(scriptName, out ui))
            {
                recyclePool.Remove(scriptName);
                stack.Add(scriptName, ui);
                Debug.Log($"<color=yellow>ReUse{stack.Count}/{recyclePool.Count}</color>");
                ui.gameObject.SetActive(true);
                ui.transform.SetAsLastSibling();
                return ui.GetComponent<T>();
            }
            else
            {
                GameObject prefab = Client.ResManager.LoadPrefab($"ui/{scriptName}");
                GameObject obj = Instantiate(prefab, Parent);
                obj.transform.localPosition = Vector3.zero;
                obj.name = scriptName;

                if (obj.GetComponent<T>() == false)
                    obj.AddComponent<T>();
                var script = obj.GetComponent<T>();
                stack.Add(scriptName, script);
                Debug.Log($"<color=yellow>New{stack.Count}/{recyclePool.Count}</color>");
                return script;
            }
        }

        public void ClosePanel(string className)
        {
            Transform panelObj = Parent.Find(className);
            if (panelObj == null) return;
            Destroy(panelObj.gameObject);
        }
        public void ClosePanel(UIBase widget)
        {
            Debug.Log("ClosePanel...");
            Destroy(widget.gameObject);
        }
        public void Pop(UIBase ui)
        {
            string scriptName = ui.name;
            if (ui == null)
            {
                Debug.LogError("没有需要销毁的UI");
                return;
            }
            //Debug.Log($"Pop: {scriptName}");
            stack.Remove(scriptName);
            //Destroy(ui.gameObject);
            //Debug.Log($"recyclePool={recyclePool.Count}");
            recyclePool.Add(scriptName, ui);
            ui.gameObject.SetActive(false);
        }
        public void PopAll()
        {
            foreach (var ui in stack)
            {
                Pop(ui.Value);
            }
        }

        public void CloseAll(string className) { }
        public void CloseAllWithout(string className) { }

        #region 测试

        public static void Test1()
        {
            Debug.Log("UIManager中的Test1");
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