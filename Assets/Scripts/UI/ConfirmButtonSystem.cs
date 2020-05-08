using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

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
            var touchKeyboard = searchWindow.View.SearchInput.touchScreenKeyboard;

            if (!Input.GetKeyUp(KeyCode.Return) 
                && !(touchKeyboard != null && touchKeyboard.status == TouchScreenKeyboard.Status.Done))
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
