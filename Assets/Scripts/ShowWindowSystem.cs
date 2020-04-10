using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSTGU
{
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
                entity.EnableComponent.ShowRequest.AddListener(() => SetWindowActive(entity, true));
                entity.EnableComponent.HideRequest.AddListener(() => SetWindowActive(entity, false));
            }
        }

        private void SetWindowActive(Archetype entity, bool value)
        {
            entity.GameObject.SetActive(value);
            entity.EnableComponent.Enable = value;
            entity.RectTransform.anchorMin = Vector2.zero;
            entity.RectTransform.anchorMax = Vector2.one;
        }
    }
}