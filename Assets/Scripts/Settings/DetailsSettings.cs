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
        private static DetailsSettings instance;

        public static DetailsSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<DetailsSettings>("PSTGU/DetailsSettings");
                }

                return instance;
            }
        }

        public GameObject PhotoPrefab;
    }
}