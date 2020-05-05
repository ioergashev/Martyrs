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
    }
}
