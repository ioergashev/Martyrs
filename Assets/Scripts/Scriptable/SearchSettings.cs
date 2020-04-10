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
        private static SearchSettings _instance;

        private static SearchSettings instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<SearchSettings>("PSTGU/SearchSettings");
                }

                return _instance;
            }
        }

        [SerializeField]
        private int itemsPerPage = 10;
        public static int ItemsPerPage { get => instance.itemsPerPage; }

        [SerializeField]
        private GameObject searchListItemPrefab;
        public static GameObject SearchListItemPrefab { get => instance.searchListItemPrefab; }

        public static UnityEvent OnStartSearch = new UnityEvent();
        public static UnityEvent OnSearchComplite = new UnityEvent();
        public static UnityEvent OnSearchError = new UnityEvent();
        public static UnityEvent OnSearchCancel = new UnityEvent();

        public static UnityEvent OnSearchListUpdated = new UnityEvent();

        public static Coroutine SearchCoroutine;

        public static List<PersonListItem> PersonList = new List<PersonListItem>();
    }
}