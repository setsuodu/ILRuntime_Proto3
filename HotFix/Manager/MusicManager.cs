using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotFix
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;

        void Awake()
        {
            Instance = this;
            //Debug.Log("[Hotfix] MusicManager.Awake");
        }
    }
}
