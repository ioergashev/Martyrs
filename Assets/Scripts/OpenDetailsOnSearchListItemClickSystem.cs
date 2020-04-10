﻿using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class OpenDetailsOnSearchListItemClickSystem : MonoBehaviour
    {
        private void Start()
        {
            SearchSettings.OnSearchListUpdated.AddListener(SearchListUpdatedAction);
        }

        private void SearchListUpdatedAction()
        {
            // Подписаться на нажатие элементов списка
            foreach(var item in SearchSettings.PersonList)
            {
                item.View.OpenDetailsBtn.onClick.AddListener(() => ItemButtonClickAction(item));
            }
        }

        private void ItemButtonClickAction(PersonListItem item)
        {
            // Передать данные о записи
            DetailsSettings.Content = item.Content;

            ManagerWindows.OpenWindow(Windows.Details);
        }
    }
}
