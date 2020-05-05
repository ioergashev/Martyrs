using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Показывает окно загрузкки во время поиска </summary>
    public class ShowLoadingOnSearchSystem : MonoBehaviour
    {
        private LoadingScreen loadingScreen;
        private SearchSettingsRuntime searchSettingsRuntime;

        private void Awake()
        {
            loadingScreen = FindObjectOfType<LoadingScreen>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
        }

        private void Start()
        {
            searchSettingsRuntime.OnStartSearch.AddListener(StartSearchAction);
            searchSettingsRuntime.OnSearchComplite.AddListener(SearchCompliteAction);
            searchSettingsRuntime.OnSearchCancel.AddListener(SearchCancelAction);
            searchSettingsRuntime.OnSearchError.AddListener(SearchErrorAction);
        }

        private void StartSearchAction()
        {
            loadingScreen.EnableComponent.ShowRequest?.Invoke();
        }

        private void SearchCompliteAction()
        {
            if (loadingScreen.EnableComponent.Enable)
            {
                loadingScreen.EnableComponent.HideRequest?.Invoke();
            }
        }

        private void SearchCancelAction()
        {
            if (loadingScreen.EnableComponent.Enable)
            {
                loadingScreen.EnableComponent.HideRequest?.Invoke();
            }
        }

        private void SearchErrorAction()
        {
            if (loadingScreen.EnableComponent.Enable)
            {
                loadingScreen.EnableComponent.HideRequest?.Invoke();
            }
        }
    }
}
