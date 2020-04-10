using UnityEngine;

namespace PSTGU
{
    public class DetailsWindow : MonoBehaviour
    {
        private static DetailsWindow _instance;

        private static DetailsWindow instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DetailsWindow>();
                }
                return _instance;
            }
        }

        [SerializeField]
        private DetailsViewComponent view;

        public static DetailsViewComponent View
        {
            get { return instance.view; }
        }

        [SerializeField]
        private EnableWindowComponent enableComponent;

        public static EnableWindowComponent EnableComponent
        {
            get { return instance.enableComponent; }
        }
    }
}