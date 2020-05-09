using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PSTGU
{
    public class SearchSettingsRuntime : MonoBehaviour
    {
        public int ItemsPerPage = 20;

        [HideInInspector]
        public int LastItemIndex = -1;

        [HideInInspector]
        public int SkipItemsCount = 0;

        [HideInInspector]
        public int ItemsCount = 0;

        public int PagesCount
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

        public int CurrentPageIndex
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

        public bool IsLastPage
        {
            get
            {
                bool value = false;

                if (PagesCount >= 1)
                {
                    value = CurrentPageIndex == PagesCount - 1;
                }

                return value;
            }
        }

        [HideInInspector]
        public UnityEvent SearchRequest = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnStartSearch = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnSearchComplite = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnSearchError = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnSearchCancel = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnSearchListUpdated = new UnityEvent();

        [HideInInspector]
        public Coroutine SearchCoroutine;

        [HideInInspector]
        public List<PersonListItem> PersonList = new List<PersonListItem>();
    }
}