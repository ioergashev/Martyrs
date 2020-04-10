using UnityEngine;
using System;
using System.Collections.Generic;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "Data")]
    public class Data : ScriptableObject
    {
        private static Data _instance;

        private static Data instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Data>("PSTGU/Data");
                }

                return _instance;
            }
        }

        public static Dictionary<string, PersonContent> Persons = new Dictionary<string, PersonContent>();
        public static Dictionary<string, PhotoItem> Photos = new Dictionary<string, PhotoItem>();

        [Header("Server Communication")]

        public static List<PersonContent> SearchResponse = new List<PersonContent>();
    }
}