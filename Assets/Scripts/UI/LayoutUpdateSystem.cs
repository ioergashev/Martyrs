using PSTGU.ServerCommunication;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;

namespace PSTGU
{
    /// <summary> Принудительно обновляет разметки </summary>
    public class LayoutUpdateSystem : MonoBehaviour
    {
        private DetailsWindow detailsWindow;
        private DetailsSettingsRuntime detailsSettingsRuntime;

        private void Awake()
        {
            detailsWindow = FindObjectOfType<DetailsWindow>();
            detailsSettingsRuntime = FindObjectOfType<DetailsSettingsRuntime>();
        }

        private void Start()
        {
            detailsSettingsRuntime.OnContentSet.AddListener(DetailsContentSetAction);
            detailsSettingsRuntime.OnPhotosSet.AddListener(DetailsPhotosSetAction);
        }

        private void DetailsContentSetAction()
        {
            UpdateDetailsLayout();
        }

        private void DetailsPhotosSetAction()
        {
            UpdateDetailsLayout();
        }

        private void UpdateDetailsLayout()
        {           
            LayoutRebuilder.ForceRebuildLayoutImmediate(detailsWindow.View.InfoLayout.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(detailsWindow.View.ContentLayout.GetComponent<RectTransform>());
            Canvas.ForceUpdateCanvases();
        }
    }
}
