using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotFix
{
    class SomeMonoBehaviour : MonoBehaviour
    {
        float time;
        void Awake()
        {
            Debug.Log("!! SomeMonoBehaviour.Awake");
        }

        void Start()
        {
            Debug.Log("!! SomeMonoBehaviour.Start");
        }

        void Update()
        {
            //if(Time.time - time > 1)
            //{
            //    Debug.Log("!! SomeMonoBehaviour.Update, t=" + Time.time);
            //    time = Time.time;
            //}
        }

        public void Test()
        {
            Debug.Log("SomeMonoBehaviour");
        }
    }

    class SomeMonoBehaviour2 : MonoBehaviour
    {
        public Transform parent;
        public List<GameObject> list = new List<GameObject>();

        void Awake()
        {
            parent = GameObject.Find("Canvas").transform;

            Debug.Log("!!! SomeMonoBehaviour2.Awake");
            list = new List<GameObject>();
            //Debug.Log(list.Count);
        }

        //public GameObject TargetGO;
        //public Texture2D Texture;
        public void Test2()
        {
            Debug.Log("!!! SomeMonoBehaviour2.Test2");

            var obj = Client.ResManager.LoadPrefab("UI/UI_Main");
            var go = Instantiate(obj, parent);
            go.name = "UI_Main";
            list.Add(go);
            var ui = go.AddComponent<UI_Main>();
            Debug.Log($"<color=red>count={list.Count}</color>");
            Debug.Log($"<color=red>parent={parent != null}</color>");
        }

        public void Test3()
        {
            Debug.Log("!!! SomeMonoBehaviour2.Test3");

            Debug.Log($"<color=green>count={list.Count}</color>");
            Debug.Log($"<color=green>{list[0].name}</color>");
            var obj = list.Find(x => x.name == "UI_Main");
            Debug.Log($"<color=green>obj={obj.name}</color>");
        }
    }

    public class TestMonoBehaviour
    {
        public static void RunTest(GameObject go)
        {
            go.AddComponent<SomeMonoBehaviour>();
        }

        public static void RunTest2(GameObject go)
        {
            go.AddComponent<SomeMonoBehaviour2>();
            var mb = go.GetComponent<SomeMonoBehaviour2>();
            Debug.Log("!!!TestMonoBehaviour.RunTest2 mb= " + mb);
            //mb.Test2();
        }
    }
}
