using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 资源管理工具
/// </summary>
public class AssetBundleManager : MonoBehaviour
{
    // 存放ab包的根目录
    private static ABInfo[] ABInfos;
    private static string[] permanentConfig = new string[]
    {
        "fonts/db.unity3d",
    };
    private static bool Initialized;

    // 每次检查更新过程Load一遍
    public static void Init()
    {
        if (!Initialized)
        {
            Initialized = true;

            string jsonStr = File.ReadAllText(Path.Combine(ConstValue.DataPath, "assets.bytes"));
            ABInfos = JsonMapper.ToObject<ABInfo[]>(jsonStr);

            LoadPermanent();
        }
    }

    // 常驻资源
    private static void LoadPermanent()
    {
        for (int i = 0; i < permanentConfig.Length; i++)
        {
            ABInfo abInfo = GetABInfo(permanentConfig[i]);
            string filePath = Path.Combine(ConstValue.DataPath, string.Format("{0}.unity3d", abInfo.md5));
            AssetBundle.LoadFromFile(filePath);
        }
    }

    // 输入加载用的名称，获得md5命名的ab文件名称
    private static ABInfo GetABInfo(string _fileName) 
    {
        string fileName = _fileName.ToLower();
        return ABInfos.Where(x => x.filePath == fileName).ToList()[0];
    }
    private static ABInfo GetABInfoByMD5(string _md5)
    {
        return ABInfos.Where(x => x.md5 == _md5).ToList()[0];
    }

    private static AssetBundle LoadAsset(string _fileName)
    {
        ABInfo abInfo = GetABInfo(_fileName);
        string filePath = Path.Combine(ConstValue.DataPath, string.Format("{0}.unity3d", abInfo.md5));
        AssetBundle asset = AssetBundle.LoadFromFile(filePath);
        //Debug.LogFormat("<color=green>{0}</color>", _fileName);
        return asset;
    }

    // 常驻资源不再加载
    public static List<AssetBundle> LoadDepends(ABInfo abInfo)
    {
        List<AssetBundle> dependList = new List<AssetBundle>();

        //取差集，排除常驻资源
        string[] exp = abInfo.depend.Except(permanentConfig).ToArray();
        //Debug.LogFormat("依赖数：{0}", exp.Length);
        for (int i = 0; i < exp.Length; i++)
        {
            string dpName = exp[i];
            ABInfo dpInfo = GetABInfo(dpName);
            string dpPath = Path.Combine(ConstValue.DataPath, string.Format("{0}.unity3d", dpInfo.md5));
            AssetBundle asset = AssetBundle.LoadFromFile(dpPath);
            dependList.Add(asset);
            //Debug.LogFormat("<color=green>{0}</color>", exp[i]);
        }
        return dependList;
    }

    public static GameObject LoadPrefab(string _fileName)
    {
        ABInfo abInfo = GetABInfo(_fileName);
        string filePath = Path.Combine(ConstValue.DataPath, string.Format("{0}.unity3d", abInfo.md5));
        AssetBundle asset = AssetBundle.LoadFromFile(filePath);
        GameObject prefab = asset.LoadAllAssets()[0] as GameObject;
        asset.Unload(false);
        return prefab;
    }

    public static GameObject LoadGameObject(string _fileName, Transform parent = null)
    {
        //先载依赖
        ABInfo abInfo = AssetBundleManager.GetABInfo(_fileName);
        List<AssetBundle> depends = AssetBundleManager.LoadDepends(abInfo);
        //再载主体
        AssetBundle asset = AssetBundleManager.LoadAsset(_fileName);
        GameObject prefab = asset.LoadAllAssets()[0] as GameObject;
        GameObject obj = Instantiate(prefab, parent);
        //卸载ab
        //Debug.Log("依赖数：" + depends.Count);
        for (int i = depends.Count - 1; i >= 0; i--)
        {
            //Debug.LogFormat("<color=red>{0}</color>", depends[i]);
            depends[i].Unload(false);
        }
        //Debug.LogFormat("<color=red>{0}</color>", _fileName);
        asset.Unload(false);
        return obj;
    }

    public static AudioClip LoadAudioClip(string _fileName)
    {
        AssetBundle asset = AssetBundleManager.LoadAsset(_fileName);
        AudioClip clip = asset.LoadAllAssets()[0] as AudioClip;
        asset.Unload(false);
        //Debug.LogFormat("<color=red>{0}</color>", _fileName);
        return clip;
    }

    // ILR代码
    public static byte[] LoadDLL()
    {
        AssetBundle asset = AssetBundleManager.LoadAsset("config/hotfix.unity3d");
        TextAsset textAsset = asset.LoadAllAssets()[0] as TextAsset;
        asset.Unload(false);
        return textAsset.bytes;
    }
}