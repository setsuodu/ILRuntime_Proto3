using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstValue
{
    public const string CONFIG_URL = "http://localhost/download/present.json"; //游戏启动首先指向的配置

    /// <summary>
    /// ab包下载地址
    /// </summary>
    private static string dataUrl;
    public static string DataUrl 
    {
        get
        {
            if (string.IsNullOrEmpty(dataUrl))
            {
#if UNITY_EDITOR
                dataUrl = Path.Combine(GameManager.gameConfig.ab_url, "StandaloneWindows64");
#elif UNITY_ANDROID
                dataUrl = Path.Combine(GameManager.gameConfig.ab_url, "ANDROID");
#elif UNITY_IOS
                dataUrl = Path.Combine(GameManager.gameConfig.ab_url, "IOS");
#endif
            }
            return dataUrl;
        }
    }

    /// <summary>
    /// ab包本地保存位置
    /// </summary>
    private static string dataPath;
    public static string DataPath 
    {
        get 
        {
            if (string.IsNullOrEmpty(dataPath)) 
            {
                dataPath = Path.Combine(Application.persistentDataPath, Application.platform.ToString());
            }
            return dataPath;
        }
    }
}
