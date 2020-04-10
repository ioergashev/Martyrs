using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PSTGU.ServerCommunication;

namespace PSTGU
{
    public class PersonListItem : MonoBehaviour
    {
        [HideInInspector]
        public PersonContent Content;
        public PersonListItemViewComponent View;
    }
}