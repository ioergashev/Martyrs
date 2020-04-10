using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    public class UI : MonoBehaviour
    {
        private static UI _instance;

        private static UI instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UI>();
                }
                return _instance;
            }
        }

        [SerializeField]
        private GraphicRaycaster graphicRaycaster;

        public static GraphicRaycaster GraphicRaycaster
        {
            get { return instance.graphicRaycaster; }
        }
    }
}