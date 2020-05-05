using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System;

namespace PSTGU
{
    /// <summary> Открывает фотографию в окне фотографий при нажатии на фотографию из другого окна </summary>
    public class ExpandPhotoSystem : MonoBehaviour
    {
        private PhotosScreen _photosScreen;
        private PhotosScreenSettingsRuntime _photosScreenSettingsRuntime;
        private DetailsSettingsRuntime _detailsSettingsRuntime;
        private ManagerWindows managerWindows;

        private bool _detailsPhotosReady = false;
        private bool _photosScreenReady = false;

        private void Awake()
        {
            _photosScreen = FindObjectOfType<PhotosScreen>();
            _photosScreenSettingsRuntime = FindObjectOfType<PhotosScreenSettingsRuntime>();
            _detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
            managerWindows = FindObjectOfType<ManagerWindows>();
        }

        private void Start()
        {
            _detailsSettingsRuntime.OnPhotosSet.AddListener(DetailsPhotosSetAction);
            _photosScreenSettingsRuntime.OnPhotosSet.AddListener(PhotosScreenPhotosSetAction);
        }

        private void DetailsPhotosSetAction()
        {
            _detailsPhotosReady = true;
        }

        private void PhotosScreenPhotosSetAction()
        {
            _photosScreenReady = true;
        }

        private void Update()
        {
            // Если установлены фотографии для окна детализации и окна фотографий
            if (_detailsPhotosReady && _photosScreenReady) 
            {
                _detailsPhotosReady = false;
                _photosScreenReady = false;


                for (int i = 0; i< Mathf.Min(_detailsSettingsRuntime.ScrollPhotos.Count, _photosScreen.View.PhotosScroll.NumberOfPanels); i++)
                {
                    int index = i;

                    // Связать фотографию из окна детализации с фотографией в окне фотографий
                    _detailsSettingsRuntime.ScrollPhotos[index].View.PhotoBtn.onClick.AddListener(
                        () =>
                        {
                            // Установить правильную фотографию в окне фотографий
                            _photosScreen.View.PhotosScroll.GoToPanel(index);

                            // Показать окно фотографий
                            managerWindows.SetScreenActive(Screens.Photos, true);
                        });
                }
            }
        }
    }
}
