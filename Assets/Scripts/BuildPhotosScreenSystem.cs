using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

namespace PSTGU
{
    public class BuildPhotosScreenSystem : MonoBehaviour
    {
        private PhotosScreen _photosScreen;
        private PhotosScreenSettingsRuntime _photosScreenSettingsRuntime;
        private DetailsSettingsRuntime _detailsSettingsRuntime;

        private void Awake()
        {
            _photosScreen = FindObjectOfType<PhotosScreen>();
            _photosScreenSettingsRuntime = FindObjectOfType<PhotosScreenSettingsRuntime>();
            _detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
        }

        private void Start()
        {
            _detailsSettingsRuntime.OnPhotosLoaded.AddListener(PhotosLoadedAction);
        }

        private void PhotosLoadedAction()
        {
            SetPhotos();
        }

        private void SetPhotos()
        {
            // Очистить старый список
            _photosScreenSettingsRuntime.ScrollPhotos.ForEach(p => Destroy(p.gameObject));
            _photosScreenSettingsRuntime.ScrollPhotos.Clear();

            // Получить список фотографий
            var photos = _detailsSettingsRuntime.Content.data.Фотографии.Select(p => p.Texture).Where(t => t != null);

            // Установить фотографии
            foreach (var photo in photos)
            {
                // Вставить фотографию
                var instance = Instantiate(DetailsSettings.Instance.PhotoPrefab, _photosScreen.View.PhotosContainer)
                    .GetComponent<ScrollPhotosItem>();

                // Установить фотографию
                instance.View.PhotoImg.texture = photo;

                // Настроить соотношение сторон
                instance.View.PhotoAspectRatio.aspectRatio = (float)photo.width / photo.height;
            }

            // Сообщить, что фотографии установлены
            _photosScreenSettingsRuntime.OnPhotosSet?.Invoke();
        }
    }
}
