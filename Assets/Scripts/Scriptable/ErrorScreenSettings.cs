using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "ErrorScreenSettings")]
    public class ErrorScreenSettings : ScriptableObject
    {
        private static ErrorScreenSettings _instance;

        private static ErrorScreenSettings instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ErrorScreenSettings>("PSTGU/ErrorScreenSettings");
                }

                return _instance;
            }
        }

        [SerializeField]
        private float shutdownDelay = 2;
        public static float ShutdownDelay { get => instance.shutdownDelay; }
    }
}