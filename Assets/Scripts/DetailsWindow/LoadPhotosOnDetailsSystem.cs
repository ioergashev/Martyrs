using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System;

namespace PSTGU
{
    /// <summary> Загружает незагруженные фотографии при открытие окна детализации </summary>
    public class LoadPhotosOnDetailsSystem : MonoBehaviour
    {
        private WindowsSettingsRuntime _windowsSettingsRuntime;
        private DetailsSettingsRuntime _detailsSettingsRuntime;
        private ManagerData _managerData;

        private void Awake()
        {
            _windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
            _detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
            _managerData = FindObjectOfType<ManagerData>();
        }

        private void Start()
        {
            // Подписаться на открытие окна
            _windowsSettingsRuntime.OnStartOpenDetails.AddListener(StartWindowTransitionAction);
        }

        private void StartWindowTransitionAction()
        {
            // Если уже выполняется загрузка фотографий
            if(_detailsSettingsRuntime.LoadPhotosCoroutine != null)
            {
                // Остановить предыдущую загрузку
                StopCoroutine(_detailsSettingsRuntime.LoadPhotosCoroutine);
            }

            // Начать загрузку фотографий по данному досье
            _detailsSettingsRuntime.LoadPhotosCoroutine = StartCoroutine(LoadPhotos());
        }

        private IEnumerator LoadPhotos()
        {
            // Загрузить недостающие фотографии по данному досье
            yield return _managerData.LoadUnloadedPhotos(_detailsSettingsRuntime.Content.data.Фотографии);

            _detailsSettingsRuntime.LoadPhotosCoroutine = null;

            // Сообщить о конце загрузки
            _detailsSettingsRuntime.OnPhotosLoaded?.Invoke();
        }
    }
}
