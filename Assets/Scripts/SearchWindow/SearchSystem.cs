using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Управляет поиском </summary>
    public class SearchSystem : MonoBehaviour
    {
        private SearchSettingsRuntime searchSettingsRuntime;
        private DataRuntime dataRuntime;
        private ManagerServer managerServer;

        private void Awake()
        {
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
            dataRuntime = FindObjectOfType<DataRuntime>();
            managerServer = FindObjectOfType<ManagerServer>();
        }

        private void Start()
        {
            searchSettingsRuntime.SearchRequest.AddListener(SearchRequestAction);
        }
        
        private void SearchRequestAction()
        {
            SearchAction(searchSettingsRuntime.SearchQuery, searchSettingsRuntime.SkipItemsCount);
        }

        private void SearchAction(string query, int skip = 0)
        {
            // Если поисковой запрос уже выполняется
            if (searchSettingsRuntime.SearchCoroutine != null)
            {
                return;
            }

            // Сбросить данные по предыдущему поиску
            ResetSearchSettings();

            // Начать поиск
            searchSettingsRuntime.SearchCoroutine = StartCoroutine(SearchCoroutine(query, skip));

            // Сообщить о начале поиска
            searchSettingsRuntime.OnStartSearch?.Invoke();
        }

        private void ResetSearchSettings()
        {
            // Начать отчет с начала
            searchSettingsRuntime.LastItemIndex = -1;
            searchSettingsRuntime.ItemsCount = 0;
        }

        private IEnumerator SearchCoroutine(string query, int skip)
        {
            // Сформировать данные для запроса
            var itemsPerPage = searchSettingsRuntime.ItemsPerPage;

            // Сформировать запрос
            var requestOper = managerServer.Search(query, skip, itemsPerPage);

            // Выполнить запрос
            yield return requestOper;

            // Получить ответ
            var response = requestOper.Current as SearchResponse;

            // Создать переменную для хранения обратного вызова
            UnityEvent callback;

            if (response == null || response.IsError)
            {
                // Сообщить об ошибке
                callback = searchSettingsRuntime.OnSearchError;

                // Сбросить данные поиска
                ResetSearchSettings();
            }
            else
            {
                // Сообщить об успешном завершении поиска
                callback = searchSettingsRuntime.OnSearchComplite;

                // Сохранить полученные данные
                dataRuntime.SearchResponse = response.data.ToList();
                searchSettingsRuntime.ItemsCount = response.RecordsFoundCount;
                searchSettingsRuntime.LastItemIndex = Mathf.Min(skip + searchSettingsRuntime.ItemsPerPage, searchSettingsRuntime.ItemsCount) - 1;
            }

            // Завершить поиск
            searchSettingsRuntime.SearchCoroutine = null;

            // Сообщить о завершении поиска
            callback?.Invoke();
        }
    }
}
