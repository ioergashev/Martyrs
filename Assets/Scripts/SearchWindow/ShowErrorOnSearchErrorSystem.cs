using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Показывает окно ошибки в случае ошибки поиска </summary>
    public class ShowErrorOnSearchErrorSystem : MonoBehaviour
    {
        private ErrorScreen _errorScreen;
        private SearchSettingsRuntime _searchSettingsRuntime;

        private void Awake()
        {
            _errorScreen = FindObjectOfType<ErrorScreen>();
            _searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
        }

        private void Start()
        {
            _searchSettingsRuntime.OnSearchError.AddListener(SearchErrorAction);
        }

        private void SearchErrorAction()
        {
            _errorScreen.EnableComponent.ShowRequest?.Invoke();

            StartCoroutine(DelayedShutdown(ErrorScreenSettings.Instance.ShutdownDelay));
        }

        private IEnumerator DelayedShutdown(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (_errorScreen.EnableComponent.Enable)
            {
                _errorScreen.EnableComponent.HideRequest?.Invoke();
            }
        }
    }
}
