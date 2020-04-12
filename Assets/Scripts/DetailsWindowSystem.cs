using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
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
            detailsWindow.View.BackBtn.onClick.AddListener(BackBtnClickAction);
        }

        private void BackBtnClickAction()
        {
            managerWindows.OpenWindow(Windows.Search);
        }
    }
}
