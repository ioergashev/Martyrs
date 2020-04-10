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
        private void Start()
        {
            WindowsSettings.OnStartTransition.AddListener(StartWindowTransitionAction);
        }

        private void StartWindowTransitionAction()
        {
            // Если другое окно
            if (WindowsSettings.TargetWindow != Windows.Details)
            {
                return;
            }

            // Установить контент
            SetCotentData(DetailsSettings.Content);
        }

        private void SetCotentData(PersonContent content)
        {
            DetailsWindow.View.NameTxt.text = content.data.ФИО;

            DetailsWindow.View.SanTxt.text = string.IsNullOrEmpty(content.data.Сан_ЦеркСлужение) ?
                "Данные отсутствуют" : content.data.Сан_ЦеркСлужение;

            DetailsWindow.View.ChinTxt.text = string.IsNullOrEmpty(content.data.Канонизация.Чин_святости)?
                "Данные отсутствуют": content.data.Канонизация.Чин_святости;

            DetailsWindow.View.CommentTxt.text = string.IsNullOrEmpty(content.data.Комментарий) ?
                "Данные отсутствуют" : content.data.Комментарий;

            // Загруить фотографии и установить первую фотографию
            StartCoroutine(LoadPhotosAndSetFirstCoroutine(content));
        }

        private IEnumerator LoadPhotosAndSetFirstCoroutine(PersonContent content)
        {
            // Установить подпись по-умолчанию
            DetailsWindow.View.PhotoSignTxt.text = string.Empty;

            // Показать изображение загрузки
            DetailsWindow.View.LoadingImg.gameObject.SetActive(true);

            // Загрузить все фотографии по данному досье
            yield return ManagerData.LoadPersonPhotos(content.data.Фотографии);

            // Спрятать изображение загрузки
            DetailsWindow.View.LoadingImg.gameObject.SetActive(false);

            // Показать изображение по-умолчанию
            DetailsWindow.View.DefaultImg.gameObject.SetActive(true);

            // Получить первую фотографию
            var photoItem = content.data.Фотографии.FirstOrDefault(p => p.Texture != null);

            // Получить текстуру
            var photo = photoItem?.Texture;

            if (photo != null)
            {
                // Спрятать изображение по-умолчанию
                DetailsWindow.View.DefaultImg.gameObject.SetActive(false);

                // Установить фотографию
                DetailsWindow.View.PhotoImg.texture = photo;

                // Настроить соотношение сторон
                DetailsWindow.View.PhotoAspectRatio.aspectRatio = (float)photo.width / photo.height;

                // Установить подпись
                DetailsWindow.View.PhotoSignTxt.text = photoItem.Подпись;
            }
        }
    }
}
