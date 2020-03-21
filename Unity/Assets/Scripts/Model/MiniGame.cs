using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : ScriptableObject
{
    public string title = "MiniGame";
    public List<Content> list;

    void OnEnable()
    {
        if (list == null)
            list = new List<Content>();
    }

    public void Add(Content content)
    {
        list.Add(content);
    }

    public void Remove(Content content)
    {
        list.Remove(content);
    }

    public void RemoveAt(int index)
    {
        list.RemoveAt(index);
    }
}

[System.Serializable]
public class Content
{
    public string Name;           //改成Enum
    public string Description;    //描述
    public GameObject MainPrefab; //场景启动预制体
    public Sprite MainSprite;
}
