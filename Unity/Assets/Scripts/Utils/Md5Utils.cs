using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Md5Utils
{
    // 计算文件MD5/32位小写
    public static string getFileHash(string filePath)
    {
        try
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            int length = (int)fs.Length;
            byte[] data = new byte[length];
            fs.Read(data, 0, length);
            fs.Close();

            //MD5 md5 = MD5.Create();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);

            string fileMD5 = "";
            for (int i = 0; i < result.Length; i++)
            {
                fileMD5 += result[i].ToString("x2"); //32位
            }
            //Debug.Log(fileMD5);
            return fileMD5;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }

    // 计算字符串MD5/32位小写
    public static string getStringHash()
    {
        return "";
    }
}
