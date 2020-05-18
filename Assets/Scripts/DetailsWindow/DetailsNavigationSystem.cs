using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    /// <summary> Управляет кнопками навигации в окне детализации </summary>
    public class DetailsNavigationSystem : MonoBehaviour
    {
        private DetailsWindow detailsWindow;
        private DetailsSettingsRuntime detailsSettingsRuntime;

        private void Awake()
        {
            detailsWindow = FindObjectOfType<DetailsWindow>();
            detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
        }

        private void Update()
        {
            if (detailsWindow.View.UpBtn.IsHeld)
            {
                detailsWindow.View.ContentScrollRect.velocity = Vector2.down * detailsSettingsRuntime.ScrollBtnSpeed;
            }
            else if (detailsWindow.View.DownBtn.IsHeld)
            {
                detailsWindow.View.ContentScrollRect.velocity = Vector2.up * detailsSettingsRuntime.ScrollBtnSpeed;
            }
        }
    }
}