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
        public static int ItemsPerPage 
        {
            get => instance.itemsPerPage; 
        }

        private int lastItemIndex = 0;
        public static int LastItemIndex
        { 
            get => instance.lastItemIndex; 
            set => instance.lastItemIndex = value;
        }

        private int itemsCount = 0;
        public static int ItemsCount
        {
            get => instance.itemsCount;
            set => instance.itemsCount = value;
        }

        public static int PagesCount 
        {
            get
            {
                int value = 0;

                if (ItemsCount >= 1 && ItemsPerPage > 0)
                {
                    value = (ItemsCount / ItemsPerPage) + 1;
                }

                return value;
            }
        }

        public static int CurrentPageIndex
        {
            get
            {
                int value = -1;

                if (LastItemIndex >= 0 && ItemsPerPage > 0)
                {
                    value = LastItemIndex / ItemsPerPage;
                }

                return value;
            }
        }

        public static bool IsLastPage
        {
            get
            {
                bool value = false;

                if(PagesCount >= 1)
                {
                    value = CurrentPageIndex == PagesCount - 1;
                }

                return value;
            }
        }

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