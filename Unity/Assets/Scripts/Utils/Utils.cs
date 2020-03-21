using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Utils
{
    public static string GetFileMD5(string filepath)
    {
        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        int bufferSize = 1048576;
        byte[] buff = new byte[bufferSize];
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        md5.Initialize();
        long offset = 0;
        while (offset < fs.Length)
        {
            long readSize = bufferSize;
            if (offset + readSize > fs.Length)
                readSize = fs.Length - offset;
            fs.Read(buff, 0, Convert.ToInt32(readSize));
            if (offset + readSize < fs.Length)
                md5.TransformBlock(buff, 0, Convert.ToInt32(readSize), buff, 0);
            else
                md5.TransformFinalBlock(buff, 0, Convert.ToInt32(readSize));
            offset += bufferSize;
        }
        if (offset >= fs.Length)
        {
            fs.Close();
            byte[] result = md5.Hash;
            md5.Clear();
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
                sb.Append(result[i].ToString("X2"));
            return sb.ToString().ToLower();
        }
        else
        {
            fs.Close();
            return null;
        }
    }
}
