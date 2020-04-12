using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PSTGU
{
    public class BackButtonSystem : MonoBehaviour
    {
        private DetailsWindow detailsWindow;
        private ManagerWindows managerWindows;
        private WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            detailsWindow = FindObjectOfType<DetailsWindow>();
            managerWindows = FindObjectOfType<ManagerWindows>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();
        }

        private void Start()
        {
            detailsWindow.View.BackBtn.onClick.AddListener(BackBtnClickAction);
        }

        private void BackBtnClickAction()
        {
            managerWindows.OpenWindow(Windows.Search);
        }

        private void Update()
        {
            if (!Input.GetKeyUp(KeyCode.Escape))
            {
                return;
            }

            if(windowsSettingsRuntime.CurrentWindow == Windows.Details)
            {
                managerWindows.OpenWindow(Windows.Search);
            }
        }
    }
}
