using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Отключает ввод пользователя во время перехода между окнами </summary>
    public class DisableInputOnTransitionSystem : MonoBehaviour
    {
        private UI _UI;
        private WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            _UI = FindObjectOfType<UI>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
        }

        private void Update()
        {
            _UI.GraphicRaycaster.enabled = windowsSettingsRuntime.TransitionCoroutine == null;
        }
    }
}
