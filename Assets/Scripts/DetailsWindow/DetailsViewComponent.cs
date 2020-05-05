using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    public class DetailsViewComponent : MonoBehaviour
    {
        public Text CommentTxt;
        public Image CommentBGImg;
        public Image EventsBGImg;
        public Text SanTxt;
        public Image SanUnderlineImg;
        public Text ChinTxt;
        public Text NameTxt;
        public Button BackBtn;
        public Button EventsBtn;
        public Text EventsTxt;
        public Transform PhotosContainer;
        public SimpleScrollSnap PhotosScroll;
        public Button CommentBtn;
        public LayoutGroup InfoLayout;
        public LayoutGroup ContentLayout;
        public LayoutGroup AttributesLayout;
    }
}