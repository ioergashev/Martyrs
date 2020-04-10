using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace PSTGU.ServerCommunication
{
    /// <summary> Фасад для взаимодействия с сервером </summary>
    public static class ManagerServer
    {
        public static IEnumerator Search(string query, int skip = 0, int take = 100)
        {
            var request = new SimpleSearchRequest(ServerSettings.ServerURL, query, skip, take);

            yield return SendRequestWithTimer(request);

            var response = ParseResponse<SearchResponse>(request);

            // Сохранить данные
            CashData(response);

            yield return response;
        }

        private static void CashData(SearchResponse response)
        {
            if (response == null || response.data == null)
            {
                return;
            }

            foreach (var item in response.data)
            {
                // Если элемент пустой, отсутсвует id или элемент уже имеется в базе
                if (item.data == null || string.IsNullOrEmpty(item.docId) || Data.Persons.ContainsKey(item.docId))
                {
                    continue;
                }
                else
                {
                    // Сохранить элемент
                    Data.Persons.Add(item.docId, item);
                }
            }
        }

        public static IEnumerator DownloadPhoto(string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                yield break;
            }

            // Сформировать url запроса
            url = ServerSettings.FileServerURL + url + ServerSettings.AuthTokenFile;

            // Сформировать запрос
            var webRequest = UnityWebRequestTexture.GetTexture(url);

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
                yield break;
            }

            var texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;

            // Сохранить данные
            //CashData(texture, url);

            yield return texture;
        }

        /// <summary> Кеширование изображения в памяти компьютера с созданием уникального имени на основании url </summary>
        private static void CashData(Texture2D texture, string url)
        {
            if (texture == null || string.IsNullOrEmpty(url))
            {
                return;
            }

            string path = ManagerIO.CreateFilePathByPhotoURL(url);

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            // Если файл уже имеется в кеше
            if (System.IO.File.Exists(path))
            {
                return;
            }

            // Сохранить изображение
            System.IO.File.WriteAllBytes(path, texture.EncodeToJPG());
        }

        /// <summary> Обработать ответ с сервера </summary>
        /// <typeparam name="T"> Тип ответа </typeparam>
        /// <param name="request"> Тип запроса </param>
        private static T ParseResponse<T>(RequestBase request) where T : SimpleResponse, new()
        {
            if (request == null || request.UnityWebRequest == null)
            {
                Debug.LogError("Can not parse response of type " + typeof(T).ToString().Split('.').Last() + ". Invalid request.");

                return null;
            }

            if (ServerSettings.LogResponseHeaders)
            {
                string headers = "";
                foreach (var t in request.UnityWebRequest.GetResponseHeaders())
                {
                    headers += string.Format("{0} : {1} \n", t.Key, t.Value);
                }
                Debug.Log("-----RESPONSE HEADERS " + request.GetType().ToString().Split('.').Last() + "\n" + headers);
            }
            
            if(request.UnityWebRequest.downloadHandler == null)
            {
                Debug.LogError("Can not parse response of type " + typeof(T).ToString().Split('.').Last() + ". Invalid request.");

                return null;
            }

            string json = request.UnityWebRequest.downloadHandler.text;

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogErrorFormat("{6} Response is null! error: {0}, code: {1}, isHttpError: {2}, isNetworkError: {3}, bytes: {4}, isDone: {5}",
                    request.UnityWebRequest.error,
                    request.UnityWebRequest.responseCode,
                    request.UnityWebRequest.isHttpError,
                    request.UnityWebRequest.isNetworkError,
                    request.UnityWebRequest.downloadedBytes,
                    request.UnityWebRequest.downloadHandler.isDone,
                    request.GetType().ToString().Split('.').Last()
                );
                return null;
            }

            // Unity JsonUtility не поддерживает десериализации массива из верхнего уровня.
            // Поэтому используется обходной путь
            if (json[0] == '[')
            {
                json = "{\"data\":" + json + "}";
            }

            if (ServerSettings.LogResponseBody)
            {
                string responseName = typeof(T).ToString().Split('.').Last();                
                string consoleLog = json.Replace(",", ",\n");

                Debug.Log(responseName + "   Response:\n" + consoleLog);

                string pathFolder = Application.persistentDataPath + "/jsons";
                string pathFile = pathFolder+ "/" + responseName;

                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }

                System.IO.File.WriteAllText(pathFile + "_RAW", consoleLog);

                string parsedResponse = "";

                // Попытаться извлечь объект-ответ из json 
                try
                {
                    parsedResponse = JsonUtility.ToJson(JsonUtility.FromJson<T>(json), true);

                    Debug.Log(responseName + "   Parsed Response:\n" + parsedResponse);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }

                System.IO.File.WriteAllText(pathFile, parsedResponse);
            }

            // Если ответ без ошибки
            if (request.UnityWebRequest.responseCode < 300 && request.UnityWebRequest.responseCode >= 200)
            {
                // Попытаться извлечь объект-ответ из json 
                try
                {
                    return JsonUtility.FromJson<T>(json);
                }
                catch (Exception e)
                {
                    return new T()
                    {
                        error = "Exception! " + e.Message + "\nJSON: " + json
                    };
                }
            }

            // Если ошибка 400
            if (request.UnityWebRequest.responseCode == 400)
            {
                // Попытаться извлечь объект-ответ из json 
                try
                {
                    var response = JsonUtility.FromJson<T>(json);
                    response.errorCode = request.UnityWebRequest.responseCode;
                    return response;
                }
                catch (Exception e)
                {
                    return new T()
                    {
                        error = "Exception! " + e.Message + "\nJSON: " + json
                    };
                }
            }

            // Если неизвестная ошибка
            return new T()
            {
                errorCode = request.UnityWebRequest.responseCode,
                error = request.UnityWebRequest.error
            };
        }

        /// <summary> Отправить запрос и дожидаться ответа в течение определенного времени </summary>
        private static IEnumerator SendRequestWithTimer(RequestBase request)
        {
            // Добавить токен аутентификации в запрос
            request.URL += ServerSettings.AuthToken;

            Debug.Log("Send Request:\n" + request.URL);

            // Отправить запрос
            var asyncOperation = request.Send();

            // Начать отсчет времени ожидания ответа
            float timer = ServerSettings.RequestTimeout;

            // Пока запрос не выполнен
            while (!asyncOperation.isDone)
            {
                // Ждать
                timer -= Time.deltaTime;
                yield return null;

                // Если время ожидания ответа истекло
                if (timer <= 0)
                {
                    // Прервать запрос
                    request.Abort();
                    break;
                }
            }
        }
    }
}