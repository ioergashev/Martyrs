using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PSTGU
{
    public enum Windows
    {
        Search,
        Details,
        None
    }

    public class WindowsSettingsRuntime : MonoBehaviour
    {
        [HideInInspector]
        public Windows CurrentWindow = Windows.None;
        
        [HideInInspector]
        public Windows TargetWindow = Windows.None;

        [HideInInspector]
        public bool InTransition = false;

        [HideInInspector]
        public Coroutine TransitionCoroutine;

        [HideInInspector]
        public UnityEvent OnStartTransition = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnEndTransition = new UnityEvent();
    }
}