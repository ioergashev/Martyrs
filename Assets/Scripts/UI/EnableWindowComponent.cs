using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSTGU
{
    public enum WindowTransitionType
    {
        None,
        Fade
    }

    public class EnableWindowComponent : MonoBehaviour
    {
        public WindowTransitionType TransitionType = WindowTransitionType.None;

        [HideInInspector]
        public bool Enable;

        [HideInInspector]
        public UnityEvent ShowRequest = new UnityEvent();

        [HideInInspector]
        public UnityEvent HideRequest = new UnityEvent();
    }
}