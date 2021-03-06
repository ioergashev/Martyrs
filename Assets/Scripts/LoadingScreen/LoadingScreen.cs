﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    [RequireComponent(typeof(LoadingViewComponent))]
    [RequireComponent(typeof(EnableWindowComponent))]
    public class LoadingScreen : MonoBehaviour
    {
        [HideInInspector]
        public LoadingViewComponent View;

        [HideInInspector]
        public EnableWindowComponent EnableComponent;

        private void Awake()
        {
            View = GetComponent<LoadingViewComponent>();
            EnableComponent = GetComponent<EnableWindowComponent>();
        }
    }
}