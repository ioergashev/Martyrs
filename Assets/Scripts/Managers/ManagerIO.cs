using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PSTGU.ServerCommunication
{
    /// <summary> Фасад для взаимодействия с I/O </summary>
    public static class ManagerIO
    {
        public static IEnumerator LoadImage(string url)
        {
            yield break;
        }

        public static string CreateFilePathByPhotoURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            string fileName = CreateFileNameByPhotoURL(url);

            if(string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            string path = Application.persistentDataPath + "/" + fileName;

            return path;
        }

        public static string CreateFileNameByPhotoURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var arr = url.Split('/');

            if (url.Contains("DocumentFiles") && arr.Last() == "Content")
            {
                string fileName = arr[arr.Length - 2];

                fileName = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(fileName));

                const int maxNameLength = 32;
                fileName = fileName.Substring(0, maxNameLength);

                return fileName;
            }

            return null;
        }
    }
}