using UnityEngine;

namespace PSTGU
{
    /// <summary> Управляет навигацией по страницам поиска </summary>
    public class SearchPagesNavigationSystem : MonoBehaviour
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
            SetButtomUIActive(false);

            searchSettingsRuntime.OnSearchComplite.AddListener(SearchCompliteAction);
            searchSettingsRuntime.OnSearchError.AddListener(SearchErrorAction);
            searchWindow.View.NextPageBtn.onClick.AddListener(NextPageBtnClickAction);
            searchWindow.View.PrevPageBtn.onClick.AddListener(PrevPageBtnClickAction);
        }

        private void SetButtomUIActive(bool value)
        {
            searchWindow.View.PagesNavigationContainer.SetActive(value);
            searchWindow.View.RecordsFoundCountTxt.gameObject.SetActive(value);
        }

        private void SearchCompliteAction()
        {
            SetButtomUIActive(true);

            searchWindow.View.RecordsFoundCountTxt.text = "Найдено записей: " + searchSettingsRuntime.ItemsCount;
            searchWindow.View.CurrentPageTxt.text = (searchSettingsRuntime.CurrentPageIndex + 1).ToString();
            searchWindow.View.PagesCountTxt.text = searchSettingsRuntime.PagesCount.ToString();
        }

        private void SearchErrorAction()
        {
            SetButtomUIActive(false);
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

            // Вычислить, сколько записей нужно пропустить
            int skip = searchSettingsRuntime.LastItemIndex + 1;

            searchSettingsRuntime.SkipItemsCount = skip;

            searchSettingsRuntime.SearchRequest?.Invoke();
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
            int skip = (searchSettingsRuntime.CurrentPageIndex - 1) * searchSettingsRuntime.ItemsPerPage;

            searchSettingsRuntime.SkipItemsCount = skip;

            searchSettingsRuntime.SearchRequest?.Invoke();
        }
    }
}
