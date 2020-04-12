using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "SearchSettings")]
    public class SearchSettings : ScriptableObject
    {
        private static SearchSettings instance;

        public static SearchSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<SearchSettings>("PSTGU/SearchSettings");
                }

                return instance;
            }
        }

        public GameObject SearchListItemPrefab;
    }
}