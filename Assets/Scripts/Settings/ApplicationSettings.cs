using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "ApplicationSettings")]
    public class ApplicationSettings : ScriptableObject
    {
        private static ApplicationSettings instance;

        public static ApplicationSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<ApplicationSettings>("PSTGU/ApplicationSettings");
                }

                return instance;
            }
        }

        public int TargetFrameRate = 60;
    }
}