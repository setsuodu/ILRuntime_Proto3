using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Security;
using System.ComponentModel;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class ThreadDownload : MonoBehaviour
{
    public static ThreadDownload instance;

    public List<string> urlList;
    public List<string> pathList;

    void Awake()
    {
        instance = this;
    }


    public class MyThread
    {
        public string _url;
        public string _filePath;
        public float _progress { get; private set; } //下载进度
        public bool _isDone { get; private set; } //是否下载完成
        public Action<object, DownloadProgressChangedEventArgs> _onProgressChanged;
        public Action<object, AsyncCompletedEventArgs> _onFileComplete;

        public MyThread(string url, string filePath, Action<object, DownloadProgressChangedEventArgs> progress, Action<object, AsyncCompletedEventArgs> complete)
        {
            _url = url;
            _filePath = filePath;
            _onProgressChanged = progress;
            _onFileComplete = complete;
        }

        public void DownLoadImage()
        {
            // 按 UrlList 的顺序
            if (!File.Exists(_filePath))
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(_url, _filePath);
            }

            FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate);
            byte[] bytes = new byte[(int)fs.Length];
            int read = fs.Read(bytes, 0, bytes.Length);
            fs.Dispose();
            fs.Close();

            Debug.Log("流写入本地:" + bytes.Length);
        }

        // 流计算MD5值
        private static string GetMD5Hash(byte[] bytedata)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(bytedata);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5Hash() fail,error:" + ex.Message);
            }
        }
    }
}
