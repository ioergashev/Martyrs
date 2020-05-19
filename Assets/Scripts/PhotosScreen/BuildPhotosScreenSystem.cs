using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;

namespace PSTGU
{
    /// <summary> Настраивает окно фотографий </summary>
    public class BuildPhotosScreenSystem : MonoBehaviour
    {
        private PhotosScreen _photosScreen;
        private PhotosScreenSettingsRuntime _photosScreenSettingsRuntime;

        private void Awake()
        {
            _photosScreen = FindObjectOfType<PhotosScreen>();
            _photosScreenSettingsRuntime = FindObjectOfType<PhotosScreenSettingsRuntime>();
        }

        private void Start()
        {
            // Подписаться на запрос установки фотографий
            _photosScreenSettingsRuntime.SetPhotosRequest.AddListener(SetPhotosRequestAction);

            // Подписаться на запрос сброса фотографий
            _photosScreenSettingsRuntime.ResetPhotosRequest.AddListener(ResetPhotosRequestAction);
        }

        private void ResetPhotosRequestAction()
        {
            ResetPhotos();
        }

        private void SetPhotosRequestAction()
        {
            SetPhotos(_photosScreenSettingsRuntime.Photos);
        }

        private void ResetPhotos()
        {
            if (ScrollSnap.Panels != null)
            {
                // Удалить старые фотографии
                for (int i = ScrollSnap.Panels.Length - 1; i >= 0; i--)
                {
                    ScrollSnap.Remove(i);
                }
            }

            // Очистить старый список
            _photosScreenSettingsRuntime.ScrollPhotos.Clear();
        }

        private SimpleScrollSnap ScrollSnap
        {
            get
            {
                var value = _photosScreen.View.PhotosScroll;

                return value;
            }
        }

        private void SetPhotos(List<PersonContent.PhotoItem> photos)
        {
            // Установить фотографии
            for (int i = 0; i< photos.Count; i++)
            {
                var texture = photos[i].Texture;

                // Если отсутсвует текстура
                if (texture == null)
                {
                    continue;
                }

                // Добавить префаб в Scroll Snap
                ScrollSnap.AddToBack(PhotosScreenSettings.Instance.PhotoPrefab);

                // Получить элемент из Scroll Snap
                var instance = ScrollSnap.Panels.Last().GetComponent<ScrollPhotosItem>();

                // Установить текстуру
                instance.View.PhotoImg.texture = texture;

                // Настроить соотношение сторон
                instance.View.PhotoAspectRatio.aspectRatio = (float)texture.width / texture.height;

                // Проверить наличие подписи
                bool signExist = !string.IsNullOrEmpty(photos[i].Подпись);

                //// Включить/выключить подпись
                //instance.View.PhotoSignBGImg.gameObject.SetActive(signExist);

                //// Установить подпись
                //instance.View.PhotoSignTxt.text = photos[i].Подпись;

                // Добавить фото в список фотографий
                _photosScreenSettingsRuntime.ScrollPhotos.Add(instance);
            }

            // Сообщить, что фотографии установлены
            _photosScreenSettingsRuntime.OnPhotosSet?.Invoke();
        }
    }
}
