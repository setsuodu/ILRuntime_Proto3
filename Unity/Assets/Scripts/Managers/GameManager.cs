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
        Init();
    }

    void Init()
    {
        if (!Initialized)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 系统设置
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.002f;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 30;
            QualitySettings.vSyncCount = 0;

            // 绑定组件
            transform.Find("ILGlobal").gameObject.AddComponent<Client.ILGlobal>();

            // 加载配置
            StartCoroutine(LoadConfig());
        }
        else
        {
            OnInited();
        }
    }

    // 读取游戏配置（ab包下载地址，游戏版本号，公告等）
    IEnumerator LoadConfig()
    {
        UnityWebRequest request = new UnityWebRequest
        {
            url = ConstValue.CONFIG_URL,
            method = "GET",
        };
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        Debug.Log($"Status code: {request.responseCode}");
        if (request.responseCode != 200)
        {
            Debug.LogError($"error code = {request.responseCode}");
            yield break;
        }
        else
        {
            string text = request.downloadHandler.text;
            Debug.Log(text);
            gameConfig = JsonMapper.ToObject<GameConfig>(text);
            request.Dispose();
        }

        /*
        WWW www = new WWW(ConstValue.CONFIG_URL);
        while (!www.isDone) yield return null;
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
            yield break;
        }
        Debug.Log(www.text);
        gameConfig = JsonMapper.ToObject<GameConfig>(www.text);
        www.Dispose();
        */

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