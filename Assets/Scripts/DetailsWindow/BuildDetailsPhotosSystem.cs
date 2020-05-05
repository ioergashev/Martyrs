//using PSTGU.ServerCommunication;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.Events;
//using System.Linq;
//using UnityEngine.UI;
//using DanielLochner.Assets.SimpleScrollSnap;

//namespace PSTGU
//{
//    /// <summary> Устанавливает загруженные фотографии в окно детализации </summary>
//    public class BuildDetailsPhotosSystem : MonoBehaviour
//    {
//        private DetailsWindow _detailsWindow;
//        private DetailsSettingsRuntime _detailsSettingsRuntime;

//        private void Awake()
//        {
//            _detailsWindow = FindObjectOfType<DetailsWindow>();
//            _detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
//        }

//        private void Start()
//        {
//            // Ожидать завершения загрузки всех фотографий
//            _detailsSettingsRuntime.OnPhotosLoaded.AddListener(PhotosLoadedAction);
//        }

//        private void PhotosLoadedAction()
//        {
//            SetPhotos();
//        }

//        private void SetPhotos()
//        {
//            //if (ScrollSnap.Panels != null)
//            //{
//            //    // Удалить старые фотографии
//            //    for (int i = ScrollSnap.Panels.Length - 1; i >= 0; i--)
//            //    {
//            //        ScrollSnap.Remove(i);
//            //    }
//            //}

//            //// Очистить старый список
//            //_detailsSettingsRuntime.ScrollPhotos.Clear();

//            // Получить список фотографий
//            var photos = _detailsSettingsRuntime.Content.data.Фотографии.Select(p => p.Texture).Where(t => t != null);

//            // Установить фотографии
//            foreach(var photo in photos)
//            {
//                // Добавить префаб в Scroll Snap
//                ScrollSnap.AddToBack(DetailsSettings.Instance.PhotoPrefab);

//                // Получить панель из Scroll Snap
//                var instance = ScrollSnap.Panels.Last().GetComponent<ScrollPhotosItem>();

//                // Установить фотографию
//                instance.View.PhotoImg.texture = photo;

//                // Настроить соотношение сторон
//                instance.View.PhotoAspectRatio.aspectRatio = (float)photo.width / photo.height;

//                // Добавить фото в список фотографий
//                _detailsSettingsRuntime.ScrollPhotos.Add(instance);
//            }

//            // Сообщить, что фотографии установлены
//            _detailsSettingsRuntime.OnPhotosSet?.Invoke();
//        }

//        private SimpleScrollSnap ScrollSnap
//        {
//            get
//            {
//                var value = _detailsWindow.View.PhotosScroll;

//                return value;
//            }
//        }
//    }
//}
