using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System;

namespace PSTGU
{
    public class ExpandPhotoSystem : MonoBehaviour
    {
        private PhotosScreen _photosScreen;
        private PhotosScreenSettingsRuntime _photosScreenSettingsRuntime;
        private DetailsSettingsRuntime _detailsSettingsRuntime;

        private bool _detailsPhotosReady = false;
        private bool _photosScreenReady = false;

        private void Awake()
        {
            _photosScreen = FindObjectOfType<PhotosScreen>();
            _photosScreenSettingsRuntime = FindObjectOfType<PhotosScreenSettingsRuntime>();
            _detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
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
            if (_detailsPhotosReady && _photosScreenReady) 
            {
                for(int i = 0; i< _photosScreenSettingsRuntime.ScrollPhotos.Count && i < _photosScreen.View.PhotosScroll.NumberOfPanels; i++)
                {
                    int index = i;

                    // Связать фотографию из окна детализации с фотографией в окне фотографий
                    _photosScreenSettingsRuntime.ScrollPhotos[index].View.PhotoBtn.onClick.AddListener(
                        () =>
                        {
                            _photosScreen.View.PhotosScroll.GoToPanel(index+1);
                        });
                }

                _detailsPhotosReady = false;
                _photosScreenReady = false;
            }
        }
    }
}
