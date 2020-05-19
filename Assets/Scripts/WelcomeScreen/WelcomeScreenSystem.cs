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
        private SearchWindow searchWindow;

        private void Awake()
        {
            welcomeScreen = FindObjectOfType<WelcomeScreen>();
            managerWindows = FindObjectOfType<ManagerWindows>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
            searchWindow = FindObjectOfType<SearchWindow>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
        }

        private void Start()
        {
            if (!Application.isEditor || WelcomeSettings.Instance.ShowWelcomeInEditor)
            {
                // Показать окно
                managerWindows.SetScreenActive(Screens.Welcome, true);
            }

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
                // Передать ввод в окно поиска
                searchWindow.View.SearchInput.text = welcomeScreen.View.SearchInput.text;

                // Закрыть окно
                managerWindows.SetScreenActive(Screens.Welcome, false);
            }
        }
    }
}
