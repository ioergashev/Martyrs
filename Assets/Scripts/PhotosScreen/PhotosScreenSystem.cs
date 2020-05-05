using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Управляет основными механиками окна фотографий </summary>
    public class PhotosScreenSystem : MonoBehaviour
    {
        private PhotosScreen photosScreen;
        private ManagerWindows managerWindows;

        private void Awake()
        {
            photosScreen = FindObjectOfType<PhotosScreen>();
            managerWindows = FindObjectOfType<ManagerWindows>();
        }

        private void Start()
        {
            // Подписаться на кнопку назад
            photosScreen.View.BackBtn.onClick.AddListener(BackBtnClickAction);
        }

        private void BackBtnClickAction()
        {
            // Закрыть окно фотографий
            managerWindows.SetScreenActive(Screens.Photos, false);
        }
    }
}
