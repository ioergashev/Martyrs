using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class DetailsWindowSystem : MonoBehaviour
    {
        private void Start()
        {
            DetailsWindow.View.BackBtn.onClick.AddListener(BackBtnClickAction);
        }

        private void BackBtnClickAction()
        {
            ManagerWindows.OpenWindow(Windows.Search);
        }
    }
}
