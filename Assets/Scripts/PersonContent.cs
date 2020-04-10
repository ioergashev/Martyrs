﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    [Serializable]
    public class PersonContent
    {
        public PersonData data;
        public string docId;
        public string key;
        public string url;
        public string collection;

        [Serializable]
        public class PersonData
        {
            public int Номер;
            public string ФИО;
            public string Фамилия;
            public string Имя;
            public string Отчество;
            public string Пол;

            public string Сан_ЦеркСлужение;

            public Kanonization Канонизация;

            public string Комментарий;

            public Birth Рождение;
            public Death Кончина;

            public List<PhotoItem> Фотографии;
        }

        [Serializable]
        public class Kanonization
        {
            public string Ссылка;
            public string Чин_святости;
            public int Год;
        }

        [Serializable]
        public class Birth
        {
            public int День;
            public int Месяц;
            public int Год;
            public string Неточная_дата;
            public string Место;
        }

        [Serializable]
        public class Death
        {
            public int День;
            public int Месяц;
            public int Год;
            public string Неточная_дата;
            public string Место;
            public string Текст;
        }
    }

    [Serializable]
    public class PhotoItem
    {
        public string Id;
        public bool ExistInCash;
        public int NUM;
        public Photo Фото_сжатое;
        public string Подпись;
        public string FileURL;
        public Texture2D Texture;
    }

    [Serializable]
    public class Photo
    {
        public string fileName;
        public string fileContentUrl;
    }
}