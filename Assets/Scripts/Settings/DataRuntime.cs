using UnityEngine;
using System;
using System.Collections.Generic;

namespace PSTGU
{
    public class DataRuntime : MonoBehaviour
    {
        [HideInInspector]
        public List<PersonContent> SearchResponse = new List<PersonContent>();
    }
}