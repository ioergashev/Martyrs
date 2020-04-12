using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class OpenDetailsOnSearchListItemClickSystem : MonoBehaviour
    {
        private ManagerWindows managerWindows;
        private SearchSettingsRuntime searchSettingsRuntime;
        private DetailsSettingsRuntime detailsSettingsRuntime;

        private void Awake()
        {
            managerWindows = FindObjectOfType<ManagerWindows>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
            detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
        }

        private void Start()
        {
            searchSettingsRuntime.OnSearchListUpdated.AddListener(SearchListUpdatedAction);
        }

        private void SearchListUpdatedAction()
        {
            // Подписаться на нажатие элементов списка
            foreach(var item in searchSettingsRuntime.PersonList)
            {
                item.View.OpenDetailsBtn.onClick.AddListener(() => ItemButtonClickAction(item));
            }
        }

        private void ItemButtonClickAction(PersonListItem item)
        {
            // Передать данные о записи
            detailsSettingsRuntime.Content = item.Content;

            managerWindows.OpenWindow(Windows.Details);
        }
    }
}
