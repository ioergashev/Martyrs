using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PSTGU
{
    public class DetailsSettingsRuntime : MonoBehaviour
    {
        public float ScrollBtnSpeed = 1;

        [HideInInspector]
        public PersonContent Content;

        [HideInInspector]
        public Coroutine LoadPhotosCoroutine;

        [HideInInspector]
        public List<ScrollPhotosItem> ScrollPhotos = new List<ScrollPhotosItem>();

        [HideInInspector]
        public UnityEvent OnPhotosLoaded = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnPhotosSet = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnContentSet = new UnityEvent();
    }
}