using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

namespace PSTGU
{
    public class BuildScrollPhotosSystem : MonoBehaviour
    {
        private ViewPhotosScreen viewPhotosScreen;
        private DetailsWindow detailsWindow;
        private WindowsSettingsRuntime windowsSettingsRuntime;
        private DetailsSettingsRuntime detailsSettingsRuntime;
        private ManagerData managerData;

        private void Awake()
        {
            viewPhotosScreen = FindObjectOfType<ViewPhotosScreen>();
            detailsWindow = FindObjectOfType<DetailsWindow>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
            detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
            managerData = FindObjectOfType<ManagerData>();
        }

        private void Start()
        {
            windowsSettingsRuntime.OnStartTransition.AddListener(StartWindowTransitionAction);
        }

        private void StartWindowTransitionAction()
        {
            // Если другое окно
            if (windowsSettingsRuntime.TargetWindow != Windows.Details)
            {
                return;
            }

            // Загруить и установить фотографии
            StartCoroutine(LoadAndSetPhotosCoroutine(detailsSettingsRuntime.Content));
        }

        private IEnumerator LoadAndSetPhotosCoroutine(PersonContent content)
        {
            // Установить подпись по-умолчанию
            detailsWindow.View.PhotoSignTxt.text = string.Empty;

            // Показать изображение загрузки
            detailsWindow.View.LoadingImg.gameObject.SetActive(true);

            // Загрузить недоставющие фотографии по данному досье
            yield return managerData.LoadUnloadedPhotos(content.data.Фотографии);

            // Спрятать изображение загрузки
            detailsWindow.View.LoadingImg.gameObject.SetActive(false);

            // Показать изображение по-умолчанию
            detailsWindow.View.DefaultImg.gameObject.SetActive(true);

            // Получить первую фотографию
            var photoItem = content.data.Фотографии.FirstOrDefault(p => p.Texture != null);

            // Получить текстуру
            var photo = photoItem?.Texture;

            if (photo != null)
            {
                // Спрятать изображение по-умолчанию
                detailsWindow.View.DefaultImg.gameObject.SetActive(false);

                // Установить фотографию
                detailsWindow.View.PhotoImg.texture = photo;

                // Настроить соотношение сторон
                detailsWindow.View.PhotoAspectRatio.aspectRatio = (float)photo.width / photo.height;

                // Установить подпись
                detailsWindow.View.PhotoSignTxt.text = photoItem.Подпись;
            }
        }
    }
}
