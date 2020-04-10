using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PSTGU.ServerCommunication
{
    public static class ManagerData
    {
        // Загрузить все фотографии по данному досье
        public static IEnumerator LoadPersonPhotos(List<PhotoItem> photoItems)
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

        public static IEnumerator LoadPhoto(PhotoItem photoItem)
        {
            // Сформировать запрос
            var operation = ManagerServer.DownloadPhoto(photoItem.Фото_сжатое.fileContentUrl);

            // Выполнить запрос
            yield return operation;

            // Получить ответ
            var response = operation.Current as Texture2D;

            yield return response;
        }
    }
}