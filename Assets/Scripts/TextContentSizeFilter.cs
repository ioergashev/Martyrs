using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    [ExecuteAlways]
    public class TextContentSizeFilter : MonoBehaviour
    {
        private RectTransform thisRectTransform;

        private LayoutGroup layout;

        private Text text;

        private void Awake()
        {
            thisRectTransform = GetComponent<RectTransform>();
            layout = GetComponent<LayoutGroup>();
            text = GetComponentInChildren<Text>();

        }

        void Update()
        {
            // Растянуть текст до максимального размера по высоте
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.preferredHeight);

            // Поддерживать родительскую позицию такой же как позиция текста
            thisRectTransform.sizeDelta = text.rectTransform.sizeDelta + 
                new Vector2(layout.padding.left + layout.padding.right,
                layout.padding.top + layout.padding.bottom);
        }
    }
}