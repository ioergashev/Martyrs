using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    public class ErrorScreen : MonoBehaviour
    {
        private static ErrorScreen _instance;

        private static ErrorScreen instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ErrorScreen>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = FindObjectOfType<ErrorScreen>();
        }

        [SerializeField]
        private ErrorViewComponent view;

        public static ErrorViewComponent View
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