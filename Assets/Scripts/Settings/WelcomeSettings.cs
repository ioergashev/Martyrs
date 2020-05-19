using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "WelcomeSettings")]
    public class WelcomeSettings : ScriptableObject
    {
        private static WelcomeSettings instance;

        public static WelcomeSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<WelcomeSettings>("PSTGU/WelcomeSettings");
                }

                return instance;
            }
        }

        public bool ShowWelcomeInEditor = false;
    }
}