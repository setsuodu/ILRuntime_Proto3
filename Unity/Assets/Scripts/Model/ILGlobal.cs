using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ILRuntime.Runtime.Enviorment;

namespace Client
{
    public class ILGlobal : UnitySingletonClass<ILGlobal>
    {
        public AppDomain appdomain;

        void Awake()
        {
            appdomain = new AppDomain();
            //InitializeILRuntime();
        }

        public void GlobalInit()
        {
            Debug.Log("进入ILR主逻辑");
            StartCoroutine(LoadHotFixAssembly());
        }

        IEnumerator LoadHotFixAssembly()
        {
            /*
#if UNITY_EDITOR
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix.dll");
#else
            //WWW www = new WWW(Application.streamingAssetsPath + "/HotFix.dll");
#endif
            while (!www.isDone) yield return null;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError($"未找到dll - {www.error}");
                yield break;
            }
            byte[] dll = www.bytes;
            www.Dispose();
            */

            // 从ab包加载dll
            yield return null;
            byte[] dll = AssetBundleManager.LoadDLL();

            var fs = new MemoryStream(dll);
            appdomain.LoadAssembly(fs, null, null);

            InitializeILRuntime();
            OnHotFixLoaded();
        }

        unsafe void InitializeILRuntime()
        {
            // 这里做一些ILRuntime的注册
            appdomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
            appdomain.RegisterCrossBindingAdaptor(new ProtobufAdapter());
            appdomain.RegisterCrossBindingAdaptor(new WidgetAdapter()); //注册跨域继承

            // 注册"空参空返回"型的委托
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>((act) => { return new UnityAction(() => { ((System.Action)act)(); }); });
        }

        void OnHotFixLoaded()
        {
            appdomain.Invoke("HotFix.PanelManager", "Test1", null, null);
            //appdomain.Invoke("HotFix.PanelManager", "Test2", gameObject, null);
            //appdomain.Invoke("HotFix.PanelManager", "Test3", gameObject, "123");
            var obj = appdomain.Invoke("HotFix.Proto3", "Test1", null, null);
            //appdomain.Invoke("HotFix.Proto3", "_TheMsg", null, null);

            GameObject music = new GameObject("IL_MusicManager");
            music.transform.SetParent(this.transform);
            var musicScript = music.AddComponent<ILMonoBehaviour>();
            musicScript.className = "MusicManager";
            musicScript.Run();

            GameObject panel = new GameObject("IL_PanelManager");
            panel.transform.SetParent(this.transform);
            var panelScript = panel.AddComponent<ILMonoBehaviour>();
            panelScript.className = "PanelManager";
            panelScript.Run();

            //通过ILR加载UI
            appdomain.Invoke("HotFix.PanelManager", "CreatePanel", gameObject, "UI_Login");
            //通过本地加载UI
            //PanelManager.Instance.CreatePanel("UI_Login");
        }
    }
}
