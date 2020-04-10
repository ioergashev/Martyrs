using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU.ServerCommunication
{
    [Serializable]
    public class SearchResponse: SimpleResponse
    {
        public PersonContent[] data;
    }
}