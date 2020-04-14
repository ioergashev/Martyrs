using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "PhotosScreenSettings")]
    public class PhotosScreenSettings : ScriptableObject
    {
        private static PhotosScreenSettings _instance;

        public static PhotosScreenSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<PhotosScreenSettings>("PSTGU/PhotosScreenSettings");
                }

                return _instance;
            }
        }

        public GameObject PhotoPrefab;
    }
}