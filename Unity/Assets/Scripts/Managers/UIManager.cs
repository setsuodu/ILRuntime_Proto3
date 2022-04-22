using System.Collections.Generic;
using UnityEngine;

// 全局配置。不放场景中，通过脚本创建。
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Get()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<UIManager>(); //场景中有，找
        }
        if (_instance == null)
        {
            var prefab = Client.ResManager.LoadPrefab($"Prefabs/UIManager");
            var obj = Instantiate(prefab);
            obj.name = "UIManager";
            _instance = obj.GetComponent<UIManager>();
            DontDestroyOnLoad(_instance.gameObject);
        }
        return _instance;
    }

    private Transform _parent;
    private Transform Parent
    {
        get
        {
            if (_parent == null)
                _parent = transform.Find("LobbyCanvas");
            return _parent;
        }
    }
    private Transform _top;
    private Transform Top
    {
        get
        {
            if (_top == null)
                _top = transform.Find("TopCanvas");
            return _top;
        }
    }

    // 给UI的委托
    //public static ShowSkillText doShowSkillText;
    //public static SetTimeText doSetTimeText;
    //public static SetCurrentHp doSetCurrentHp;

    // UI存储栈
    [SerializeField] List<UIBase> stack = new List<UIBase>();
    [SerializeField] List<UIBase> recyclePool = new List<UIBase>();

    // 获取当前顶层UI
    public System.Type GetActiveUI()
    {
        UIBase ui = stack[stack.Count - 1];
        var type = ui.GetType();
        return type;
    }

    public T GetUI<T>() where T : UIBase
    {
        string scriptName = typeof(T).ToString();
        var ui = stack.Find(x => x.name == scriptName);
        if (ui == null)
        {
            Debug.LogError($"还没有创建：{scriptName}");
        }
        return ui.GetComponent<T>();
    }

    public T Push<T>(int layer = 1) where T : UIBase
    {
        string scriptName = typeof(T).ToString();
        UIBase ui = stack.Find(x => x.name == scriptName);
        if (ui != null)
        {
            //Debug.LogError($"{scriptName}已经在栈里，不再Push");
            return ui.GetComponent<T>();
        }
        ui = recyclePool.Find(x => x.name == scriptName);
        if (ui != null)
        {
            //Debug.LogError($"{scriptName}重新利用");
            recyclePool.Remove(ui);
            stack.Add(ui);
            ui.gameObject.SetActive(true);
            ui.transform.SetAsLastSibling();

            return ui.GetComponent<T>();
        }
        else
        {
            GameObject prefab = Client.ResManager.LoadPrefab($"ui/{scriptName}");
            Transform p = layer == 1 ? Parent : Top;
            GameObject obj = Instantiate(prefab, p);
            obj.transform.localPosition = Vector3.zero;
            obj.name = scriptName;

            if (obj.GetComponent<T>() == false)
                obj.AddComponent<T>();
            var script = obj.GetComponent<T>();
            stack.Add(script);

            return script;
        }
    }

    public void Pop(UIBase ui)
    {
        if (ui == null)
        {
            Debug.LogError("没有需要销毁的UI");
            return;
        }
        stack.Remove(ui);
        //Destroy(ui.gameObject);
        recyclePool.Add(ui);
        ui.gameObject.SetActive(false);
    }

    public void PopAll()
    {
        for (int i = stack.Count - 1; i >= 0; i--)
        {
            UIBase ui = stack[i];
            Pop(ui);
        }
    }

    public void PopAllWithout(UIBase keep)
    {
        //TODO: 有Bug，调试
        foreach (UIBase item in stack)
        {
            if (item.Equals(keep))
            {
                Debug.Log($"保留：{keep.name}");
                continue;
            }
            Pop(item);
        }
    }
}