using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Управляет основными механиками окна поиска </summary>
    public class SearchWindowSystem : MonoBehaviour
    {
        private SearchWindow searchWindow;
        private SearchSettingsRuntime searchSettingsRuntime;

        private void Awake()
        {
            searchWindow = FindObjectOfType<SearchWindow>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
        }

        private void Start()
        {
            searchWindow.View.SearchBtn.onClick.AddListener(SearchBtnClickAction);
            searchSettingsRuntime.OnSearchComplite.AddListener(SearchCompliteAction);
        }

        private void SearchBtnClickAction()
        {
            // Передать параметры поиска
            searchSettingsRuntime.SkipItemsCount = 0;
            searchSettingsRuntime.SearchQuery = searchWindow.View.SearchInput.text;

            // Выполнить поиск
            searchSettingsRuntime.SearchRequest?.Invoke();
        }

        private void SearchCompliteAction()
        {
            // Перейти на начало списка
            searchWindow.View.VertSearchScrollbar.value = 1;
        }
    }
}
