using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Common
{


    public class ProfileBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header(" Sprites")]
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteClick;

        [Header("Profile Slot references")]
        [SerializeField] ProfileSlot profileSlot;

        [Header("References")]
        [SerializeField] Image bgImg; 
        [SerializeField] Image abbasImg;
        [SerializeField] TextMeshProUGUI calDate;
        [SerializeField] TextMeshProUGUI profileTxt;
        [SerializeField] Image lodImg;

        [Header("ref")]
        LoadView loadView; 
        ProfilePgView profilePgView;
        List<GameModel> allGameModelsInProfile = new List<GameModel>();  
        public void Init( ProfilePgView profilePgView, LoadView loadView)
        {
            this.loadView = loadView; 
            this.profilePgView = profilePgView;
            foreach (GameModel gameModel in GameService.Instance.allGameModel)
            {
                if ((gameModel.profileSlot == profileSlot))
                {
                    allGameModelsInProfile.Add(gameModel);
                }
            }
            GameModel gameModel_1 = allGameModelsInProfile[0];    
            profileTxt.text = gameModel_1.GetProfileName();
            Sprite spriteAbbas =
                CharService.Instance.allCharSO.GetAbbasClasstypeData(gameModel_1.abbasClassType).spriteN;
            abbasImg.sprite = spriteAbbas;

            Sprite spriteLOD =
                GameService.Instance.gameController.allGameDiffSO.GetGameDiffSO(gameModel_1.gameDifficulty).spriteN;
            lodImg.sprite = spriteLOD;

            abbasImg.gameObject.SetActive(true);
            lodImg.gameObject.SetActive(true);
            MonthSO monthSO = SaveService.Instance.allMonthSO.GetMonthSO(gameModel_1.calDate.monthName);
            string monthNameShortHand = monthSO.monthNameShort;
            calDate.text =  gameModel_1.calDate.day + "  " + monthNameShortHand;
            bgImg.sprite = spriteN; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bgImg.sprite = spriteClick;
            loadView.OnProfileBtnClicked(allGameModelsInProfile);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            bgImg.sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            bgImg.sprite = spriteN; 
        }
    }
}