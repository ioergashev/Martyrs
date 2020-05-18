using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    public class SearchViewComponent : MonoBehaviour
    {
        [Header("Header")]
        public InputField SearchInput;
        public Button SearchBtn;

        [Header("Body")]
        public ScrollRect SearchScrollRect;
        public Transform SearchListContainer;

        [Header("Bottom")]
        public Button NextPageBtn;
        public Button PrevPageBtn;
        public Text RecordsFoundCountTxt;
        public Text PagesCountTxt;
        public GameObject PagesNavigationContainer;       
    }
}