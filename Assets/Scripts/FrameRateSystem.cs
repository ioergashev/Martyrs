using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    /// <summary> Контролирует частоту кадров </summary>
    public class FrameRateSystem : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = ApplicationSettings.Instance.TargetFrameRate;
        }
    }
}