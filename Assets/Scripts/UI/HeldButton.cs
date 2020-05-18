using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PSTGU
{
    public class HeldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [HideInInspector]
        public bool IsHeld;

        [HideInInspector]
        public UnityEvent OnDown = new UnityEvent();

        [HideInInspector]
        public UnityEvent OnUp = new UnityEvent();

        public void OnPointerDown(PointerEventData eventData)
        {
            IsHeld = true;
            OnDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsHeld = false;
            OnUp?.Invoke();
        }
    }
}