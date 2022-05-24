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
    // 批处理文件统一放在同一个目录里管理，执行也统一在这里调用
    protected static void RunBatch(string batFileName)
    {
        //DirectoryInfo unityFolder = new DirectoryInfo("Assets");
        //string projRoot2 = unityFolder.Parent.Parent.ToString();
        //Debug.Log(projRoot2);
        string currDir = Directory.GetCurrentDirectory();
        DirectoryInfo currDirInfo = new DirectoryInfo(currDir);
        string projRoot = currDirInfo.Parent.ToString();
        Debug.Log(projRoot);

        string filePath = $@"{projRoot}\{batFileName}";
        Debug.Log(filePath);
        Process proc = new Process();
        proc.StartInfo.WorkingDirectory = projRoot; //在文件所在位置执行
        proc.StartInfo.FileName = filePath; //初始化可执行文件名
        proc.Start();
    }

    #region 热更新

    [MenuItem("Tools/热更新/生成Proto", false, 21)]
    private static void ConvertProto()
    {
        InnerProto2CS.Proto2CS();
    }
    [MenuItem("Tools/热更新/编译热更工程", false, 22)]
    private static void CompileHotFix()
    {
        RunBatch("compile_hotfix.bat");
    }
    [MenuItem("Tools/热更新/MoveDLL", false, 23)]
    private static void MoveDLL()
    {
        string dllPath = Path.Combine(Application.streamingAssetsPath, "HotFix.dll");
        if (!File.Exists(dllPath))
        {
            Debug.LogError($"文件不存在：{dllPath}");
            return;
        }

        string bytesPath = Path.Combine(Application.dataPath, "Bundles/Configs/HotFix.dll.bytes");
        if (File.Exists(bytesPath))
        {
            File.Delete(bytesPath);
        }
        File.Move(dllPath, bytesPath);

        Directory.Delete(Application.streamingAssetsPath, true);

        AssetDatabase.Refresh();
        Debug.Log("移动完成");
    }

    [MenuItem("Tools/Shader/重置IncludedShaders", true)]
    private static void ResetIncludedShaders() { }
    [MenuItem("Tools/Shader/设置IncludedShaders", true)]
    private static void SetIncludedShaders() { }

    [MenuItem("Assets/Open Server Project", false)]
    private static void OpenServerProject()
    {
        string currDir = Directory.GetCurrentDirectory();
        DirectoryInfo currDirInfo = new DirectoryInfo(currDir);
        string projPath = $@"{currDirInfo.Parent}\NetCoreServer\NetCoreApp.sln";
        //Debug.Log(projPath);
        Process proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = projPath,
                CreateNoWindow = true,
            },
        };
        proc.Start();
    }
    [MenuItem("Tools/服务器/打开服务器AB资源存放目录", true)]
    private static void OpenBundleServer()
    {
        Process.Start("explorer.exe", GetServerDir());
    }
    [MenuItem("Tools/服务器/打开运行时AB资源下载目录", false)]
    private static void OpenBundleLocal()
    {
        //string path = Path.Combine(ConstValue.DataPath, string.Empty).Replace("/", @"\");
        string path = Path.Combine(Application.persistentDataPath, string.Empty).Replace("/", @"\");
        Process.Start("explorer.exe", path);
    }
    [MenuItem("Tools/服务器/启动服务器", false)]
    private static void StartServer()
    {
        string currDir = Directory.GetCurrentDirectory();
        DirectoryInfo currDirInfo = new DirectoryInfo(currDir);
        string exePath = $@"{currDirInfo.Parent}\NetCoreServer\NetCoreApp\bin\Debug\netcoreapp3.1\NetCoreServer.exe";
        Debug.Log(exePath);
        ExecuteCommand(exePath);
    }

    #endregion
}
#endif