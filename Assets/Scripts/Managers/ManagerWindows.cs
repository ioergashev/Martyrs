using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSTGU
{
    public class ManagerWindows : MonoBehaviour
    {
        private WindowsSettingsRuntime _windowsSettingsRuntime;

        private DetailsWindow _detailsWindow;
        private SearchWindow _searchWindow;

        private LoadingScreen _loadingScreen;
        private ErrorScreen _errorScreen;
        private PhotosScreen _photosScreen;

        private void Awake()
        {
            _windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();

            _detailsWindow = FindObjectOfType<DetailsWindow>();
            _searchWindow = FindObjectOfType<SearchWindow>();

            _loadingScreen = FindObjectOfType<LoadingScreen>();
            _errorScreen = FindObjectOfType<ErrorScreen>();
            _photosScreen = FindObjectOfType<PhotosScreen>();
        }

        public void CloseLastOpenedScreen()
        {
            // Если не открыто ни одно всплывающее окно
            if (_windowsSettingsRuntime.OpenedScreens.Count == 0)
            {
                return;
            }

            // Получть последнее открытое всплывающее окно
            var lastScreen = _windowsSettingsRuntime.OpenedScreens.Last();

            // Закрыть последнее открытое всплывающее окно
            SetScreenActive(lastScreen, false);
        }    

        /// <summary>См. <seealso cref="_windowsSettingsRuntime.OpenedScreens"/> - список открытых окон </summary>
        public void SetScreenActive(Screens screen, bool value)
        {
            EnableWindowComponent enableComponent = GetScreenEnableComponent(screen);

            if (enableComponent == null)
            {
                return;
            }

            if (value == true)
            {
                // Если окно не открыто
                if (!_windowsSettingsRuntime.OpenedScreens.Contains(screen))
                {
                    // Обновить список открытых окон
                    _windowsSettingsRuntime.OpenedScreens.Add(screen);

                    // Открыть окно
                    enableComponent.ShowRequest?.Invoke(); 
                }                
            }
            else
            {
                // Если окно открыто
                if (_windowsSettingsRuntime.OpenedScreens.Contains(screen))
                {
                    // Обновить список открытых окон
                    _windowsSettingsRuntime.OpenedScreens.Remove(screen);

                    // Закрыть окно
                    enableComponent.HideRequest?.Invoke();  
                }
                
            }
        }

        /// <summary> Получить компонент, отвечающий за открытие/закрытие окна </summary>
        private EnableWindowComponent GetScreenEnableComponent(Screens screen)
        {
            EnableWindowComponent enableComponent = null;

            // Получить компонент, отвечающий за открытие/закрытие окна
            switch (screen)
            {
                case Screens.Loading:
                    enableComponent = _loadingScreen.EnableComponent;
                    break;
                case Screens.Error:
                    enableComponent = _errorScreen.EnableComponent;
                    break;
                case Screens.Photos:
                    enableComponent = _photosScreen.EnableComponent;
                    break;

                default:
                    enableComponent = null;
                    break;
            }

            return enableComponent;
        }

        /// <summary>См. <seealso cref="_windowsSettingsRuntime.TransitionCoroutine"/> - процесс открытия окна </summary>
        public void OpenWindow(Windows window)
        {
            // Если уже выполняется переход между окнами
            if (_windowsSettingsRuntime.TransitionCoroutine != null)
            {
                return;
            }

            // Указать целевое окно
            _windowsSettingsRuntime.TargetWindow = window;

            // Начать открытие окна и сохранить операцию
            _windowsSettingsRuntime.TransitionCoroutine = StartCoroutine(OpenWindowCoroutine(window));

            // Сообщить о начале перехода
            _windowsSettingsRuntime.OnStartTransition?.Invoke();

            if (_windowsSettingsRuntime.TargetWindow == Windows.Details)
            {
                // Сообщить о начале открытия окна
                _windowsSettingsRuntime.OnStartOpenDetails?.Invoke();
            }
        }

        private IEnumerator OpenWindowCoroutine(Windows window)
        {
            // Закрыть текущее открытое окно
            yield return SetWindowActiveCroutine(_windowsSettingsRuntime.CurrentWindow, false);

            // Указать открытое окно
            _windowsSettingsRuntime.CurrentWindow = Windows.None;

            // Окрыть целевое окно
            yield return SetWindowActiveCroutine(window, true);

            // Указать открытое окно
            _windowsSettingsRuntime.CurrentWindow =  window;

            // Завершить операцию
            _windowsSettingsRuntime.TransitionCoroutine = null;

            // Сообщить о завершении перехода
            _windowsSettingsRuntime.OnEndTransition?.Invoke();
        }

        private IEnumerator SetWindowActiveCroutine(Windows window, bool value)
        {
            EnableWindowComponent enableComponent = GetWindowEnableComponent(window);

            if (enableComponent == null)
            {
                yield break;
            }

            if(value == true)
            {
                // Открыть окно
                enableComponent.ShowRequest?.Invoke();

                // Ждать октрытия окна
                yield return new WaitUntil(() => enableComponent.Enable);
            }
            else
            {
                // Закрыть окно
                enableComponent.HideRequest?.Invoke();

                // Ждать закрытия окна
                yield return new WaitWhile(() => enableComponent.Enable);
            }
        }

        /// <summary> Получить компонент, отвечающий за открытие/закрытие окна </summary>
        private EnableWindowComponent GetWindowEnableComponent(Windows window)
        {
            EnableWindowComponent enableComponent = null;

            // Получить компонент, отвечающий за открытие/закрытие окна
            switch (window)
            {
                case Windows.Details:
                    enableComponent = _detailsWindow.EnableComponent;
                    break;
                case Windows.Search:
                    enableComponent = _searchWindow.EnableComponent;
                    break;

                default:
                    enableComponent = null;
                    break;
            }

            return enableComponent;
        }
    }
}