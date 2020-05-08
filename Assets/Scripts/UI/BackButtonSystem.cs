using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class BackButtonSystem : MonoBehaviour
    {
        private ManagerWindows _managerWindows;
        private WindowsSettingsRuntime _windowsSettingsRuntime;

        private void Awake()
        {
            _managerWindows = FindObjectOfType<ManagerWindows>();
            _windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
        }

        private void Update()
        {
            if (!Input.GetKeyUp(KeyCode.Escape))
            {
                return;
            }

            // Если открыто хотя бы одно всплывающее окно
            if(_windowsSettingsRuntime.OpenedScreens.Count >= 1)
            {
                // Закрыть последнее открытое всплывающее окно
                _managerWindows.CloseLastOpenedScreen();
            }
            // Если открыто окно детализации
            else if (_windowsSettingsRuntime.CurrentWindow == Windows.Details)
            {
                // Открыть окно поиска
                _managerWindows.OpenWindow(Windows.Search);
            }
            // Если открыто окно поиска
            else if (_windowsSettingsRuntime.CurrentWindow == Windows.Search)
            {
                // Завершить работу
                Quit();
            }
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    //Application.Quit();
    System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
        }
    }
}
