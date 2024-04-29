using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Intro
{
    public class SetProfilePtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool isPlankClicked = false;
        [SerializeField] TMP_InputField inputField;

        [Header(" Profile Name")]
        [SerializeField] TextMeshProUGUI profileName;


        [Header(" Abbas Sprite N  LOD IMG  TBR")]
        [SerializeField] Image AbbasImg;
        [SerializeField] Image LODImg;

        [Header(" BG Sprite")]
        [SerializeField] Image bgImg;
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteOnHover;
        [SerializeField] Sprite spriteClicked;

        [Header(" Profile txt")]
        [SerializeField] string ClickedStateTxt = "_";
        [SerializeField] string dsplyTxt;

        [Header("Game - Profile Details")]
        [SerializeField] GameModel gameModel;
        [SerializeField] SetProfileView setProfileView; 
 
        public void Init(GameModel gameModel, SetProfileView setProfileView) // each profile mapped to one game 
        {
            this.gameModel = gameModel;
            this.setProfileView = setProfileView;
            FillPlankDetails();
        }

        void FillPlankDetails()
        {
            if(gameModel == null)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            dsplyTxt= gameModel.GetProfileName();
            profileName.text = dsplyTxt; 

            Sprite spriteAbbas =
                CharService.Instance.allCharSO.GetAbbasClasstypeData(gameModel.abbasClassType).spriteN;
            AbbasImg.sprite = spriteAbbas;

            Sprite spriteLOD =
                GameService.Instance.gameController.allGameDiffSO.GetGameDiffSO(gameModel.gameDifficulty).spriteN; 
            LODImg.sprite = spriteLOD;
            SetUnclickedState(); 
        }


        void DisplayAssignmentTxt()
        {
            inputField.text = ClickedStateTxt;            
        }

        void DisplayProfileTxt()
        {
            inputField.text = dsplyTxt;
            gameModel.SetProfileName(inputField.text);
            profileName.text = dsplyTxt;
        }
        public void SetClickedState()
        {
            isPlankClicked = true;
            bgImg.sprite = spriteClicked; 
            DisplayAssignmentTxt();
            inputField.gameObject.SetActive(true);
            profileName.gameObject.SetActive(false);
        }
        public void SetUnclickedState()
        {
            isPlankClicked = false;
            bgImg.sprite = spriteN;
            DisplayProfileTxt();
            inputField.gameObject.SetActive(false);
            profileName.gameObject.SetActive(true);
        }

        public void OnGUI() // Assignment
        {
            if (Event.current.isKey && Event.current.type == EventType.KeyDown &&
                Event.current.keyCode == KeyCode.KeypadEnter && isPlankClicked)
            {
                {
                    dsplyTxt = inputField.text; 
                    SetUnclickedState();
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(isPlankClicked) 
                SetClickedState(); 
            else
                SetUnclickedState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isPlankClicked)
                bgImg.sprite = spriteOnHover; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isPlankClicked)
                bgImg.sprite = spriteN;
        }
    }
}