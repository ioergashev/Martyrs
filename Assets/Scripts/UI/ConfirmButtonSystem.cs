using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class ConfirmButtonSystem : MonoBehaviour
    {
        private SearchWindow searchWindow;
        private SearchSettingsRuntime searchSettingsRuntime;
        private WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            searchWindow = FindObjectOfType<SearchWindow>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
        }

        private void Update()
        {
            if (!Input.GetKeyUp(KeyCode.Return))
            {
                return;
            }

            // Если открыто окно поиска, нет открытых панелей и не выполняется поиск
            if (windowsSettingsRuntime.CurrentWindow == Windows.Search
                && windowsSettingsRuntime.OpenedScreens.Count == 0
                && searchSettingsRuntime.SearchCoroutine == null)
            {
                // Выполнить поиск
                searchSettingsRuntime.SearchRequest?.Invoke();
            }
        }
    }
}
