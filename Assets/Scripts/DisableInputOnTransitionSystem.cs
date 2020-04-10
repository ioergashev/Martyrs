using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class DisableInputOnTransitionSystem : MonoBehaviour
    {
        private void Update()
        {
            UI.GraphicRaycaster.enabled = WindowsSettings.TransitionCoroutine == null;
        }
    }
}
