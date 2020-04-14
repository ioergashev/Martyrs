using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PSTGU
{
    public class PhotosScreenSettingsRuntime : MonoBehaviour
    {
        [HideInInspector]
        public List<ScrollPhotosItem> ScrollPhotos = new List<ScrollPhotosItem>();

        [HideInInspector]
        public UnityEvent OnPhotosSet = new UnityEvent();
    }
}