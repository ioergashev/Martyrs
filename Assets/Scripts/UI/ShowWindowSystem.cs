using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSTGU
{
    /// <summary> Управляет открытием окна </summary>
    public class ShowWindowSystem : MonoBehaviour
    {
        public struct Archetype
        {
            public GameObject GameObject;
            public EnableWindowComponent EnableComponent;
            public RectTransform RectTransform;
        }

        [HideInInspector]
        public List<Archetype> entities = new List<Archetype>();

        private void Awake()
        {
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

        private void ShowWindowRequestAction(Archetype window)
        {
            // Если окно не активно
            if(!window.EnableComponent.Enable)
            {
                SetWindowActive(window, true);
            }
        }

        private void HideWindowRequestAction(Archetype window)
        {
            // Если окно активно
            if (window.EnableComponent.Enable)
            {
                SetWindowActive(window, false);
            }
        }

        private void SetWindowActive(Archetype window, bool value)
        {
            window.GameObject.SetActive(value);
            window.EnableComponent.Enable = value;
            window.RectTransform.anchorMin = Vector2.zero;
            window.RectTransform.anchorMax = Vector2.one;
        }
    }
}