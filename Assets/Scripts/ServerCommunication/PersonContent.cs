using System;
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

            public List<Event> События;

            public List<Source> Библиография;
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

        [Serializable]
        public class PhotoItem
        {
            public int NUM;
            public Photo Фото_сжатое;
            public string Подпись;
            public Texture2D Texture;
        }

        [Serializable]
        public class Photo
        {
            public string fileName;
            public string fileContentUrl;
        }

        [Serializable]
        public class Event
        {
            public string Датировка;
            public string Текст;
        }

        [Serializable]
        public class Source
        {
            public int NUM;
            public string Название;
            public string Тип;
        }
    }
}