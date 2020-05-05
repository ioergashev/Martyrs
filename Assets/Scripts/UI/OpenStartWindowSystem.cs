using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Открывает стартовое окно при запуске приложения </summary>
    public class OpenStartWindowSystem : MonoBehaviour
    {
        private ManagerWindows managerWindows;
        private WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
            managerWindows = FindObjectOfType<ManagerWindows>();
        }

        private void Start()
        {
            managerWindows.OpenWindow(windowsSettingsRuntime.StartWindow);
        }
    }
}
