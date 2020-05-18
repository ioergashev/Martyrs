using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSTGU
{
    [ExecuteAlways]
    public class BodyPositionController : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform HeaderRectTransform;
        public float spaceFromHeader = -10f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            if (rectTransform == null || HeaderRectTransform == null)
            {
                return;
            }

            float targetOffsetMax = - (HeaderRectTransform.sizeDelta.y + spaceFromHeader);

            if (!Mathf.Approximately(rectTransform.offsetMax.y, targetOffsetMax))
            {
                rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, targetOffsetMax);
            }
        }
    }
}