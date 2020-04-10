using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    public class ManagerWindows : MonoBehaviour
    {
        private static ManagerWindows _instance;

        private static ManagerWindows instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ManagerWindows>();
                }
                return _instance;
            }
        }

        /// <summary>См. <seealso cref="WindowsSettings.TransitionCoroutine"/> - процесс открытия окна </summary>
        public static void OpenWindow(Windows window)
        {
            // Если уже выполняется переход между окнами
            if (WindowsSettings.TransitionCoroutine != null)
            {
                return;
            }

            // Указать целевое окно
            WindowsSettings.TargetWindow = window;

            // Начать открытие окна и сохранить операцию
            WindowsSettings.TransitionCoroutine = instance.StartCoroutine(OpenWindowCoroutine(window));

            // Сообщить о начале перехода
            WindowsSettings.OnStartTransition?.Invoke();
        }

        private static IEnumerator OpenWindowCoroutine(Windows window)
        {
            // Закрыть текущее открытое окно
            yield return SetWindowActiveCroutine(WindowsSettings.CurrentWindow, false);

            // Указать открытое окно
            WindowsSettings.CurrentWindow = Windows.None;

            // Окрыть целевое окно
            yield return SetWindowActiveCroutine(window, true);

            // Указать открытое окно
            WindowsSettings.CurrentWindow = window;

            // Завершить операцию
            WindowsSettings.TransitionCoroutine = null;

            // Сообщить о завершении перехода
            WindowsSettings.OnEndTransition?.Invoke();
        }

        private static IEnumerator SetWindowActiveCroutine(Windows window, bool value)
        {
            EnableWindowComponent enableComponent;

            // Получить компонент, отвечающий за открытие/закрытие окна
            switch (window)
            {
                case Windows.Details:
                    enableComponent = DetailsWindow.EnableComponent;
                    break;
                case Windows.Search:
                    enableComponent = SearchWindow.EnableComponent;
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