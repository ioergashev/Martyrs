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
    /// <summary> Настраивает окно детализации при открытии </summary>
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
            // Подписаться на запрос открытия окна
            windowsSettingsRuntime.OnStartOpenDetails.AddListener(StartOpenDetailsAction);

            // Ожидать завершения загрузки всех фотографий
            detailsSettingsRuntime.OnPhotosLoaded.AddListener(PhotosLoadedAction); 
        }

        private void PhotosLoadedAction()
        {
            // Установить загруженные фотграфии
            SetPhotos(detailsSettingsRuntime.Content.data.Фотографии);
        }

        private void StartOpenDetailsAction()
        {
            ResetPhotos();

            // Установить контент
            SetCotentData(detailsSettingsRuntime.Content);
        }

        /// <summary> Настроить окно в соответствии с контентом </summary>
        private void SetCotentData(PersonContent content)
        {
            // Установить ФИО
            detailsWindow.View.NameTxt.text = content.data.ФИО;

            //---------------------------------------------
            // Проверить наличие аттрибутов
            bool sanExist = !string.IsNullOrEmpty(content.data.Сан_ЦеркСлужение);
            bool chinExist = !string.IsNullOrEmpty(content.data.Канонизация.Чин_святости);

            // Подчеркивание должно быть, если имеется сан и чин святости
            detailsWindow.View.SanUnderlineImg.gameObject.SetActive(sanExist && chinExist);

            // Включить/выключить текст сана
            detailsWindow.View.SanTxt.gameObject.SetActive(sanExist);

            // Включить/выключить текст чина
            detailsWindow.View.ChinTxt.gameObject.SetActive(chinExist);

            // Установить сан
            detailsWindow.View.SanTxt.text = content.data.Сан_ЦеркСлужение;

            // Установить чин святости
            detailsWindow.View.ChinTxt.text = content.data.Канонизация.Чин_святости;

            //---------------------------------------------
            // Проверить наличие комментария
            bool commentExist = !string.IsNullOrEmpty(content.data.Комментарий);

            // Включить/выключить кнопку комментария
            detailsWindow.View.CommentBtn.gameObject.SetActive(commentExist);

            // Выключить комментарий
            detailsWindow.View.CommentBGImg.gameObject.SetActive(false);

            // Установить комментарий
            detailsWindow.View.CommentTxt.text = content.data.Комментарий;

            //---------------------------------------------
            // Проверить наличие событий
            bool eventsExist = content.data.События != null && content.data.События.Count != 0;

            // Включить/выключить кнопку событий
            detailsWindow.View.EventsBtn.gameObject.SetActive(eventsExist);

            // Выключить события
            detailsWindow.View.EventsBGImg.gameObject.SetActive(false);

            // Установить события
            detailsWindow.View.EventsTxt.text = FormatEvents(content.data.События);

            detailsSettingsRuntime.OnContentSet?.Invoke();
        }

        private string FormatEvents(List<PersonContent.Event> events)
        {
            string result = string.Empty;

            foreach (var событие in events)
            {
                // Если имеется датировка и текст
                if (!string.IsNullOrEmpty(событие.Датировка) && !string.IsNullOrEmpty(событие.Текст))
                {
                    result += string.Format("{0} - {1}\n", событие.Датировка, событие.Текст);
                }
                // Если имеется текст
                else if (!string.IsNullOrEmpty(событие.Текст))
                {
                    result += string.Format("{0}\n", событие.Текст);
                }
            }

            return result;
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

            // Выключить фотографии
            ScrollSnap.gameObject.SetActive(false);

            // Очистить старый список
            detailsSettingsRuntime.ScrollPhotos.Clear();
        }

        private SimpleScrollSnap ScrollSnap
        {
            get
            {
                var value = detailsWindow.View.PhotosScroll;

                return value;
            }
        }

        private void SetPhotos(List<PersonContent.PhotoItem> photos)
        {
            // Если фотографии имеются
            if (photos.Count != 0)
            {
                // Включить фотографии
                ScrollSnap.gameObject.SetActive(true);
            }

            // Установить фотографии
            for (int i = 0; i < photos.Count; i++)
            {
                var texture = photos[i].Texture;

                // Если отсутсвует текстура
                if (texture == null)
                {
                    continue;
                }

                // Добавить префаб в Scroll Snap
                ScrollSnap.AddToBack(DetailsSettings.Instance.PhotoPrefab);

                // Получить элемент из Scroll Snap
                var instance = ScrollSnap.Panels.Last().GetComponent<ScrollPhotosItem>();

                // Установить текстуру
                instance.View.PhotoImg.texture = texture;

                // Настроить соотношение сторон
                instance.View.PhotoAspectRatio.aspectRatio = (float)texture.width / texture.height;

                // Добавить фото в список фотографий
                detailsSettingsRuntime.ScrollPhotos.Add(instance);
            }

            // Сообщить, что фотографии установлены
            detailsSettingsRuntime.OnPhotosSet?.Invoke();
        }
    }
}
