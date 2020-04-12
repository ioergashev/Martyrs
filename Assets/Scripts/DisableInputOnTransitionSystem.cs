using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
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
