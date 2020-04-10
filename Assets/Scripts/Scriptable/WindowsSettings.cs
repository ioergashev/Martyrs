using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

namespace PSTGU
{
    public enum Windows
    {
        Search,
        Details,
        None
    }

    [Serializable]
    [CreateAssetMenu(fileName = "WindowsSettings")]
    public class WindowsSettings : ScriptableObject
    {
        private static WindowsSettings _instance;

        private static WindowsSettings instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<WindowsSettings>("PSTGU/WindowsSettings");
                }

                return _instance;
            }
        }

        public static Windows CurrentWindow;
        public static Windows TargetWindow;

        public static bool InTransition;

        public static Coroutine TransitionCoroutine;

        public static UnityEvent OnWindowOpened = new UnityEvent();
        public static UnityEvent PrepareTargetWindowRequest = new UnityEvent();

        public static UnityEvent OnStartTransition = new UnityEvent();

        public static UnityEvent OnEndTransition = new UnityEvent();
    }
}