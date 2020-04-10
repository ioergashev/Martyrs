using UnityEngine;
using UnityEngine.UI;

namespace PSTGU
{
    public class DetailsViewComponent : MonoBehaviour
    {
        public Text CommentTxt;
        public Text SanTxt;
        public Text ChinTxt;
        public Text NameTxt;
        public Text PhotoSignTxt;
        public Button BackBtn;
        public Button EventsBtn;
        public Button BibliographyBtn;
        public Button PrevPhotoBtn;
        public Button NextPhotoBtn;  
        public RawImage PhotoImg;
        public AspectRatioFitter PhotoAspectRatio;
        public Image DefaultImg;
        public Image LoadingImg;
    }
}