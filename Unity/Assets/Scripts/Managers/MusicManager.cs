using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;

        void Awake()
        {
            Instance = this;
        }
    }
}
