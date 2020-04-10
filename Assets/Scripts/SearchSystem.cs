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
        }

        private void SearchBtnClickAction()
        {
            // Если поисковой запрос уже выполняется
            if (SearchSettings.SearchCoroutine != null)
            {
                return;
            }

            // Начать поиск
            SearchSettings.SearchCoroutine = StartCoroutine(SearchCoroutine());

            // Сообщить о начале поиска
            SearchSettings.OnStartSearch?.Invoke();
        }

        private IEnumerator SearchCoroutine()
        {
            // Сформировать данные для запроса
            string query = SearchWindow.View.SearchInput.text;
            int itemsPerPage = SearchSettings.ItemsPerPage;

            // Сформировать запрос
            var requestOper = ManagerServer.Search(query, 0, itemsPerPage);

            // Выполнить запрос
            yield return requestOper;

            // Получить ответ
            var response = requestOper.Current as SearchResponse;

            // Создать переменную для хранения обратного вызова
            UnityEvent callback;

            if (response == null || response.IsError)
            {
                // Сообщить об ошибке
                callback = SearchSettings.OnSearchError;
            }
            else
            {
                // Сообщить об успешном завершении поиска
                callback = SearchSettings.OnSearchComplite;

                // Сохранить полученные данные
                Data.SearchResponse = response.data.ToList();
            }

            // Завершить поиск
            SearchSettings.SearchCoroutine = null;

            // Сообщить о завершении поиска
            callback?.Invoke();
        }
    }
}
