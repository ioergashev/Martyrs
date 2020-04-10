using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class ShowLoadingOnSearchSystem : MonoBehaviour
    {
        private void Start()
        {
            SearchSettings.OnStartSearch.AddListener(StartSearchAction);
            SearchSettings.OnSearchComplite.AddListener(SearchCompliteAction);
            SearchSettings.OnSearchCancel.AddListener(SearchCancelAction);
            SearchSettings.OnSearchError.AddListener(SearchErrorAction);
        }

        private void StartSearchAction()
        {
            if (!LoadingScreen.EnableComponent.Enable)
            {
                LoadingScreen.EnableComponent.ShowRequest?.Invoke();
            }
        }

        private void SearchCompliteAction()
        {
            if (LoadingScreen.EnableComponent.Enable)
            {
                LoadingScreen.EnableComponent.HideRequest?.Invoke();
            }
        }

        private void SearchCancelAction()
        {
            if (LoadingScreen.EnableComponent.Enable)
            {
                LoadingScreen.EnableComponent.HideRequest?.Invoke();
            }
        }

        private void SearchErrorAction()
        {
            if (LoadingScreen.EnableComponent.Enable)
            {
                LoadingScreen.EnableComponent.HideRequest?.Invoke();
            }
        }
    }
}
