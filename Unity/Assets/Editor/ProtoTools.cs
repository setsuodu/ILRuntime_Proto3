using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public partial class BundleTools : Editor
{
    protected static void ExecuteCommand(string command)
    {
        /* cmd /c dir 是执行完dir命令后关闭命令窗口。
         * cmd /k dir 是执行完dir命令后不关闭命令窗口。
         * cmd /c start dir 会打开一个新窗口后执行dir指令，原窗口会关闭。
         * cmd /k start dir 会打开一个新窗口后执行dir指令，原窗口不会关闭。*/

        Process p = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "cmd.exe";
        startInfo.WorkingDirectory = @"C:\";
        //startInfo.Arguments = @"/c " + command; // cmd.exe spesific implementation
        startInfo.Arguments = @"/k " + command;
        p.StartInfo = startInfo;
        p.Start();
        //p.WaitForExit(); //等待一定时间（ms）退出
    }
    //批处理文件统一放在同一个目录里管理，执行也统一在这里调用
    protected static void RunBatch(string batFileName)
    {
        DirectoryInfo unityFolder = new DirectoryInfo("Assets");
        string batPath = $@"{unityFolder.Parent.Parent}\{batFileName}";
        Debug.Log(batPath);
        Process proc = new Process();
        proc.StartInfo.FileName = batPath; //初始化可执行文件名
        proc.Start();
    }

    #region 测试

    [MenuItem("Tools/Protoc/生成Proto")]
    private static void ConvertProto()
    {
        RunBatch("convert_proto.bat");
    }

    [MenuItem("Tools/取消读条")]
    static void CancelableProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }
    [MenuItem("Tools/测试/CMD")]
    private static void TestCMD()
    {
        ExecuteCommand(@"ipconfig /flushdns");
        //ExecuteCommand(@"ping www.baidu.com");
    }
    [MenuItem("Tools/测试/打包目标平台")]
    private static void GetCurrentTarget()
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
        // 两个需要清理的目录
        // ./Assets/StreamingAssets/AssetBundle
        // ./StandaloneWindows64
        string[] pathArray = new string[]
        {
            Path.Combine(Application.streamingAssetsPath, "AssetBundle"),
            Path.Combine(GetUnityDir(), "StandaloneWindows64"),
        };
        for (int i = 0; i < pathArray.Length; i++)
        {
            var path = pathArray[i];
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
        Debug.Log("清理完成");
    }

    [MenuItem("Tools/测试/打开服务器AB资源存放目录")]
    private static void OpenBundleServer()
    {
        Process.Start("explorer.exe", GetServerDir());
    }
    [MenuItem("Tools/测试/打开运行时AB资源下载目录")]
    private static void OpenBundleLocal()
    {
        //string path = Path.Combine(ConstValue.DataPath, "").Replace("/", @"\");
        //System.Diagnostics.Process.Start("explorer.exe", path);
        string path = Path.Combine(Application.persistentDataPath, "").Replace("/", @"\");
        Process.Start("explorer.exe", path);
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

    #region 热更新

    [MenuItem("Tools/热更新/编译热更工程", false, 21)]
    private static void CompileHotFix()
    {
        RunBatch("compile_hotfix.bat");
    }
    [MenuItem("Tools/热更新/MoveDLL", false, 22)]
    private static void MoveDLL()
    {
        string dllPath = Path.Combine(Application.streamingAssetsPath, "HotFix.dll");
        if (!File.Exists(dllPath))
        {
            Debug.LogError($"文件不存在：{dllPath}");
            return;
        }

        string bytesPath = Path.Combine(Application.dataPath, "AssetBundle/Config/HotFix.dll.bytes");
        if (File.Exists(bytesPath))
        {
            File.Delete(bytesPath);
        }
        File.Move(dllPath, bytesPath);

        Directory.Delete(Application.streamingAssetsPath, true);

        AssetDatabase.Refresh();
        Debug.Log("移动完成");
    }

    [MenuItem("Assets/Open Hotfix Project", false, 1000)]
    private static void OpenHotfixProject()
    {
        DirectoryInfo unityFolder = new DirectoryInfo("Assets");
        string batPath = $@"{unityFolder.Parent.Parent}\HotFix\HotFix_Project.sln";
        //Debug.Log(batPath);
        Process proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = batPath,
                CreateNoWindow = true,
            },
        };
        proc.Start();
    }

    #endregion
}
#endif