using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class ShowErrorOnSearchErrorSystem : MonoBehaviour
    {
        private void Start()
        {
            SearchSettings.OnSearchError.AddListener(SearchErrorAction);
        }

        private void SearchErrorAction()
        {
            if (!ErrorScreen.EnableComponent.Enable)
            {
                ErrorScreen.EnableComponent.ShowRequest?.Invoke();
            }

            StartCoroutine(DelayedShutdown(ErrorScreenSettings.ShutdownDelay));
        }

        private IEnumerator DelayedShutdown(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (ErrorScreen.EnableComponent.Enable)
            {
                ErrorScreen.EnableComponent.HideRequest?.Invoke();
            }
        }
    }
}
