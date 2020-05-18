using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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
            public CanvasGroup CanvasGroup;

            public Archetype(GameObject instance)
            {
                GameObject = instance;
                EnableComponent = instance.GetComponent<EnableWindowComponent>();
                RectTransform = instance.GetComponent<RectTransform>();
                CanvasGroup = instance.GetComponent<CanvasGroup>();

                Init();
            }

            private void Init()
            {
                SetActive(false);
            }

            public void SetActive(bool value)
            {
                EnableComponent.Enable = value;
                RectTransform.anchorMin = value == true? Vector2.zero: 3 * Vector2.one;
                RectTransform.anchorMax = value == true ? Vector2.one : 4 * Vector2.one;
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
                entities.Add(new Archetype(instance));
            }

            foreach (var entity in entities)
            {
                entity.EnableComponent.ShowRequest.AddListener(() => ShowWindowRequestAction(entity));
                entity.EnableComponent.HideRequest.AddListener(() => HideWindowRequestAction(entity));
            }
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

            switch(window.EnableComponent.TransitionType)
            {
                case WindowTransitionType.Fade:

                    window.SetActive(true);

                    float startAlpha = value == true ? 0 : 1;
                    window.CanvasGroup.alpha = startAlpha;

                    float targetAlpha = value == true ? 1 : 0;
                    window.CanvasGroup.DOFade(targetAlpha, windowsSettingsRuntime.FadeTransitionDuration / 2)
                     .OnComplete(() => FadeCompleteAction(window, value));

                    break;

                case WindowTransitionType.None:
                default:
                    window.SetActive(value);
                    break;
            }
        }

        private void FadeCompleteAction(Archetype window, bool value)
        {
            void action()
            {
                window.SetActive(value);
            }

            if (value == false)
            {
                Invoke(action, windowsSettingsRuntime.TransitionDelay);
            }
            else
            {
                action();
            }
          
        }

        private void Invoke(UnityAction callback, float delay)
        {
            StartCoroutine(DelayedInvokeIEnumerator(callback, delay));
        }

        private IEnumerator DelayedInvokeIEnumerator(UnityAction callback, float delay)
        {
            yield return new WaitForSeconds(delay);

            callback?.Invoke();
        }
    }
}