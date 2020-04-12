using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class SearchSystem : MonoBehaviour
    {
        private SearchWindow searchWindow;
        private SearchSettingsRuntime searchSettingsRuntime;
        private DataRuntime dataRuntime;
        private ManagerServer managerServer;

        private void Awake()
        {
            searchWindow = FindObjectOfType<SearchWindow>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
            dataRuntime = FindObjectOfType<DataRuntime>();
            managerServer = FindObjectOfType<ManagerServer>();
        }

        private void Start()
        {
            searchWindow.View.SearchBtn.onClick.AddListener(SearchBtnClickAction);
            searchWindow.View.NextPageBtn.onClick.AddListener(NextPageBtnClickAction);
            searchWindow.View.PrevPageBtn.onClick.AddListener(PrevPageBtnClickAction);
        }
        
        private void SearchBtnClickAction()
        {
            // Если поисковой запрос уже выполняется
            if (searchSettingsRuntime.SearchCoroutine != null)
            {
                return;
            }

            // Сбросить данные по предыдущему поиску
            ResetSearchSettings();

            // Начать поиск
            searchSettingsRuntime.SearchCoroutine = StartCoroutine(SearchCoroutine(0));

            // Сообщить о начале поиска
            searchSettingsRuntime.OnStartSearch?.Invoke();
        }

        private void NextPageBtnClickAction()
        {
            // Если поисковой запрос уже выполняется
            if (searchSettingsRuntime.SearchCoroutine != null)
            {
                return;
            }

            // Если не более одной страницы или последняя страница
            if (searchSettingsRuntime.PagesCount <= 1 || searchSettingsRuntime.IsLastPage)
            {
                return;
            }

            // Начать поиск для следующей страницы
            searchSettingsRuntime.SearchCoroutine = StartCoroutine(SearchCoroutine(searchSettingsRuntime.LastItemIndex + 1));

            // Сообщить о начале поиска
            searchSettingsRuntime.OnStartSearch?.Invoke();
        }

        private void PrevPageBtnClickAction()
        {
            // Если поисковой запрос уже выполняется
            if (searchSettingsRuntime.SearchCoroutine != null)
            {
                return;
            }

            // Если не более одной страницы или первая страница
            if (searchSettingsRuntime.PagesCount <= 1 || searchSettingsRuntime.CurrentPageIndex <= 0)
            {
                return;
            }

            // Вычислить, сколько записей нужно пропустить
            var skip = (searchSettingsRuntime.CurrentPageIndex - 1) * searchSettingsRuntime.ItemsPerPage;

            // Начать поиск для предыдущей страницы
            searchSettingsRuntime.SearchCoroutine = StartCoroutine(SearchCoroutine(skip));

            // Сообщить о начале поиска
            searchSettingsRuntime.OnStartSearch?.Invoke();
        }

        private void ResetSearchSettings()
        {
            // Начать отчет с начала
            searchSettingsRuntime.LastItemIndex = -1;
            searchSettingsRuntime.ItemsCount = 0;
        }

        private IEnumerator SearchCoroutine(int skip)
        {
            // Сформировать данные для запроса
            var query = searchWindow.View.SearchInput.text;
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
                Debug.LogError(response == null);

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
