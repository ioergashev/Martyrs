using System.Collections;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

namespace PSTGU
{
    /// <summary> Исправляет ошибку ScrollSnap с дергающимися панелями</summary>
    public class FixScrollSnapSystem : MonoBehaviour
    {
        private DetailsWindow detailsWindow;

        private void Awake()
        {
            detailsWindow = FindObjectOfType<DetailsWindow>();
        }

        private void Start()
        {
            StartCoroutine(FixScrollSnapIEnumerator());
        }

        private IEnumerator FixScrollSnapIEnumerator()
        {
            yield return null;

            bool wasActive = ScrollSnap.gameObject.activeSelf;

            // Включить фотографии
            ScrollSnap.gameObject.SetActive(true);

            // Добавить префаб в Scroll Snap
            ScrollSnap.AddToBack(DetailsSettings.Instance.PhotoPrefab);

            // Удалить элемент из списка
            ScrollSnap.RemoveFromBack();

            ScrollSnap.gameObject.SetActive(wasActive);
        }

        private SimpleScrollSnap ScrollSnap
        {
            get
            {
                var value = detailsWindow.View.PhotosScroll;

                return value;
            }
        }
    }
}
