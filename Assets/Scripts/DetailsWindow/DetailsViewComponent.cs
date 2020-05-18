using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    public class DetailsViewComponent : MonoBehaviour
    {
        [Header("Header")]
        public Text TittleTxt;
        public LayoutGroup InfoLayout;

        [Header("Body")]
        public ScrollRect ContentScrollRect;

        public Button CommentBtn;
        public Text CommentTxt;
        public Image CommentBGImg;

        public Button EventsBtn;
        public Image EventsBGImg;
        public Text EventsTxt;

        public Button BibliographyBtn;
        public Image BibliographyBGImg;
        public Text BibliographyTxt;

        public Transform PhotosContainer;
        public SimpleScrollSnap PhotosScroll;

        public LayoutGroup ContentLayout;

        [Header("Bottom")]
        public Button BackBtn;     
    }
}