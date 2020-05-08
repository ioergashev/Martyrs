using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    [RequireComponent(typeof(WelcomeViewComponent))]
    [RequireComponent(typeof(EnableWindowComponent))]
    public class WelcomeScreen : MonoBehaviour
    {
        [HideInInspector]
        public WelcomeViewComponent View;

        [HideInInspector]
        public EnableWindowComponent EnableComponent;

        private void Awake()
        {
            View = GetComponent<WelcomeViewComponent>();
            EnableComponent = GetComponent<EnableWindowComponent>();
        }
    }
}