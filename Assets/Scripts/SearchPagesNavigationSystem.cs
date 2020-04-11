using UnityEngine;

namespace PSTGU
{
    public class SearchPagesNavigationSystem : MonoBehaviour
    {
        private void Start()
        {
            SetButtomUIActive(false);

            SearchSettings.OnSearchComplite.AddListener(SearchCompliteAction);
            SearchSettings.OnSearchError.AddListener(SearchErrorAction);
        }

        private void SetButtomUIActive(bool value)
        {
            SearchWindow.View.PagesNavigationContainer.SetActive(value);
            SearchWindow.View.RecordsFoundCountTxt.gameObject.SetActive(value);
        }

        private void SearchCompliteAction()
        {
            SetButtomUIActive(true);

            SearchWindow.View.RecordsFoundCountTxt.text = "Найдено записей: " + SearchSettings.ItemsCount;
            SearchWindow.View.CurrentPageTxt.text = (SearchSettings.CurrentPageIndex + 1).ToString();
            SearchWindow.View.PagesCountTxt.text = SearchSettings.PagesCount.ToString();
        }

        private void SearchErrorAction()
        {
            SetButtomUIActive(false);
        }
    }
}
