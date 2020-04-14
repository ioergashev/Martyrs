﻿using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    public class ViewPhotosScreenView : MonoBehaviour
    {
        public InputField SearchInput;
        public Button SearchBtn;

        [Header("Pages Navigation")]
        public Button NextPageBtn;
        public Button PrevPageBtn;
        public Text RecordsFoundCountTxt;
        public Text CurrentPageTxt;
        public Text PagesCountTxt;
        public GameObject PagesNavigationContainer;
    }
}