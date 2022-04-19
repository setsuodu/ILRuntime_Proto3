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
        }

        public void GlobalInit()
        {
            StartCoroutine(LoadHotFixAssembly());
        }

        // 从ab包加载dll
        IEnumerator LoadHotFixAssembly()
        {
            yield return null;

            byte[] dll = ResManager.LoadDLL();
            var fs = new MemoryStream(dll);
            appdomain.LoadAssembly(fs, null, null);

            InitializeILRuntime();
            OnHotFixLoaded();
        }

        // 注册类、委托、跨域继承(Adaptor)
        unsafe void InitializeILRuntime()
        {
            // 这里做一些ILRuntime的注册
            appdomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
            appdomain.RegisterCrossBindingAdaptor(new ProtobufAdapter());
            appdomain.RegisterCrossBindingAdaptor(new Adapt_IMessage());
            appdomain.RegisterCrossBindingAdaptor(new UIBaseAdapter()); //注册跨域继承
            appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter()); //注册System.IDisposable, IEnumerator

            // 注册"空参空返回"型的委托
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>((act) => { return new UnityAction(() => { ((System.Action)act)(); }); });
            appdomain.DelegateManager.RegisterFunctionDelegate<UIBase, System.Boolean>();
            appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<UIBase>>((act) =>
            {
                return new System.Predicate<UIBase>((obj) =>
                {
                    return ((System.Func<UIBase, System.Boolean>)act)(obj);
                });
            });
            appdomain.DelegateManager.RegisterMethodDelegate<System.Object, System.Net.Sockets.SocketAsyncEventArgs>();
            appdomain.DelegateManager.RegisterDelegateConvertor<System.EventHandler<System.Net.Sockets.SocketAsyncEventArgs>>((act) =>
            {
                return new System.EventHandler<System.Net.Sockets.SocketAsyncEventArgs>((sender, e) =>
                {
                    ((System.Action<System.Object, System.Net.Sockets.SocketAsyncEventArgs>)act)(sender, e);
                });
            });
        }

        // 加载完成，调用ILR代码
        void OnHotFixLoaded()
        {
            /* 测试调用
            appdomain.Invoke("HotFix.UIManager", "Test1", null, null);
            appdomain.Invoke("HotFix.UIManager", "Test2", gameObject, null);
            appdomain.Invoke("HotFix.UIManager", "Test3", gameObject, "123");
            var obj = appdomain.Invoke("HotFix.Proto3", "Test1", null, null);
            appdomain.Invoke("HotFix.Proto3", "_TheMsg", null, null);
            */

            //IL_InitAdapter("EventManager");
            IL_InitAdapter("UIManager");

            // IL热更加载UI
            appdomain.Invoke("HotFix.Main", "Init", gameObject, null); //static方法
        }
        void IL_InitAdapter(string adapterName)
        {
            GameObject obj = new GameObject($"IL_{adapterName}");
            obj.transform.SetParent(this.transform);
            var script = obj.AddComponent<ILMonoBehaviour>();
            script.className = adapterName;
            Debug.Log($"{adapterName}.Run");
            script.Run();
        }
    }
}