using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace Interactables
{

    public class CompanionViewController : MonoBehaviour
    {
        [Header("TO BE REF")]
        [SerializeField] GameObject raceBtnGO;
        [SerializeField] Transform scrollNameGO;
        [SerializeField] GameObject charPortraitGO;
        [SerializeField] Button cultBtn;
        [SerializeField] Button raceBtn; 

       
        public List<CharModel> allComp = new List<CharModel>();
        public List<CharModel> selectComp = new List<CharModel>();
   
        [SerializeField] TextMeshProUGUI nameTxt;
        [SerializeField] Button rightBtn;
        [SerializeField] Button leftBtn;
        [SerializeField] Image bannerImg;

        [Header("Lock Btn")]
        [SerializeField] GameObject lockBtn;

        [Header("Global")]
        public RaceType selectRace ;
        public CultureType selectCult;
        public CharModel currCompOnDisplay;
        int index = 0;
        bool isBannerDown = false;
        //public BtnSpreadAnim btnSpreadAnim;
        
        [SerializeField] float bannerParentPos;
        [SerializeField] float bannerClosedPos;

        public RaceBtnAnim raceBtnAnim;
        [SerializeField] bool isToggleOn = false;
        [Header("Global Var")]
        [SerializeField] bool hasInitialized = false;
        [SerializeField] CompCharParaViewController compCharParaView; 
        void Start()
        {

            nameTxt = scrollNameGO.GetChild(0).GetComponent<TextMeshProUGUI>();
            rightBtn = scrollNameGO.GetChild(1).GetComponent<Button>();
            leftBtn = scrollNameGO.GetChild(2).GetComponent<Button>();
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            
            cultBtn.onClick.AddListener(OnCultBtnPressed);
            //raceBtn.onClick.AddListener(OnRaceBtnPressed); 
            raceBtnAnim = raceBtnGO.GetComponent<RaceBtnAnim>();
            compCharParaView = GetComponent<CompCharParaViewController>();


            selectRace = RaceType.None;
            bannerClosedPos = bannerImg.transform.localPosition.y;
            hasInitialized = false; 
        }

        public void Init()
        {
            if (hasInitialized) return; 
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                CharNames charName = c.charModel.charName;
               // allComp.Clear(); selectComp.Clear(); 
                if(charName != CharNames.Abbas_Skirmisher)
                {
                    allComp.Add(c.charModel);
                    selectComp.Add(c.charModel);
                }
            }
            
            lockBtn.GetComponent<LockBtnPtrEvents>().Init(this);
            PopulateScollData();
            raceBtnAnim.Init(selectComp[index], this);
            hasInitialized = true; 
        }
        public void ToggleLock(bool isClicked)
        {
            isToggleOn = isClicked;
        }
        void PopulateScollData()
        {
            nameTxt.text = selectComp[index].charName.ToString();
            compCharParaView.SetPara(selectComp[index].charName);
            CharacterSO charSO =
                    CharService.Instance.GetAllySO(selectComp[index].charName);
            charPortraitGO.GetComponent<Image>().sprite = charSO.charSprite;

            selectCult = selectComp[index].cultType;
            cultBtn.GetComponentInChildren<TextMeshProUGUI>().text =
                                                        selectCult.ToString();

            selectRace = selectComp[index].raceType; 
            raceBtn.GetComponentInChildren<TextMeshProUGUI>().text =
                                                        selectRace.ToString();
            //PopulateBtnSprite();
            
        }
   
     
        public void OnRaceSelected(RaceType raceType)
        {
            if (isToggleOn)
                selectComp = allComp.Where(t => t.raceType == raceType).ToList();
            else
                selectComp = allComp;
            if (selectComp.Count < 1) return; 
            index = 0;
            if (raceType == selectRace) return;
            selectRace = raceType;
            PopulateScollData();
            raceBtnAnim.SetNewRaceModel(selectComp[index]);
        }

       
        void OnCultBtnPressed()
        {
            Sprite cultSprite = 
                    CharService.Instance.charComplimentarySO.GetCultBanner(selectCult);
            bannerImg.sprite = cultSprite;
            
            ToggleBanner();
            // get flag and drop flag
        }

        void ToggleBanner()
        {            
            if (!isBannerDown)
            {
                bannerImg.transform.DOLocalMoveY(0, 0.15f);
                isBannerDown = true; 
            }
            else
            {
                bannerImg.transform.DOLocalMoveY(bannerClosedPos, 0.15f);
                isBannerDown = false; 
            }
        }

        void OnRightBtnPressed()
        {
            if (index == -1) return;
            if (index >= selectComp.Count - 1)
            {
                index = selectComp.Count - 1;
            }
            else
            {
                index++;
                PopulateScollData();
            }
        }
        void OnLeftBtnPressed()
        {
            if (index == -1) return;

            if (index <= 0)
            {
                index = 0;
            }
            else
            {
                index--; PopulateScollData();
            }
        }



    }

}