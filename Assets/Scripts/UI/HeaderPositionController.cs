using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    [ExecuteAlways]
    public class HeaderPositionController : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            if(rectTransform == null)
            {
                return;
            }

            float targetYPos = -rectTransform.sizeDelta.y / 2;

            if (!Mathf.Approximately(rectTransform.anchoredPosition.y, targetYPos))
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetYPos);
            }
        } 
    }
}