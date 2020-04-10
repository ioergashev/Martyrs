using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    public class LoadingScreen : MonoBehaviour
    {
        private static LoadingScreen _instance;

        private static LoadingScreen instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LoadingScreen>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = FindObjectOfType<LoadingScreen>();
        }

        [SerializeField]
        private LoadingViewComponent view;

        public static LoadingViewComponent View
        {
            get { return instance.view; }
        }

        [SerializeField]
        private EnableWindowComponent enableComponent;

        public static EnableWindowComponent EnableComponent
        {
            get { return instance.enableComponent; }
        }
    }
}