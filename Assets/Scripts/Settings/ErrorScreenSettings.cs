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
        private static ErrorScreenSettings instance;

        public static ErrorScreenSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<ErrorScreenSettings>("PSTGU/ErrorScreenSettings");
                }

                return instance;
            }
        }

        public float ShutdownDelay = 2;
    }
}