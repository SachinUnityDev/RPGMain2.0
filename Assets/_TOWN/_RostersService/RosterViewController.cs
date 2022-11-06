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

        public void PopulatePortrait()
        {
            charScrollGO.transform.GetChild(0)
                .GetComponent<CharScrollSlotController>().PopulatePortrait(unLockedChars[index]);
            //charSO = CharService.Instance.GetCharSO(unLockedChars[index]);
            //charCompSO = CharService.Instance.charComplimentarySO;

            //string charNameStr = RosterService.Instance.currCharModel.charNameStr;
            //scrollName.text = charNameStr.CreateSpace();

            //if (RosterService.Instance.currCharModel.availOfChar == AvailOfChar.Available)
            //{
            //    CharPortraitGO.transform.GetChild(1).gameObject.SetActive(true);
            //    CharPortraitGO.transform.GetChild(2).gameObject.SetActive(true);

            //    CharPortraitGO.transform.GetChild(1).GetComponent<Image>().sprite
            //                                                    = charSO.bpPortraitUnLocked;
            //    CharPortraitGO.transform.GetChild(2).GetComponent<Image>().sprite
            //                                   = charCompSO.frameAvail;
            //    CharPortraitGO.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite
            //        = CharService.Instance.charComplimentarySO.lvlBarAvail;

            //    BGUnClicked = CharService.Instance.charComplimentarySO.BGAvailUnClicked;
            //    BGClicked = CharService.Instance.charComplimentarySO.BGAvailClicked;
            //    CharPortraitGO.transform.GetChild(0).gameObject.SetActive(true);
            //    CharPortraitGO.transform.GetChild(0).GetComponent<Image>().sprite
            //                    = BGUnClicked;


            //}
            //else
            //{
            //    CharPortraitGO.transform.GetChild(0).gameObject.SetActive(false);

            //    CharPortraitGO.transform.GetChild(1).GetComponent<Image>().sprite
            //                                                   = charSO.bpPortraitUnAvail;
            //    CharPortraitGO.transform.GetChild(2).GetComponent<Image>().sprite
            //                                  = charCompSO.frameUnavail;

            //    // SIDE BARS LVL
            //    CharPortraitGO.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite
            //                = CharService.Instance.charComplimentarySO.lvlbarUnAvail;

            //}
            //CharPortraitGO.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text
            //                = RosterService.Instance.currCharModel.classType.ToString().CreateSpace();           
        }

        // create a duplicate in the list always if its available .. drag only the top one 

        void PopulateSidePlank()
        {
            // pass data values here get strings from roster model/ roster SO 
            if (RosterService.Instance.scrollSelectCharModel.availOfChar == AvailOfChar.Available)
            {
                SidePlankTrans.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = "Available";
              //  SidePlankTrans.GetChild(0).GetComponent<SidePanelPtrEvents>().descStr
              //= "Available to join your Party"; 
            } else
            {
                SidePlankTrans.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = "Not Available";
          
            }
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
            // PopulateSidepanel(); 
            // code the call function; 
            //
        }
    }



}

