using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Удаляет шаблонные элементы в окнах при старте приложения </summary>
    public class RemoveTemplatesOnStartSystem : MonoBehaviour
    {
        private DetailsWindow detailsWindow;
        private PhotosScreen photosScreen;
        private SearchWindow searchWindow;

        private void Awake()
        {
            detailsWindow = FindObjectOfType<DetailsWindow>();
            photosScreen = FindObjectOfType<PhotosScreen>();
            searchWindow = FindObjectOfType<SearchWindow>();
        }

        private void Start()
        {
            RemoveAllChilds(detailsWindow.View.PhotosContainer);
            RemoveAllChilds(photosScreen.View.PhotosContainer);
            RemoveAllChilds(searchWindow.View.SearchListContainer);
        }

        private void RemoveAllChilds(Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }    
    }
}
