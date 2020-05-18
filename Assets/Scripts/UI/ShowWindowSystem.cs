using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    /// <summary> Управляет открытием окна </summary>
    public class ShowWindowSystem : MonoBehaviour
    {
        public class Archetype
        {
            public GameObject GameObject;
            public EnableWindowComponent EnableComponent;
            public RectTransform RectTransform;

            public void SetActive(bool value)
            {
                GameObject.SetActive(value);
                EnableComponent.Enable = value;
                RectTransform.anchorMin = Vector2.zero;
                RectTransform.anchorMax = Vector2.one;
            }
        }

        [HideInInspector]
        public List<Archetype> entities = new List<Archetype>();

        [HideInInspector]   
        public TransitionScreen transitionScreen;

        [HideInInspector]
        public WindowsSettingsRuntime windowsSettingsRuntime;

        private void Awake()
        {
            transitionScreen = FindObjectOfType<TransitionScreen>();
            windowsSettingsRuntime = FindObjectOfType<WindowsSettingsRuntime>();

            var instances = FindObjectsOfType<EnableWindowComponent>()
                .Where(c => c.GetComponent<RectTransform>() != null)
                .ToList().Select(c => c.gameObject);

            foreach(var instance in instances)
            {
                entities.Add(new Archetype 
                {
                    GameObject = instance, 
                    EnableComponent = instance.GetComponent<EnableWindowComponent>(), 
                    RectTransform = instance.GetComponent<RectTransform>()
                });
            }

            foreach (var entity in entities)
            {
                entity.EnableComponent.ShowRequest.AddListener(() => ShowWindowRequestAction(entity));
                entity.EnableComponent.HideRequest.AddListener(() => HideWindowRequestAction(entity));
            }
        }

        private void Start()
        {
            transitionScreen.gameObject.SetActive(false);
        }

        private void ShowWindowRequestAction(Archetype window)
        {
            SetWindowActive(window, true);
        }

        private void HideWindowRequestAction(Archetype window)
        {
            SetWindowActive(window, false);
        }

        private void SetWindowActive(Archetype window, bool value)
        {
            if (value == true && window.EnableComponent.Enable
                || value == false && !window.EnableComponent.Enable)
            {
                return;
            }

            var transitionType = window.EnableComponent.TransitionType;

            if (transitionType == WindowTransitionType.Fade
                && transitionScreen.View.FadeImg.color.a == 0)
            {
                transitionScreen.gameObject.SetActive(true);

                transitionScreen.View.FadeImg.DOFade(1, windowsSettingsRuntime.WidowsTransitionDuration / 2)
                       .OnComplete(() => FadeToFullCompliteAction(window, value));
            }
            else
            {
                window.SetActive(value);
            }
        }

        private void FadeToFullCompliteAction(Archetype window, bool value)
        {
            window.SetActive(value);

            transitionScreen.View.FadeImg.DOFade(0, windowsSettingsRuntime.WidowsTransitionDuration / 2)
                     .OnComplete(FadeToEmptyCompliteAction);
        }

        private void FadeToEmptyCompliteAction()
        {
            transitionScreen.gameObject.SetActive(false);
        }
    }
}