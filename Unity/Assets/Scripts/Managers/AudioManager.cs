using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 全局配置。不放场景中，通过脚本创建。
public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;
    public static AudioManager Get()
    {
        if (_instance == null)
            _instance = FindObjectOfType<AudioManager>();
        if (_instance == null)
        {
            var obj = new GameObject("AudioManager");
            _instance = obj.AddComponent<AudioManager>();
        }
        return _instance;
    }
    static bool created = false;

    public Dictionary<string, Sound> dic_sounds = new Dictionary<string, Sound>();

    public const string Round_1 = "round1";
    public const string GetHeart = "GetHeart";
    public const string Kill = "Kill";
    public const string MediumItem = "MediumItem";
    public const string Paradise = "dolls in pseudo paradise";
    public const string FallGround = "fall_ground";
    public const string FistHit = "FistHit";
    public const string HeavyHit = "HeavyHit";

    public static float musicVolume = 1;
    public static float soundVolume = 1;
    // 应用到当播放中的音乐
    public void ApplyToCurrent()
    {
        Debug.Log($"当前有{dic_sounds.Count}个音乐");
        foreach (var item in dic_sounds)
        {
            var sound = item.Value;
            Debug.Log($"音乐：{sound.audioName}，{sound.isMusic}，音量：{sound.source.volume}");
            sound.source.volume = sound.isMusic ? musicVolume : soundVolume;
        }
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            DestroyImmediate(gameObject, true); //多了一个
            Debug.LogError($"多了一个{this.GetType()}");
            return;
        }
    }

    // 播放BGM
    public void PlayMusic(string soundName, bool loop = false)
    {
        Sound sound = null;
        if (dic_sounds.TryGetValue(soundName, out sound) == false)
        {
            sound = new Sound(soundName);
            dic_sounds.Add(soundName, sound);
        }
        if (sound.source == null)
        {
            // 没有source组件，创建
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.volume = musicVolume;
            sound.source = source;
        }
        else
        {
            // 复用原来的source组件
        }
        sound.loop = loop;
        sound.isMusic = true;
        sound.TweenPlay();
    }
    // 播放音效
    public void PlaySound(string soundName, bool loop = false)
    {
        Sound sound = new Sound(soundName);
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.volume = soundVolume;
        sound.source = source;
        sound.loop = loop;
        sound.isMusic = false;
        sound.TweenPlay();
    }
    // 停止所有声音
    public void StopAll()
    {
        var array = gameObject.GetComponents<AudioSource>();
        for (int i = 0; i < array.Length; i++)
        {
            var source = array[i];
            DestroyImmediate(source);
        }
    }
}
public class Sound
{
    public Sound(string newName)
    {
        audioName = newName;
        clip = ResManager.LoadAudioClip($"audios/{audioName}");
        if (clip == null)
            throw new System.Exception("Couldn't find AudioClip with name '" + audioName + "'. Are you sure the file is in a folder named 'Resources'?");
    }

    public string audioName;
    public AudioClip clip;
    public AudioSource source;
    public bool loop;
    public bool isMusic;

    public void TweenPlay()
    {
        source.playOnAwake = false;
        source.clip = clip;
        source.loop = loop;
        source.Play();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            if (!loop)
            {
                //Debug.Log($"<color=red>播放完毕，删除：{audioName}</color>");
                MonoBehaviour.DestroyImmediate(source);
                source = null;
            }
            else
            {
                Debug.Log($"<color=green>播放完毕，保留：{audioName}</color>");
            }
        })
        .SetDelay(clip.length);
    }
}