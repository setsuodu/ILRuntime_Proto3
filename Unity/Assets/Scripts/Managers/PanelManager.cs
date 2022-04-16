using UnityEngine;

namespace Client
{
    public class PanelManager : MonoBehaviour
    {
        public static PanelManager Instance;

        public Transform Parent;

        void Awake()
        {
            Instance = this;

            Parent = GameObject.Find("Canvas").transform;
        }

        // UI控制，走ILR接口

        //public void CreatePanel(string className, XLua.LuaFunction func = null)
        //{
        //    var uiRoot = GameObject.Find("Canvas").transform;
        //    string fileName = string.Format("prefabs/ui/{0}.unity3d", className);
        //    GameObject go = AssetBundleManager.LoadGameObject(fileName, uiRoot);
        //    //GameObject prefab = Resources.Load<GameObject>("Prefabs/" + className);
        //    //GameObject go = Instantiate(prefab) as GameObject;

        //    go.name = className;
        //    go.layer = LayerMask.NameToLayer("Default");
        //    go.transform.parent = Parent;
        //    go.transform.localScale = Vector3.one;
        //    go.transform.localPosition = Vector3.zero;

        //    //go.AddComponent<XLuaTest.LuaAdapter>();

        //    if (func != null) func.Call(go);
        //}

        public void CreatePanel(string className, System.Action func = null)
        {
            string fileName = $"prefabs/ui/{className.ToLower()}.unity3d";
            GameObject go = AssetBundleManager.LoadGameObject(fileName, Parent);

            go.name = className;
            go.layer = LayerMask.NameToLayer("Default");
            go.transform.parent = Parent;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;

            //if (func != null) func.Call(go);
        }

        public void ClosePanel(string className)
        {
            var panelObj = Parent.Find(className);
            if (panelObj == null) return;
            Destroy(panelObj.gameObject);
        }
    }
}