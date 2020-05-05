using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    [RequireComponent(typeof(ErrorViewComponent))]
    [RequireComponent(typeof(EnableWindowComponent))]
    public class ErrorScreen : MonoBehaviour
    {
        [HideInInspector]
        public ErrorViewComponent View;

        [HideInInspector]
        public EnableWindowComponent EnableComponent;

        private void Awake()
        {
            View = GetComponent<ErrorViewComponent>();
            EnableComponent = GetComponent<EnableWindowComponent>();
        }
    }
}