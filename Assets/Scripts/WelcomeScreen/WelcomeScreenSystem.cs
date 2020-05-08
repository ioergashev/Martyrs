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
        private WelcomeSettingsRuntime welcomeSettingsRuntime;
        private ManagerWindows managerWindows;
        private WindowsSettingsRuntime windowsSettingsRuntime;
        private SearchSettingsRuntime searchSettingsRuntime;

        private void Awake()
        {
            welcomeScreen = FindObjectOfType<WelcomeScreen>();
            welcomeSettingsRuntime = FindObjectOfType<WelcomeSettingsRuntime>();
            managerWindows = FindObjectOfType<ManagerWindows>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
        }

        private void Start()
        {
            // Если нужно показать окно
            if (welcomeSettingsRuntime.NeedShowWelcome)
            {
                // Показать окно
                managerWindows.SetScreenActive(Screens.Welcome, true);
            }

            // Подписаться на кнопку открытия сайта
            welcomeScreen.View.OpenUrlBtn.onClick.AddListener(OpenUrlClickAction);

            // Подписаться на начало поиска
            searchSettingsRuntime.OnStartSearch.AddListener(StartSearchAction);

            // Подписаться на закрытие окна
            windowsSettingsRuntime.OnStartCloseWelcome.AddListener(StartCloseWelcomeAction);
        }

        private void OpenUrlClickAction()
        {
            Application.OpenURL(welcomeSettingsRuntime.SiteUrl);
        }

        private void StartCloseWelcomeAction()
        {
            // Проерить галочку
            welcomeSettingsRuntime.NeedShowWelcome = !welcomeScreen.View.DontShowToggle.isOn;
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
