using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSTGU
{
    public class EnableWindowComponent : MonoBehaviour
    {
        [HideInInspector]
        public bool Enable;

        [HideInInspector]
        public UnityEvent ShowRequest = new UnityEvent();

        [HideInInspector]
        public UnityEvent HideRequest = new UnityEvent();
    }
}