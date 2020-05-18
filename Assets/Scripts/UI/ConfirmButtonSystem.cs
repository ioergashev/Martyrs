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
        private WelcomeScreen welcomeScreen;
        private SearchSettingsRuntime searchSettingsRuntime;
        private WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            searchWindow = FindObjectOfType<SearchWindow>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
            welcomeScreen = FindObjectOfType<WelcomeScreen>();
        }

        private void Update()
        {
            var touchKeyboard = searchWindow.View.SearchInput.touchScreenKeyboard;

            if (!Input.GetKeyUp(KeyCode.Return) 
                && !(touchKeyboard != null && touchKeyboard.status == TouchScreenKeyboard.Status.Done))
            {
                return;
            }

            // Если не выполняется поиск,
            // открыто окно поиска, 
            // нет открытых панелей
            bool searchInSearchWindow =  
                windowsSettingsRuntime.CurrentWindow == Windows.Search
                && searchSettingsRuntime.SearchCoroutine == null
                && windowsSettingsRuntime.OpenedScreens.Count == 0;

            // Если не выполняется поиск,
            // открыто только окно приветствия, 
            // анимация закончилась
            bool searchInWelcomeScreen =
                searchSettingsRuntime.SearchCoroutine == null
                && windowsSettingsRuntime.OpenedScreens.Count == 1
                && windowsSettingsRuntime.OpenedScreens.Contains(Screens.Welcome)
                && welcomeScreen.View.SearchInput.interactable;


            if (searchInSearchWindow)
            {
                // Настроить поиск
                searchSettingsRuntime.SkipItemsCount = 0;
                searchSettingsRuntime.SearchQuery = searchWindow.View.SearchInput.text;

                // Выполнить поиск
                searchSettingsRuntime.SearchRequest?.Invoke();
            }
            else if (searchInWelcomeScreen)
            {
                // Настроить поиск
                searchSettingsRuntime.SkipItemsCount = 0;
                searchSettingsRuntime.SearchQuery = welcomeScreen.View.SearchInput.text;

                // Выполнить поиск
                searchSettingsRuntime.SearchRequest?.Invoke();
            }
        }
    }
}
