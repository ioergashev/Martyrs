using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    public class ManagerWindows : MonoBehaviour
    {
        private DetailsWindow detailsWindow;
        private WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            detailsWindow = FindObjectOfType<DetailsWindow>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
        }

        /// <summary>См. <seealso cref="windowsSettingsRuntime.TransitionCoroutine"/> - процесс открытия окна </summary>
        public void OpenWindow(Windows window)
        {
            // Если уже выполняется переход между окнами
            if (windowsSettingsRuntime.TransitionCoroutine != null)
            {
                return;
            }

            // Указать целевое окно
            windowsSettingsRuntime.TargetWindow = window;

            // Начать открытие окна и сохранить операцию
            windowsSettingsRuntime.TransitionCoroutine = StartCoroutine(OpenWindowCoroutine(window));

            // Сообщить о начале перехода
            windowsSettingsRuntime.OnStartTransition?.Invoke();
        }

        private IEnumerator OpenWindowCoroutine(Windows window)
        {
            // Закрыть текущее открытое окно
            yield return SetWindowActiveCroutine(windowsSettingsRuntime.CurrentWindow, false);

            // Указать открытое окно
            windowsSettingsRuntime.CurrentWindow = Windows.None;

            // Окрыть целевое окно
            yield return SetWindowActiveCroutine(window, true);

            // Указать открытое окно
            windowsSettingsRuntime.CurrentWindow = window;

            // Завершить операцию
            windowsSettingsRuntime.TransitionCoroutine = null;

            // Сообщить о завершении перехода
            windowsSettingsRuntime.OnEndTransition?.Invoke();
        }

        private IEnumerator SetWindowActiveCroutine(Windows window, bool value)
        {
            EnableWindowComponent enableComponent = null;

            // Получить компонент, отвечающий за открытие/закрытие окна
            switch (window)
            {
                case Windows.Details:
                    enableComponent = detailsWindow.EnableComponent;
                    break;
                case Windows.Search:
                    enableComponent = detailsWindow.EnableComponent;
                    break;

                default:
                    enableComponent = null;
                    break;
            }

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
    }
}