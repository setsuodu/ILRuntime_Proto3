using UnityEditor;

public class ToolsEditor
{
    [MenuItem("Tools/Proto2CS")]
    public static void Proto2CS()
    {
        /* 使用编译好的 Tools.exe
#if UNITY_EDITOR_OSX
            const string tools = "./Tools";
#else
        const string tools = ".\\Tools.exe";
#endif
        ShellHelper.Run($"{tools} --AppType=Proto2CS --Console=1", "../Bin/");
        */

        InnerProto2CS.Proto2CS();
    }
}