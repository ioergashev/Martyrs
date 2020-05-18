using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    public class SearchViewComponent : MonoBehaviour
    {
        [Header("Search")]
        public InputField SearchInput;
        public Button SearchBtn;
        public ScrollRect SearchScrollRect;


        [Header("Pages Navigation")]
        public Button NextPageBtn;
        public Button PrevPageBtn;
        public Text RecordsFoundCountTxt;
        public Text PagesCountTxt;
        public GameObject PagesNavigationContainer;
        public Transform SearchListContainer;
    }
}