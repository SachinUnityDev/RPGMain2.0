using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Intro;
using Combat;
using DG.Tweening;
using System;

namespace Common
{
  


    public class RosterViewController : MonoBehaviour, IPanel
    {
        [Header("TO BE REF")]
        [SerializeField] Button closeBtn; 
        [SerializeField] GameObject charScrollGO;
        public GameObject CharPortraitGO;

        [Header("TO BE REF IN SIDE PANEL")]
        [SerializeField] Transform SidePlankTrans;
        [SerializeField] Button inviteBtn; 
        [SerializeField] TextMeshProUGUI descOnHover; 

        [Header("Not to be ref")]
        [SerializeField] Transform nameContainer; 
        [SerializeField] TextMeshProUGUI scrollName;
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
        [SerializeField] float prevLeftClick =0f;
        [SerializeField] float prevRightClick = 0f;

        [SerializeField] int index;
        [SerializeField] List<CharModel> unLockedChars = new List<CharModel>();
        [SerializeField] Sprite BGUnClicked;
        [SerializeField] Sprite BGClicked;

        [Header("Globals")]
      
        CharacterSO charSO;
        CharComplimentarySO charCompSO; 


        void OnEnable()
        {
            closeBtn.onClick.AddListener(OnCloseBtnPressed);
            nameContainer = charScrollGO.transform.GetChild(1);
            scrollName = nameContainer.GetChild(2).GetComponent<TextMeshProUGUI>();

            leftBtn = nameContainer.GetChild(0).GetComponent<Button>();
            rightBtn = nameContainer.GetChild(1).GetComponent<Button>();
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);

            inviteBtn.onClick.AddListener(OnInviteBtnPressed); 
        }

        public void ReverseBack(PortraitDragNDrop portraitDragNDrop)
        {
            Transform slotParent = portraitDragNDrop.parentTransform;
            RectTransform draggedGORect = portraitDragNDrop.GetComponent<RectTransform>();

            portraitDragNDrop.transform.SetParent(slotParent);

            draggedGORect.localScale = Vector3.one;
            draggedGORect.anchoredPosition = new Vector3(0, 0, 0);           
        }

        void OnDrag2PartySuccessful()
        {

        }
        void OnInviteBtnPressed()
        {

        }

        void OnCloseBtnPressed()
        {
            UnLoad();
        }
        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return; 
            if (index <= 0)
            {
                index = 0;
            }
            else
            {
                --index; PopulateCharScroll();
            }
            prevLeftClick = Time.time; 
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if (index >= unLockedChars.Count-1)
            {
              //  index = 0;
                index = unLockedChars.Count-1;
            }
            else
            {
                ++index; PopulateCharScroll();
            }
            prevRightClick = Time.time;
        }

        void PopulateCharScroll()
        {
           RosterService.Instance.On_ScrollSelectCharModel(unLockedChars[index]);
            if (RosterService.Instance.scrollSelectCharModel.charName == CharNames.Abbas_Skirmisher)
            {
                index++;
                RosterService.Instance.scrollSelectCharModel = unLockedChars[index];
            }
            Debug.Log("RosterService" + RosterService.Instance.scrollSelectCharModel.charName);
            PopulatePortrait();
            PopulateSidePlank();
        }

        public int GetIndexOfChar(CharNames charName)
        {
            int i = unLockedChars.FindIndex(t => t.charName == charName); 
            if(i!= -1)
                return i;
            else 
                Debug.Log("CharIndex not found!");
            return 0;
        }
        public void PopulatePortrait2_Char(CharNames charName)
        {
            index = 
            unLockedChars.FindIndex(t => t.charName == charName);                
            PopulateCharScroll();
            PopulateSidePlank();
        }
        // change index here 
        public void PopulatePortrait()
        {
            charScrollGO.transform.GetChild(0)
                .GetComponent<CharScrollSlotController>().PopulatePortrait();           
        }
        void PopulateSidePlank()
        {
            TextMeshProUGUI availabilityTxt = SidePlankTrans.GetChild(0)
                        .GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
            CharModel charModel = RosterService.Instance.scrollSelectCharModel;
            if (charModel.availOfChar == AvailOfChar.Available)
                availabilityTxt.text = "Available";
            else if (charModel.availOfChar == AvailOfChar.UnAvailable_InParty)
                availabilityTxt.text = "Already In Party";
            else
                availabilityTxt.text = "UnAvailable";           
            
            // Fame Behavior
            //FameBehavior fameBehavior = RosterService.Instance.currCharModel.fameBehavior;
            //SidePlankTrans.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
            //       = fameBehavior.ToString().CreateSpace();

            SidePlankTrans.GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                 = RosterService.Instance.scrollSelectCharModel.currCharLoc.ToString();
        }

        public void Load()
        {
            foreach (CharModel charModel in CharService.Instance.allyUnLockedCharModels)
            {
                CharService.Instance.GetCharCtrlWithName(charModel.charName); 
            }
           
            index = 0;
            gameObject.SetActive(true);
            unLockedChars = CharService.Instance.allyUnLockedCharModels; 
            PopulateCharScroll();
        }

        public void UnLoad()
        {
            gameObject.SetActive(false);
           
        }

        public void Init()
        {

            Load();          
        }
    }



}

