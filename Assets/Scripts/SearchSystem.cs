using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class SearchSystem : MonoBehaviour
    {
        private void Start()
        {
            SearchWindow.View.SearchBtn.onClick.AddListener(SearchBtnClickAction);
            SearchWindow.View.NextPageBtn.onClick.AddListener(NextPageBtnClickAction);
            SearchWindow.View.PrevPageBtn.onClick.AddListener(PrevPageBtnClickAction);
        }
        
        private void SearchBtnClickAction()
        {
            // Если поисковой запрос уже выполняется
            if (SearchSettings.SearchCoroutine != null)
            {
                return;
            }

            // Сбросить данные по предыдущему поиску
            ResetSearchSettings();

            // Начать поиск
            SearchSettings.SearchCoroutine = StartCoroutine(SearchCoroutine(0));

            // Сообщить о начале поиска
            SearchSettings.OnStartSearch?.Invoke();
        }

        private void NextPageBtnClickAction()
        {
            // Если поисковой запрос уже выполняется
            if (SearchSettings.SearchCoroutine != null)
            {
                return;
            }

            // Если не более одной страницы или последняя страница
            if (SearchSettings.PagesCount <= 1 || SearchSettings.IsLastPage)
            {
                return;
            }

            // Начать поиск для следующей страницы
            SearchSettings.SearchCoroutine = StartCoroutine(SearchCoroutine(SearchSettings.LastItemIndex + 1));

            // Сообщить о начале поиска
            SearchSettings.OnStartSearch?.Invoke();
        }

        private void PrevPageBtnClickAction()
        {
            // Если поисковой запрос уже выполняется
            if (SearchSettings.SearchCoroutine != null)
            {
                return;
            }

            // Если не более одной страницы или первая страница
            if (SearchSettings.PagesCount <= 1 || SearchSettings.CurrentPageIndex <= 0)
            {
                return;
            }

            // Вычислить, сколько записей нужно пропустить
            var skip = (SearchSettings.CurrentPageIndex - 1) * SearchSettings.ItemsPerPage;

            // Начать поиск для предыдущей страницы
            SearchSettings.SearchCoroutine = StartCoroutine(SearchCoroutine(skip));

            // Сообщить о начале поиска
            SearchSettings.OnStartSearch?.Invoke();
        }

        private void ResetSearchSettings()
        {
            // Начать отчет с начала
            SearchSettings.LastItemIndex = -1;
            SearchSettings.ItemsCount = 0;
        }

        private IEnumerator SearchCoroutine(int skip)
        {
            // Сформировать данные для запроса
            var query = SearchWindow.View.SearchInput.text;
            var itemsPerPage = SearchSettings.ItemsPerPage;

            // Сформировать запрос
            var requestOper = ManagerServer.Search(query, skip, itemsPerPage);

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
                callback = SearchSettings.OnSearchError;
                // Сбросить данные поиска
                ResetSearchSettings();
            }
            else
            {
                // Сообщить об успешном завершении поиска
                callback = SearchSettings.OnSearchComplite;

                // Сохранить полученные данные
                Data.SearchResponse = response.data.ToList();
                SearchSettings.ItemsCount = response.RecordsFoundCount;
                SearchSettings.LastItemIndex = Mathf.Min(skip + SearchSettings.ItemsPerPage, SearchSettings.ItemsCount) - 1;
            }

            // Завершить поиск
            SearchSettings.SearchCoroutine = null;

            // Сообщить о завершении поиска
            callback?.Invoke();
        }
    }
}
