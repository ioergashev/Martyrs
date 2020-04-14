using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System;

namespace PSTGU
{
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
            _windowsSettingsRuntime.OnStartTransition.AddListener(StartWindowTransitionAction);
        }

        private void StartWindowTransitionAction()
        {
            // Если другое окно
            if (_windowsSettingsRuntime.TargetWindow != Windows.Details)
            {
                return;
            }

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
