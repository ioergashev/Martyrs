using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Управляет основными механиками окна приветствия </summary>
    public class WelcomeScreenSystem : MonoBehaviour
    {
        private WelcomeScreen welcomeScreen;
        private ManagerWindows managerWindows;
        private WindowsSettingsRuntime windowsSettingsRuntime;
        private SearchSettingsRuntime searchSettingsRuntime;

        private void Awake()
        {
            welcomeScreen = FindObjectOfType<WelcomeScreen>();
            managerWindows = FindObjectOfType<ManagerWindows>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
        }

        private void Start()
        {
            // Показать окно
            managerWindows.SetScreenActive(Screens.Welcome, true);

            // Подписаться на кнопку поиска
            welcomeScreen.View.SearchBtn.onClick.AddListener(SearchBtnClickAction);

            // Подписаться на начало поиска
            searchSettingsRuntime.OnStartSearch.AddListener(StartSearchAction);
        }

        private void SearchBtnClickAction()
        {
            // Передать параметры поиска
            searchSettingsRuntime.SkipItemsCount = 0;
            searchSettingsRuntime.SearchQuery = welcomeScreen.View.SearchInput.text;

            // Выполнить поиск
            searchSettingsRuntime.SearchRequest?.Invoke();
        }

        private void StartSearchAction()
        {
            // Если окно открыто
            if(windowsSettingsRuntime.OpenedScreens.Contains(Screens.Welcome))
            {
                // Закрыть окно
                managerWindows.SetScreenActive(Screens.Welcome, false);
            }
        }
    }
}
