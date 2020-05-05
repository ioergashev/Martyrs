using PSTGU.ServerCommunication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PSTGU
{
    public class ManagerData : MonoBehaviour
    {
        private ManagerServer managerServer;

        private void Awake()
        {
            managerServer = FindObjectOfType<ManagerServer>();
        }

        // Загрузить недостающие фотографии по данному досье
        public IEnumerator LoadUnloadedPhotos(List<PersonContent.PhotoItem> photoItems)
        {
            foreach(var photoItem in photoItems)
            {
                // Если фото уже загружено
                if(photoItem.Texture != null)
                {
                    continue;
                }

                // Сформировать запрос
                var operation = LoadPhoto(photoItem);

                // Выполнить запрос
                yield return operation;

                // Получить ответ
                var response = operation.Current as Texture2D;

                // Сохранить фотографию
                photoItem.Texture = response;
            }
        }

        public IEnumerator LoadPhoto(PersonContent.PhotoItem photoItem)
        {
            // Сформировать запрос
            var operation = managerServer.DownloadPhoto(photoItem.Фото_сжатое.fileContentUrl);

            // Выполнить запрос
            yield return operation;

            // Получить ответ
            var response = operation.Current as Texture2D;

            yield return response;
        }
    }
}