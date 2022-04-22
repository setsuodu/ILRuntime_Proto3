using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

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
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
            //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
            appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif

            // 这里做一些ILRuntime的注册
            appdomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter()); //注册跨域继承
            appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter()); //注册System.IDisposable, IEnumerator
            appdomain.RegisterCrossBindingAdaptor(new ProtobufAdapter());

            // 注册"空参空返回"型的委托
            appdomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>((act) => { return new UnityAction(() => { ((System.Action)act)(); }); });
            //appdomain.DelegateManager.RegisterFunctionDelegate<UIBase, System.Boolean>();
            //appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<UIBase>>((act) =>
            //{
            //    return new System.Predicate<UIBase>((obj) =>
            //    {
            //        return ((System.Func<UIBase, System.Boolean>)act)(obj);
            //    });
            //});
            appdomain.DelegateManager.RegisterMethodDelegate<System.Object, System.Net.Sockets.SocketAsyncEventArgs>();
            appdomain.DelegateManager.RegisterDelegateConvertor<System.EventHandler<System.Net.Sockets.SocketAsyncEventArgs>>((act) =>
            {
                return new System.EventHandler<System.Net.Sockets.SocketAsyncEventArgs>((sender, e) =>
                {
                    ((System.Action<System.Object, System.Net.Sockets.SocketAsyncEventArgs>)act)(sender, e);
                });
            });

            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
            ET.ILHelper.InitILRuntime(appdomain); // 好像没啥用

            // HelloWorld，第一次方法调用
            //appdomain.Invoke("HotFix.Main", "Proto", gameObject, null); //实例方法
            //appdomain.Invoke("HotFix.Main", "OnProto", null, null); //静态方法
        }

        // 加载完成，调用ILR代码
        void OnHotFixLoaded()
        {
            /* 测试调用
            appdomain.Invoke("HotFix.UIManager", "Test1", null, null);
            appdomain.Invoke("HotFix.UIManager", "Test2", gameObject, null);
            appdomain.Invoke("HotFix.UIManager", "Test3", gameObject, "123");
            */

            IL_InitAdapter("UIManager");
            IL_InitAdapter("EventManager");

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