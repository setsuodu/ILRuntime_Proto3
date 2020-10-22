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
    #region 路径

    private static string patchName = "AssetBundle";

    private static string _srcPath;
    private static string srcPath
    {
        get
        {
            if (string.IsNullOrEmpty(_srcPath))
            {
                _srcPath = Path.Combine(Application.dataPath, patchName);
            }
            return _srcPath;
        }
    }

    private static string _outputPath;
    private static string outputPath
    {
        get
        {
            if (string.IsNullOrEmpty(_outputPath))
            {
                _outputPath = Path.Combine(Application.streamingAssetsPath, patchName);
            }
            return _outputPath;
        }
    }

    private static string GetUnityDir()
    {
        DirectoryInfo direction = new DirectoryInfo("Assets");
        return direction.Parent.ToString();
    }
    private static string GetServerDir()
    {
        string path = @"D:\wamp\www\download";
        return path;
    }

    #endregion

    #region 标记

    [MenuItem("Tools/打包AB/Set Labels", false, 11)]
    private static void SetAssetBundleLabels()
    {
        // 移除所有没有使用的标记
        AssetDatabase.RemoveUnusedAssetBundleNames();

        // 1. 找到资源所在的文件夹
        DirectoryInfo directoryInfo = new DirectoryInfo(srcPath);
        DirectoryInfo[] typeDirectories = directoryInfo.GetDirectories(); //子文件夹

        // 2. 遍历里面每个子文件夹
        foreach (DirectoryInfo childDirectory in typeDirectories)
        {
            string typeDirectory = srcPath + "/" + childDirectory.Name;
            DirectoryInfo sceneDirectoryInfo = new DirectoryInfo(typeDirectory); //一级目录
            //Debug.Log("<color=red>" + typeDirectory + "</color>");

            // 错误检测
            if (sceneDirectoryInfo == null)
            {
                Debug.LogError(typeDirectory + "不存在");
                return;
            }
            else
            {
                Dictionary<string, string> namePathDict = new Dictionary<string, string>();

                // 3. 遍历子文件夹里的所有文件系统
                string typeName = Path.GetFileName(typeDirectory);
                //Debug.Log(typeName);

                onSceneFileSystemInfo(sceneDirectoryInfo, typeName, namePathDict);

                //onWriteConfig(typeName, namePathDict);
            }
        }
        AssetDatabase.Refresh();
        Debug.LogWarning("设置成功");
    }

    /// <summary>
    /// 清除所有的AssetBundleName，由于打包方法会将所有设置过AssetBundleName的资源打包，所以自动打包前需要清理
    /// </summary>
    [MenuItem("Tools/打包AB/Clean Labels", false, 11)]
    private static void ClearAssetBundlesName()
    {
        // 获取所有的AssetBundle名称
        string[] abNames = AssetDatabase.GetAllAssetBundleNames();

        // 强制删除所有AssetBundle名称
        for (int i = 0; i < abNames.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(abNames[i], true);
        }
    }

    private static void onSceneFileSystemInfo(FileSystemInfo fileSystemInfo, string typeName, Dictionary<string, string> namePathDict)
    {
        if (!fileSystemInfo.Exists)
        {
            Debug.LogError(fileSystemInfo.FullName + ":不存在");
            return;
        }
        DirectoryInfo directoryInfo = fileSystemInfo as DirectoryInfo;
        FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
        foreach (var tempfileInfo in fileSystemInfos)
        {
            FileInfo fileInfo = tempfileInfo as FileInfo;
            if (fileInfo == null)
            {
                // 4. 如果找到的是文件夹, 递归直到没有文件夹
                DirectoryInfo dirInfo = tempfileInfo as DirectoryInfo; //二级目录
                //Debug.Log("强转失败，是文件夹:" + dirInfo);
                onSceneFileSystemInfo(tempfileInfo, typeName, namePathDict);
            }
            else
            {
                // 5. 找到文件, 修改他的 AssetLabels
                //Debug.Log("是文件");
                setLables(fileInfo, typeName, namePathDict);
            }
        }
    }

    /// <summary>
    /// 修改资源文件的 assetbundle labels
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <param name="typeName"></param>
    private static void setLables(FileInfo fileInfo, string typeName, Dictionary<string, string> namePathDict)
    {
        // 忽视unity自身生成的meta文件
        if (fileInfo.Extension == ".meta") return;
        //Debug.Log(fileInfo); // ..\v2\Lightmap\home\Lightmap-0_comp_light.exr  => Lightmap\Lightmap-0_comp_light.exr

        string bundleName = getBundleName(fileInfo, typeName); //sofa_1.mat
        //Debug.Log(bundleName); // 最终结果

        int index = fileInfo.FullName.IndexOf("Assets");
        string assetPath = fileInfo.FullName.Substring(index); // Assets/Sources/Materials/sofa_1.mat
        //Debug.Log(assetPath);

        // 6. 修改名称和后缀
        AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
        assetImporter.assetBundleName = bundleName;
        if (fileInfo.Extension == ".unity")
        {
            assetImporter.assetBundleVariant = "u3d"; //场景文件
        }
        else
        {
            assetImporter.assetBundleVariant = "unity3d"; //资源文件
        }

        // 添加到字典
        string folderName = "";
        if (bundleName.Contains("/"))
        {
            folderName = bundleName.Split('/')[1];
        }
        else
        {
            folderName = bundleName;
        }

        string bundlePath = assetImporter.assetBundleName + "." + assetImporter.assetBundleVariant;
        if (!namePathDict.ContainsKey(folderName))
            namePathDict.Add(folderName, bundlePath);
    }

    /// <summary>
    /// 获取包名
    /// </summary>
    /// <param name="fileInfo">文件信息</param>
    /// <param name="typeName">资源类型</param>
    /// <returns></returns>
    private static string getBundleName(FileInfo fileInfo, string typeName)
    {
        string windowPath = fileInfo.FullName;
        string unityPath = windowPath.Replace(@"\", "/"); //转斜杠 C:/Users/Administrator/Documents/GitHub/AssetBundleExample/Assets/Sources/Textures/trash_2.jpg

        int Index = unityPath.IndexOf(typeName) + typeName.Length;
        string bundlePath = unityPath.Substring(Index + 1);
        //string bundlePath = Path.GetFileNameWithoutExtension(unityPath);
        //Debug.Log(fileInfo + " + " + typeName + " = " + bundlePath); //sofa_3.mat
        //string result = Path.Combine(typeName, bundlePath);

        var array = bundlePath.Split('.');
        string bundlePathWithoutExt = array[0];
        string result = Path.Combine(typeName, bundlePathWithoutExt);
        //Debug.Log(result);
        return result;
    }

    #endregion

    #region 打包

    private static void BuildAssetBundles()
    {
        SetAssetBundleLabels();

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(outputPath, 0, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();

        //打包完成后

        //清理AssetLabel
        ClearAssetBundlesName();

        //生成配置文件
        MakeVersion();

        //转移到根目录
        ExportBundles();
    }

    private static void MakeVersion()
    {
        string assetBundlePath = Path.Combine(Application.streamingAssetsPath, "AssetBundle/AssetBundle");
        if (!File.Exists(assetBundlePath))
        {
            Debug.LogError("assetBundle不存在");
            return;
        }
        string manifestPath = Path.Combine(Application.streamingAssetsPath, "AssetBundle/AssetBundle.manifest");
        if (!File.Exists(manifestPath))
        {
            Debug.LogError("manifest不存在");
            return;
        }

        //解析*.manifest
        var bundle = AssetBundle.LoadFromFile(assetBundlePath);
        AssetBundleManifest manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] bundles = manifest.GetAllAssetBundles();

        List<ABInfo> ABInfoList = new List<ABInfo>();
        for (int i = 0; i < bundles.Length; i++) 
        {
            //计算文件md5，写入json
            string filePath = Path.Combine(Application.streamingAssetsPath, "AssetBundle/" + bundles[i]);
            string md5 = GetMD5HashFromFile(filePath);
            string[] depends = manifest.GetAllDependencies(bundles[i]);
            ABInfo fs = new ABInfo(bundles[i], md5, depends);
            ABInfoList.Add(fs);
        }
        string jsonStr = JsonMapper.ToJson(ABInfoList);
        Debug.Log(jsonStr);

        // 压缩包释放掉
        bundle.Unload(false);
        bundle = null;

        string assetsPath = Path.Combine(Application.streamingAssetsPath, "AssetBundle/assets.bytes");
        File.WriteAllText(assetsPath, jsonStr);
    }

    //移动到根目录，删除*.manifest
    //[MenuItem("Tools/ExportBundles")]
    private static void ExportBundles()
    {
        string fullPath = "Assets/StreamingAssets";
        DirectoryInfo direction = new DirectoryInfo(fullPath);
        if (!Directory.Exists(fullPath))
        {
            Debug.LogError("路径不存在");
            return;
        }
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        //Debug.Log(files.Length);

        //string outputPath = Path.Combine(Application.streamingAssetsPath, EditorUserBuildSettings.activeBuildTarget.ToString());
        string outputPath = Path.Combine(GetUnityDir(), EditorUserBuildSettings.activeBuildTarget.ToString());
        if (Directory.Exists(outputPath))
            Directory.Delete(outputPath, true);
        Directory.CreateDirectory(outputPath);

        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".meta") || files[i].Name.EndsWith(".manifest")) continue;
            //Debug.Log(i + "---" + files[i].FullName + "\n" + GetMD5HashFromFile(files[i].FullName));

            string srcFilePath = files[i].FullName;
            string dstFilePath = Path.Combine(outputPath, files[i].Name);
            if (files[i].Name.EndsWith(".unity3d"))
            {
                dstFilePath = Path.Combine(outputPath, GetMD5HashFromFile(files[i].FullName) + ".unity3d");
            }
            File.Copy(srcFilePath, dstFilePath);
        }
    }

    private static void ClearAssetBundle()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "AssetBundle");
        Directory.Delete(filePath, true);
    }

    /// <summary>
    /// 获取md5
    /// </summary>
    /// <param name="filePath">文件地址</param>
    /// <returns></returns>
    private static string GetMD5HashFromFile(string filePath)
    {
        try
        {
            FileStream file = new FileStream(filePath, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);   //计算指定Stream 对象的哈希值  
            file.Close();

            StringBuilder Ac = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                Ac.Append(retVal[i].ToString("x2"));
            }
            return Ac.ToString();
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }

    #endregion

    #region 目标平台

    private static void Build_Target(BuildTarget target)
    {
        if (!EditorUserBuildSettings.activeBuildTarget.Equals(target))
        {
            Debug.LogError("请先切换平台");
            EditorUtility.DisplayDialog("目标平台与当前平台不一致，请先进行平台转换", "当前平台：" + EditorUserBuildSettings.activeBuildTarget + $"\n目标平台：{target}", "OK");
            return;
        }

        // 清空streamingAssets
        if (Directory.Exists(Application.streamingAssetsPath))
            Directory.Delete(Application.streamingAssetsPath, true);

        BuildAssetBundles();

        ClearAssetBundle();

        Deploy(target);

        Debug.Log("打包完成");
    }

    [MenuItem("Tools/打包AB/StandaloneWindows64", false, 0)]
    private static void AB_StandaloneWindows64()
    {
        Build_Target(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Tools/打包AB/Android", false, 0)]
    private static void AB_Android()
    {
        Build_Target(BuildTarget.Android);
    }

    [MenuItem("Tools/打包AB/iOS", false, 0)]
    private static void AB_iOS()
    {
        if (!EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.iOS))
        {
            Debug.LogError("请先切换平台");
            EditorUtility.DisplayDialog("目标平台与当前平台不一致，请先进行平台转换", "当前平台：" + EditorUserBuildSettings.activeBuildTarget + "\n目标平台：iOS", "OK");
            return;
        }
        BuildAssetBundles();
        Debug.Log("打包完成");
    }

    [MenuItem("Tools/打包AB/部署")]
    private static void Deploy(BuildTarget target)
    {
        string srcPath = Path.Combine(GetUnityDir(), target.ToString());
        //Debug.Log(srcPath);
        if (!Directory.Exists(srcPath))
        {
            Debug.LogError($"src不存在：{srcPath}");
            return;
        }

        string dstPath = $@"{GetServerDir()}\{target}";
        //string dstPath = $@"{GetServerDir()}";
        //Debug.Log(dstPath);
        if (Directory.Exists(dstPath))
        {
            Debug.Log($"dst存在：{dstPath}，先删除");
            Directory.Delete(dstPath, true);
        }

        //c#不支持跨硬盘移动文件夹
        //Directory.Move(srcPath, dstPath);
        //DirectoryInfo dirInfo = new DirectoryInfo(srcPath);
        //dirInfo.MoveTo(dstPath);
        CopyFolder(srcPath, dstPath);
        Debug.Log("部署完成");
    }
    private static void CopyFolder(string strFromPath, string strToPath)
    {
        //如果源文件夹不存在，则创建
        if (!Directory.Exists(strFromPath))
        {
            Directory.CreateDirectory(strFromPath);
        }
        //取得要拷贝的文件夹名
        //string strFolderName = strFromPath.Substring(strFromPath.LastIndexOf("\\") + 1, strFromPath.Length - strFromPath.LastIndexOf("\\") - 1);
        //如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
        if (!Directory.Exists(strToPath))
        {
            Directory.CreateDirectory(strToPath);
        }
        //创建数组保存源文件夹下的文件名
        string[] strFiles = Directory.GetFiles(strFromPath);
        //循环拷贝文件
        for (int i = 0; i < strFiles.Length; i++)
        {
            //取得拷贝的文件名，只取文件名，地址截掉。
            string strFileName = strFiles[i].Substring(strFiles[i].LastIndexOf("\\") + 1, strFiles[i].Length - strFiles[i].LastIndexOf("\\") - 1);
            //开始拷贝文件,true表示覆盖同名文件
            File.Copy(strFiles[i], strToPath + "\\" + strFileName, true);
        }
        //创建DirectoryInfo实例
        DirectoryInfo dirInfo = new DirectoryInfo(strFromPath);
        //取得源文件夹下的所有子文件夹名称
        DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
        for (int j = 0; j < ZiPath.Length; j++)
        {
            //获取所有子文件夹名
            string strZiPath = strFromPath + "\\" + ZiPath[j].ToString();
            //把得到的子文件夹当成新的源文件夹，从头开始新一轮的拷贝
            CopyFolder(strZiPath, strToPath);
        }
    }


    [MenuItem("Tools/打包AB/编译热更工程", false, 21)]
    private static void CompileHotFix()
    {
        RunBatch("compile_hotfix.bat");
    }

    [MenuItem("Tools/打包AB/MoveDLL", false, 22)]
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

    #endregion
}
#endif