using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    /// <summary> Управляет основными механиками окна детализации </summary>
    public class DetailsWindowSystem : MonoBehaviour
    {
        private DetailsWindow detailsWindow;
        private ManagerWindows managerWindows;

        private void Awake()
        {
            detailsWindow = FindObjectOfType<DetailsWindow>();
            managerWindows = FindObjectOfType<ManagerWindows>();
        }

        private void Start()
        {
            // Подписаться на кнопку назад
            detailsWindow.View.BackBtn.onClick.AddListener(BackBtnClickAction);

            // Подписаться на кнопку комментария
            detailsWindow.View.CommentBtn.onClick.AddListener(CommentBtnClickAction);

            // Подписаться на кнопку событий
            detailsWindow.View.EventsBtn.onClick.AddListener(EventsBtnClickAction);
        }

        private void BackBtnClickAction()
        {
            // Открыть окно поиска
            managerWindows.OpenWindow(Windows.Search);
        }

        private void CommentBtnClickAction()
        {
            // Включить/выключить комментарий
            detailsWindow.View.CommentBGImg.gameObject.SetActive(!detailsWindow.View.CommentBGImg.gameObject.activeSelf);
        }

        private void EventsBtnClickAction()
        {
            // Включить/выключить события
            detailsWindow.View.EventsBGImg.gameObject.SetActive(!detailsWindow.View.EventsBGImg.gameObject.activeSelf);
        }
    }
}
