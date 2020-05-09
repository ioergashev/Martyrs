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

            // Если открыто окно поиска, 
            // не выполняется поиск
            // нет открытых панелей за исключением окна приветствия,
            bool allowSearch =  
                windowsSettingsRuntime.CurrentWindow == Windows.Search
                && searchSettingsRuntime.SearchCoroutine == null
                && (windowsSettingsRuntime.OpenedScreens.Count == 0
                    || (windowsSettingsRuntime.OpenedScreens.Count == 1
                        && windowsSettingsRuntime.OpenedScreens.Contains(Screens.Welcome)));

            if (allowSearch)
            {
                // Выполнить поиск
                searchSettingsRuntime.SkipItemsCount = 0;

                searchSettingsRuntime.SearchRequest?.Invoke();
            }
        }
    }
}
