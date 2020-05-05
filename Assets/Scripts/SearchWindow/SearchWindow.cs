using UnityEngine;

namespace PSTGU
{
    [RequireComponent(typeof(SearchViewComponent))]
    [RequireComponent(typeof(EnableWindowComponent))]
    public class SearchWindow : MonoBehaviour
    {
        [HideInInspector]
        public SearchViewComponent View;

        [HideInInspector]
        public EnableWindowComponent EnableComponent;

        private void Awake()
        {
            View = GetComponent<SearchViewComponent>();
            EnableComponent = GetComponent<EnableWindowComponent>();
        }
    }
}