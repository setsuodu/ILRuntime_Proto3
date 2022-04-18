using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
public class CreateConfig : Editor
{
    static void CreateAsset<Type>() where Type : ScriptableObject
    {
        Type asset = ScriptableObject.CreateInstance<Type>();

        string root = Application.dataPath + "/Resources";
        if (!Directory.Exists(root))
        {
            Directory.CreateDirectory(root);
        }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/" + typeof(Type) + ".asset");

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
#endif