﻿using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

namespace PSTGU
{
    public class BuildDetailsPhotosSystem : MonoBehaviour
    {
        private DetailsWindow _detailsWindow;
        private DetailsSettingsRuntime _detailsSettingsRuntime;

        private void Awake()
        {
            _detailsWindow = FindObjectOfType<DetailsWindow>();
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
            _detailsSettingsRuntime.ScrollPhotos.ForEach(p => Destroy(p.gameObject));
            _detailsSettingsRuntime.ScrollPhotos.Clear();

            // Получить список фотографий
            var photos = _detailsSettingsRuntime.Content.data.Фотографии.Select(p => p.Texture).Where(t => t != null);

            // Установить фотографии
            foreach(var photo in photos)
            {
                // Вставить фотографию
                var instance = Instantiate(DetailsSettings.Instance.PhotoPrefab, _detailsWindow.View.PhotosContainer)
                    .GetComponent<ScrollPhotosItem>();

                // Установить фотографию
                instance.View.PhotoImg.texture = photo;

                // Настроить соотношение сторон
                instance.View.PhotoAspectRatio.aspectRatio = (float)photo.width / photo.height;
            }

            // Сообщить, что фотографии установлены
            _detailsSettingsRuntime.OnPhotosSet?.Invoke();
        }
    }
}
