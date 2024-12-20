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

        [Header("Del Btn")]
        public DelBtnPtrEvents delBtnPtrEvents;
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
        [Header(" Error Txt")]
        public TextMeshProUGUI errorTxt; 
        [Header("Game - Profile Details")]
        public GameModel gameModel;
        [SerializeField] SetProfileView setProfileView;
        private void OnEnable()
        {
            inputField.onEndEdit.AddListener(OnEndEdit);
            errorTxt.text = "The Profile name lready in use"; 
        }
        private void OnDisable()
        {
            inputField.onEndEdit.RemoveAllListeners();
        }

        public void ToggleErrorTxt(bool setActive)
        {
            errorTxt.gameObject.SetActive(setActive); 
        }

        void OnEndEdit(string str)
        {
            profileTxt= str;
            DisplayProfileTxt();
            inputField.gameObject.SetActive(false);
            profileName.gameObject.SetActive(true);            
            setProfileView.OnNewProfileSelected();
        }
        public void Init(GameModel gameModel, SetProfileView setProfileView) // each profile mapped to one game 
        {
            this.gameModel = gameModel;
            this.setProfileView = setProfileView;
            ToggleErrorTxt(false);
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
                AbbasImg.gameObject.SetActive(true);
                LODImg.gameObject.SetActive(true);
                AbbasClasstypeData abbasClasstypeData 
                    = CharService.Instance.allCharSO.GetAbbasClasstypeData(gameModel.abbasClassType);
                AbbasImg.sprite = abbasClasstypeData.spriteN;                

                Sprite spriteLOD =
                    GameService.Instance.gameController.allGameDiffSO.GetGameDiffSO(gameModel.gameDifficulty).spriteN;
                LODImg.sprite = spriteLOD;

           
            }
            
        }
        void DisplayProfileTxt()
        {          
            if(inputField.text != string.Empty)               
            
                profileName.text = inputField.text;  

        }
        public void SetClickedState()
        {
            isPlankClicked = true;
            if(gameModel == null)
            {
                inputField.gameObject.SetActive(true);
                inputField.ActivateInputField();
                profileName.gameObject.SetActive(false);
            }
            else
            {
                // hl the cross and show notify box 
                delBtnPtrEvents.gameObject.SetActive(true);
                delBtnPtrEvents.Init(gameModel, this, setProfileView); 
            }
            bgImg.sprite = spriteClicked;      
        }

        public void OnProfileClear()
        {
            GameService.Instance.DelAGameProfile(gameModel);         
            gameModel = null;
            Init(gameModel, setProfileView);          
            delBtnPtrEvents.Init(gameModel, this, setProfileView); // remove del btn            
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
            else
                SetClickedState();
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

