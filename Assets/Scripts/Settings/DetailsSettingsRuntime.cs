using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PSTGU
{
    public class DetailsSettingsRuntime : MonoBehaviour
    {
        [HideInInspector]
        public PersonContent Content;

        [HideInInspector]
        public Coroutine LoadPhotoCoroutine;
    }
}