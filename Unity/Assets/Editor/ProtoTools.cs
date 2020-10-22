using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;

#if UNITY_EDITOR
public partial class BundleTools : Editor
{
    #region 测试

    [MenuItem("Tools/Protoc/运行批处理")]
    private static void RunBatch()
    {
        DirectoryInfo unityFolder = new DirectoryInfo("Assets");
        //string batPath = $"{unityFolder.Parent.Parent}/Protoc/helloworld.bat";
        string batPath = $"{unityFolder.Parent.Parent}/Protoc/run.bat";
        Debug.Log(batPath);
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = batPath; // 初始化可执行文件名
        proc.Start();
    }

    [MenuItem("Tools/测试/Print")]
    private static void Print()
    {
        Debug.Log(BuildTarget.StandaloneWindows64);
    }

    private static void Clean_Cookies()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("清理完成");
    }

    [MenuItem("Tools/测试/清理临时文件夹")]
    private static void ClearTmpFolders()
    {
        string[] pathArray = new string[]
        {
            Path.Combine(Application.streamingAssetsPath, "AssetBundle"),
            Path.Combine(GetUnityDir(), "StandaloneWindows64")
        };
        for (int i = 0; i < pathArray.Length; i++)
        {
            if (Directory.Exists(pathArray[i]))
                Directory.Delete(pathArray[i], true);
        }
        Debug.Log("清理完成");
    }

    [MenuItem("Tools/测试/打开资源服务器")]
    private static void OpenBundleServer()
    {
        System.Diagnostics.Process.Start("explorer.exe", GetServerDir());
    }

    [MenuItem("Tools/测试/打开资源文件夹")]
    private static void OpenBundleLocal()
    {
        //string path = Path.Combine(ConstValue.DataPath, "").Replace("/", @"\");
        //System.Diagnostics.Process.Start("explorer.exe", path);
        string path = Path.Combine(Application.persistentDataPath, "").Replace("/", @"\");
        System.Diagnostics.Process.Start("explorer.exe", path);
    }

    [MenuItem("Tools/Shader/重置IncludedShaders")]
    private static void ResetIncludedShaders()
    {

    }

    [MenuItem("Tools/Shader/设置IncludedShaders")]
    private static void SetIncludedShaders()
    {

    }

    #endregion
}
#endif