using UnityEngine;

namespace PSTGU
{
    public class DetailsWindow : MonoBehaviour
    {
        [HideInInspector]
        public DetailsViewComponent View;

        [HideInInspector]
        public EnableWindowComponent EnableComponent;

        private void Awake()
        {
            View = GetComponent<DetailsViewComponent>();
            EnableComponent = GetComponent<EnableWindowComponent>();
        }
    }
}