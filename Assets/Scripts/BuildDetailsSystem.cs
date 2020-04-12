using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

namespace PSTGU
{
    public class BuildDetailsSystem : MonoBehaviour
    {
        private DetailsWindow detailsWindow;
        private WindowsSettingsRuntime windowsSettingsRuntime;
        private DetailsSettingsRuntime detailsSettingsRuntime;
        private ManagerData managerData;

        private void Awake()
        {
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

            // Установить контент
            SetCotentData(detailsSettingsRuntime.Content);
        }

        private void SetCotentData(PersonContent content)
        {
            detailsWindow.View.NameTxt.text = content.data.ФИО;

            detailsWindow.View.SanTxt.text = string.IsNullOrEmpty(content.data.Сан_ЦеркСлужение) ?
                "Данные отсутствуют" : content.data.Сан_ЦеркСлужение;

            detailsWindow.View.ChinTxt.text = string.IsNullOrEmpty(content.data.Канонизация.Чин_святости)?
                "Данные отсутствуют": content.data.Канонизация.Чин_святости;

            detailsWindow.View.CommentTxt.text = string.IsNullOrEmpty(content.data.Комментарий) ?
                "Данные отсутствуют" : content.data.Комментарий;

            // Загруить фотографии и установить первую фотографию
            StartCoroutine(LoadPhotosAndSetFirstCoroutine(content));
        }

        private IEnumerator LoadPhotosAndSetFirstCoroutine(PersonContent content)
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
