using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class FileHelper
{
    public static void WriteText()
    {
        string content = "123456";
        string fileName = "ABC";
        string root = Environment.CurrentDirectory;
        //string outPath = $"{root}/Assets/{fileName}.txt";
        string outPath = $"./Assets/{fileName}.txt";
        string srcPath = $"{root}/Assets/{fileName}.txt";

        /*
        // 创建
        string srcContent = string.Empty;
        bool srcExist = File.Exists(outPath);
        Debug.Log($"{outPath}, exist={srcExist}");
        if (srcExist == false)
        {
            // 没有文件，创建，写入。
        }
        else
        {
            // 有文件，先读取，新建一行写入。
            srcContent = File.ReadAllText(outPath);
            content = srcContent + "\n" + content;
        }
        byte[] data = Encoding.UTF8.GetBytes(content);
        File.WriteAllBytes(outPath, data); //直接覆盖了
        */

        /*
        // 读取
        var fileExist = File.Exists(srcPath);
        Debug.Log($"{srcPath}, exist={fileExist}");
        if (fileExist == false)
        {
            return;
        }
        FileInfo file = new FileInfo(srcPath);
        StreamReader sr = file.OpenText();
        string fileStr = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();
        Debug.Log($"读取：{fileStr}");
        */

        /*
        // 删除
        var exist = File.Exists(outPath);
        Debug.Log($"{outPath}, exist={exist}");
        if (exist)
        {
            File.Delete(outPath);
            Debug.Log($"exist={exist}");
        }
        */

        // 修改


        //AssetDatabase.Refresh();
    }

    public static void WriteBytes(byte[] data)
    {
        Debug.Log($"Write data length={data.Length}");

        string fileName = "byte[]对比_Client";
        string filePath = @$"C:\Users\Administrator\Desktop\ILRuntime_Proto3\{fileName}.txt";
        File.WriteAllBytes(filePath, data); //直接覆盖了
        Debug.Log($"WriteBytes to: {filePath}");
    }

    public static byte[] ReadBytes(string path)
    {
        var data = File.ReadAllBytes(path);
        return data;
    }
}