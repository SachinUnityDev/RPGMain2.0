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
    public class RosterViewController : MonoBehaviour, IPanel, iHelp
    {
        [SerializeField] HelpName helpName;
        [Header("TO BE REF")]
        [SerializeField] Button closeBtn; 
        [SerializeField] GameObject charScrollGO;
        public GameObject CharPortraitGO;
      

        [Header("Btm Char List")]
        [SerializeField] Transform btmCharTrans;
        [SerializeField] CharScrollSlotController charSlot; 

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
        [SerializeField] List<CharController> unLockedChars = new List<CharController>();
        [SerializeField] Sprite BGUnClicked;
        [SerializeField] Sprite BGClicked;

        [Header("Globals")]
      
        CharacterSO charSO;
        CharComplimentarySO charCompSO;

        private void Start()
        {
            CharService.Instance.OnPartyDisbanded += OnPartyDisbandedFameBehMisMatchChk;
            CharScrollSlotController charSlot = 
                    charScrollGO.transform.GetChild(0).GetComponent<CharScrollSlotController>(); 
        }
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
        void OnPartyDisbandedFameBehMisMatchChk()
        {
            // LOOP THRU TRANSFORMS IN btn char scroll 
            // all unlocked char 
            // load 
            // 
            for (int i = 1; i < btmCharTrans.childCount; i++)
            {
                PortraitDragNDrop port = btmCharTrans.GetChild(i).GetComponentInChildren<PortraitDragNDrop>(); 
               if (port != null)
                {
                    CharController charController = CharService.Instance.GetCharCtrlWithName(port.charDragged);
                    if (!FameService.Instance.fameController.IsFameBehaviorMatching(charController))
                    {
                        charController.charModel.availOfChar = AvailOfChar.UnAvailable_Fame;
                       // CharService.Instance.allAvailCompModels.Add(charController.charModel);
                        CharService.Instance.allCharsInPartyLocked.Remove(charController);
                        Destroy(port.gameObject);       
                        Load();
                    }
                }
            }
        }

        public void ReverseBack(PortraitDragNDrop portraitDragNDrop)
        {
            Transform slotParent = portraitDragNDrop.parentTransform;
            RectTransform draggedGORect = portraitDragNDrop.GetComponent<RectTransform>();

            portraitDragNDrop.transform.SetParent(slotParent);

            draggedGORect.localScale = Vector3.one;
            draggedGORect.anchoredPosition = new Vector3(0, 0, 0);
            PopulateCharScroll();
            Destroy(portraitDragNDrop.gameObject);
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
            if (index == 0)
            {
                index = unLockedChars.Count-1;
                PopulateCharScroll();
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
            if (index == unLockedChars.Count-1)
            {
                index = 0; 
                PopulateCharScroll();   
            }
            else
            {
                ++index; PopulateCharScroll();
            }
            prevRightClick = Time.time;
        }

        void PopulateCharScroll()
        {
           RosterService.Instance.On_ScrollSelectCharModel(unLockedChars[index].charModel);
            if (RosterService.Instance.scrollSelectCharModel.charName == CharNames.Abbas)
            {
                index++;
                RosterService.Instance.scrollSelectCharModel = unLockedChars[index].charModel;
            }
            Debug.Log("RosterService" + RosterService.Instance.scrollSelectCharModel.charName);
            PopulatePortrait();
            PopulateSidePlank();
        }
        public void PopulatePortrait2_Char(CharNames charName)
        {
            index = 
                unLockedChars.FindIndex(t => t.charModel.charName == charName);                
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
            SidePlankTrans.GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                 = RosterService.Instance.scrollSelectCharModel.currCharLoc.ToString();
        }

        public void Load()
        {
            foreach (CharModel charModel in CharService.Instance.allyUnLockedCompModels)
            {
                CharService.Instance.GetCharCtrlWithName(charModel.charName); 
            }
            index = 0;
            gameObject.SetActive(true);
            unLockedChars = RosterService.Instance.rosterController.GetCharUnlockedWithStatusUpdated(); 
            PopulateCharScroll();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public void Init()
        {
            foreach (Transform child in SidePlankTrans.transform)
            {
                child.GetComponent<SidePanelPtrEvents>().SidePlankInit();
            }         
        }

        public HelpName GetHelpName()
        {
            return helpName;
        }
    }



}

