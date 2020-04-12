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
    public class ManagerServer : MonoBehaviour
    {
        /// <summary> Успешный запрос возвращает объект типа <see cref="SearchResponse"/></summary>
        public IEnumerator Search(string query, int skip = 0, int take = 100)
        {
            // Сформировать запрос
            var request = new SimpleSearchRequest(ServerSettings.Instance.ServerURL, query, skip, take);

            // Выполнить запрос
            yield return SendRequestWithTimer(request);

            // Получить ответ
            var response = ParseResponse<SearchResponse>(request);

            // Сохранить количество найденных записей
            response.RecordsFoundCount = GetRecordsCount(request.UnityWebRequest);

            yield return response;
        }

        private int GetRecordsCount(UnityWebRequest request)
        {
            int recordsCount = -1;

            try
            {
                recordsCount = int.Parse(request.GetResponseHeader("X-Total-Count"));
            }
            catch (Exception e)
            {
                recordsCount = -1;
                Debug.LogException(e);
            }

            return recordsCount;
        }

        /// <summary> Успешный запрос возвращает объект типа <see cref="Texture2D"/></summary>
        public IEnumerator DownloadPhoto(string url)
        {
            // Обработка ошибок
            if (string.IsNullOrEmpty(url))
            {
                yield break;
            }

            // Сформировать url запроса
            url = ServerSettings.Instance.FileServerURL + url + ServerSettings.Instance.AuthTokenFile;

            // Сформировать запрос
            var webRequest = UnityWebRequestTexture.GetTexture(url);

            // Выполнить запрос
            yield return webRequest.SendWebRequest();

            // Обработка ошибок
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
                yield break;
            }

            // Извлечь изображение
            var texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;

            yield return texture;
        }

        /// <summary> Обработать ответ с сервера. Возвращает объект-ответ </summary>
        /// <typeparam name="T"> Тип ответа </typeparam>
        private T ParseResponse<T>(RequestBase request) where T : SimpleResponse, new()
        {
            // Обработка ошибок
            if (request == null || request.UnityWebRequest == null)
            {
                Debug.LogError("Can not parse response of type " + typeof(T).ToString().Split('.').Last() + ". Invalid request.");

                return null;
            }

            // Логгирование
            if (ServerSettings.Instance.LogResponseHeaders)
            {
                string headers = "";
                foreach (var t in request.UnityWebRequest.GetResponseHeaders())
                {
                    headers += string.Format("{0} : {1} \n", t.Key, t.Value);
                }
                Debug.Log("-----RESPONSE HEADERS " + request.GetType().ToString().Split('.').Last() + "\n" + headers);
            }

            // Обработка ошибок
            if (request.UnityWebRequest.downloadHandler == null)
            {
                Debug.LogError("Can not parse response of type " + typeof(T).ToString().Split('.').Last() + ". Invalid request.");

                return null;
            }

            string json = request.UnityWebRequest.downloadHandler.text;

            // Обработка ошибок
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

            // Логгирование на диск
            if (ServerSettings.Instance.LogResponseBody)
            {
                string responseName = typeof(T).ToString().Split('.').Last();                
                string consoleLog = json.Replace(",", ",\n");

                Debug.Log(responseName + "   Response:\n" + consoleLog);

                string pathFolder = Application.persistentDataPath + "/jsons";
                string pathFile = pathFolder+ "/" + System.DateTime.Now.ToFileTime() + "_" + responseName;

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
        private IEnumerator SendRequestWithTimer(RequestBase request)
        {
            // Добавить токен аутентификации в запрос
            request.URL += ServerSettings.Instance.AuthToken;

            Debug.Log("Send Request:\n" + request.URL);

            // Отправить запрос
            var asyncOperation = request.Send();

            // Начать отсчет времени ожидания ответа
            float timer = ServerSettings.Instance.RequestTimeout;

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