using System;
using UnityEngine;

[Obsolete("无用的类，仅供参考", false)]
public class UIManager : MonoBehaviour
{
    public static T EnterUI<T>() where T : UIWidget
    {
        var uiRoot = GameObject.Find("Canvas").transform;
        string fileName = string.Format("prefabs/ui/{0}.unity3d", typeof(T));
        GameObject obj = AssetBundleManager.LoadGameObject(fileName, uiRoot);
        obj.AddComponent<T>();
        return obj.GetComponent<T>();
    }
}