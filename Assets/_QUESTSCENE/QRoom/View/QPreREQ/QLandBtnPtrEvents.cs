using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Quest
{
    public class QLandBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] Image img;
        LandscapeNames landName;
        QModeNLandView qModeNLandView;

        LandscapeSO landSO;
        public void InitLandBtn(QModeNLandView qModeNLandView)
        {
            this.qModeNLandView = qModeNLandView;
           landName
                = LandscapeService.Instance.currLandscape;
            landSO = LandscapeService.Instance.allLandSO.GetLandSO(landName);

            img.sprite = landSO.iconSpriteDay;
           

        }


        public void OnPointerClick(PointerEventData eventData)
        {
            //GameObject invPanelXL =
            //                InvService.Instance.invXLPanel;

            //UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(invPanelXL, true);
            //invPanelXL.GetComponent<IPanel>().Init();
        }
        


        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTxt();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Hidetxt();
        }

        void ShowTxt()
        {
            string str = landName.ToString();
            heading.alignment = TextAlignmentOptions.Left;
            heading.text = str;

            qModeNLandView.ShowLandDisplay(); 
        }
        void Hidetxt()
        {
            heading.text = "";
            qModeNLandView.HideLandDisplay();
        }

        void Start()
        {
            heading = transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
            heading.text = "";
            img = transform.GetComponent<Image>();
        }
    }
}