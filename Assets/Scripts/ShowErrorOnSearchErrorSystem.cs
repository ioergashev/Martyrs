using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class ShowErrorOnSearchErrorSystem : MonoBehaviour
    {
        private ErrorScreen errorScreen;
        private SearchSettingsRuntime searchSettingsRuntime;

        private void Awake()
        {
            errorScreen = FindObjectOfType<ErrorScreen>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
        }

        private void Start()
        {
            searchSettingsRuntime.OnSearchError.AddListener(SearchErrorAction);
        }

        private void SearchErrorAction()
        {
            if (!errorScreen.EnableComponent.Enable)
            {
                errorScreen.EnableComponent.ShowRequest?.Invoke();
            }

            StartCoroutine(DelayedShutdown(ErrorScreenSettings.Instance.ShutdownDelay));
        }

        private IEnumerator DelayedShutdown(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (errorScreen.EnableComponent.Enable)
            {
                errorScreen.EnableComponent.HideRequest?.Invoke();
            }
        }
    }
}
