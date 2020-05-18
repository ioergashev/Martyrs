using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    [RequireComponent(typeof(TransitionViewComponent))]
    public class TransitionScreen : MonoBehaviour
    {
        [HideInInspector]
        public TransitionViewComponent View;

        private void Awake()
        {
            View = GetComponent<TransitionViewComponent>();
        }
    }
}