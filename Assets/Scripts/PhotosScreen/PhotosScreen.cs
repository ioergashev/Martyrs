using UnityEngine;

namespace PSTGU
{
    [RequireComponent(typeof(PhotosScreenViewComponent))]
    [RequireComponent(typeof(EnableWindowComponent))]
    public class PhotosScreen : MonoBehaviour
    {
        [HideInInspector]
        public PhotosScreenViewComponent View;

        [HideInInspector]
        public EnableWindowComponent EnableComponent;

        private void Awake()
        {
            View = GetComponent<PhotosScreenViewComponent>();
            EnableComponent = GetComponent<EnableWindowComponent>();
        }
    }
}