using UnityEngine;

namespace PSTGU
{
    public class SearchWindow : MonoBehaviour
    {
        private static SearchWindow _instance;

        private static SearchWindow instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SearchWindow>();
                }
                return _instance;
            }
        }

        [SerializeField]
        private SearchViewComponent view;

        public static SearchViewComponent View
        {
            get { return instance.view; }
        }

        [SerializeField]
        private SearchListComponent searchList;

        public static SearchListComponent SearchList
        {
            get { return instance.searchList; }
        }

        [SerializeField]
        private EnableWindowComponent enableComponent;

        public static EnableWindowComponent EnableComponent
        {
            get { return instance.enableComponent; }
        }
    }
}