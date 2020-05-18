using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PSTGU
{
    public enum Windows
    {
        None,
        Search,
        Details
    }

    public enum Screens
    {
        None,
        Loading,
        Error,
        Photos,
        Welcome
    }

    public class WindowsSettingsRuntime : MonoBehaviour
    {
        public Windows StartWindow = Windows.Search;

        [HideInInspector]
        public Windows CurrentWindow = Windows.None;
        
        [HideInInspector]
        public Windows TargetWindow = Windows.None;

        [HideInInspector]
        public Coroutine CheckDoubleClickBackCoroutine;

        [HideInInspector]
        public bool InTransition = false;

        [HideInInspector]
        public Coroutine TransitionCoroutine;

        [HideInInspector]
        public UnityEvent OnStartTransition = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnStartOpenDetails = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnStartCloseWelcome = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnEndTransition = new UnityEvent();

        [HideInInspector]
        public List<Screens> OpenedScreens = new List<Screens>();

        public float WidowsTransitionDuration = 1f;
    }
}