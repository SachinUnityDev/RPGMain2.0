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
        public  string profileTxt = string.Empty;

        [Header("Game - Profile Details")]
        [SerializeField] GameModel gameModel;
        [SerializeField] SetProfileView setProfileView;
        private void OnEnable()
        {
            inputField.onEndEdit.AddListener(OnEndEdit);
        }
        private void OnDisable()
        {
            inputField.onEndEdit.RemoveAllListeners();
        }

        void OnEndEdit(string str)
        {
            profileTxt= str;
            DisplayProfileTxt();
            inputField.gameObject.SetActive(false);
            profileName.gameObject.SetActive(true);           
        }
        public void Init(GameModel gameModel, SetProfileView setProfileView) // each profile mapped to one game 
        {
            this.gameModel = gameModel;
            this.setProfileView = setProfileView;
            FillPlankDetails();
        }

        void FillPlankDetails()
        {
            if (gameModel == null)
            {                
                AbbasImg.gameObject.SetActive(false);
                LODImg.gameObject.SetActive(false);
                profileName.text = "Empty Slot";
               // inputField.text = "_";
            }
            else
            {
                profileTxt = gameModel.GetProfileName();
                profileName.text = profileTxt;

                Sprite spriteAbbas =
                    CharService.Instance.allCharSO.GetAbbasClasstypeData(gameModel.abbasClassType).spriteN;
                AbbasImg.sprite = spriteAbbas;

                Sprite spriteLOD =
                    GameService.Instance.gameController.allGameDiffSO.GetGameDiffSO(gameModel.gameDifficulty).spriteN;
                LODImg.sprite = spriteLOD;

                AbbasImg.gameObject.SetActive(true);
                LODImg.gameObject.SetActive(true);
            }
            AbbasImg.sprite = spriteN; 
        }
        void DisplayProfileTxt()
        {          
            if(inputField.text != string.Empty)               
            
                profileName.text = inputField.text;  

        }
        public void SetClickedState()
        {
            isPlankClicked = true;
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
            profileName.gameObject.SetActive(false);
            bgImg.sprite = spriteClicked;      
        }
        public void SetUnclickedState()
        {
            isPlankClicked = false;
            bgImg.sprite = spriteN;     
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isPlankClicked)
                setProfileView.SetClickSlot(transform.GetSiblingIndex()); 
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

