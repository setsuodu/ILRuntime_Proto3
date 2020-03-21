using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static T EnterUI<T>() where T : UIWidget
    {
        var uiRoot = GameObject.Find("Canvas").transform;
        string fileName = string.Format("prefabs/ui/{0}.unity3d", typeof(T));
        GameObject obj = AssetBundleManager.LoadGameObject(fileName, uiRoot);
        //GameObject obj = Instantiate(prefab, uiRoot);
        obj.AddComponent<T>();
        return obj.GetComponent<T>();
    }
}
