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
    /// <summary> Настраивает окно фотографий при открытии окна детализации </summary>
    public class BuildPhotosScreenOnDetailsSystem : MonoBehaviour
    {
        private PhotosScreenSettingsRuntime _photosScreenSettingsRuntime;
        private DetailsSettingsRuntime detailsSettingsRuntime;
        private WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            _photosScreenSettingsRuntime = FindObjectOfType<PhotosScreenSettingsRuntime>();
            detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
        }

        private void Start()
        {
            // Подписаться на запрос открытия окна
            windowsSettingsRuntime.OnStartOpenDetails.AddListener(StartOpenDetailsAction);

            // Ожидать завершения загрузки всех фотографий
            detailsSettingsRuntime.OnPhotosLoaded.AddListener(PhotosLoadedAction);
        }

        private void StartOpenDetailsAction()
        {
            _photosScreenSettingsRuntime.ResetPhotosRequest?.Invoke();
        }

        private void PhotosLoadedAction()
        {
            _photosScreenSettingsRuntime.Photos = detailsSettingsRuntime.Content.data.Фотографии;

            _photosScreenSettingsRuntime.SetPhotosRequest?.Invoke();
        }
    }
}
