using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "DetailsSettings")]
    public class DetailsSettings : ScriptableObject
    {
        private static DetailsSettings _instance;

        private static DetailsSettings instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<DetailsSettings>("PSTGU/DetailsSettings");
                }

                return _instance;
            }
        }

        public static PersonContent Content;

        public static Coroutine LoadPhotoCoroutine;
    }
}