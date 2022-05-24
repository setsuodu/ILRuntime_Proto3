using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private static bool Initialized = false;
    public static GameConfig gameConfig;

    void Awake()
    {
        if (!Initialized)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 系统设置
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.002f;
            Screen.fullScreen = false;
            //Screen.SetResolution(540, 960);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 30;
            QualitySettings.vSyncCount = 0;

            // 绑定组件
            transform.Find("ILGlobal").gameObject.AddComponent<Client.ILGlobal>();

#if UNITY_EDITOR && !USE_ASSETBUNDLE
            // 不检查更新
            OnInited();
#else
            // 加载配置（需要启动资源服务器）
            StartCoroutine(GetConfig());
#endif
        }
        else
        {
            OnInited();
        }
    }

    // 读取游戏配置（ab包下载地址，游戏版本号，公告等）
    IEnumerator GetConfig()
    {
        UnityWebRequest request = new UnityWebRequest
        {
            url = ConstValue.CONFIG_URL,
            method = "GET",
        };
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.responseCode != 200)
        {
            Debug.LogError($"error code = {request.responseCode}");
            yield break;
        }
        string text = request.downloadHandler.text;
        Debug.Log($"success: {text}");
        gameConfig = JsonMapper.ToObject<GameConfig>(text);
        request.Dispose();

        yield return CheckUpdateAsync(OnInited);
    }

    IEnumerator CheckUpdateAsync(System.Action action)
    {
        if (!Directory.Exists(ConstValue.DataPath))
            Directory.CreateDirectory(ConstValue.DataPath);

        Transform root = GameObject.Find("Canvas").transform;
        var request = Resources.LoadAsync<GameObject>("UI_CheckUpdate");
        yield return request;

        var asset = request.asset as GameObject;
        GameObject prefab = Instantiate(asset, root);
        var script = prefab.AddComponent<UI_CheckUpdate>();

        yield return script.StartCheck(action);
    }

    // 初始化完成，控制权移交ILR
    void OnInited()
    {
        Initialized = true;

        Client.ILGlobal.Instance.GlobalInit(); //加载dll
    }
}