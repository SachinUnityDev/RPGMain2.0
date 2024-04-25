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
        [SerializeField] Transform scrollNameGO;
        [SerializeField] GameObject charPortraitGO;
        [SerializeField] TextMeshProUGUI cultTxt;
        [SerializeField] TextMeshProUGUI raceTxt;        
      
        public List<CharModel> selectComp = new List<CharModel>();
   
        [SerializeField] TextMeshProUGUI nameTxt;
        [SerializeField] Button rightBtn;
        [SerializeField] Button leftBtn;
        [SerializeField] Image bannerImg;

        [Header("Global")]
        public RaceType selectRace ;
        public CultureType selectCult;
        public CharModel currCompOnDisplay;
        [SerializeField]int index = 0;
        [SerializeField] float prevClickRt =0;
        [SerializeField] float prevClickLeft = 0;
        bool isBannerDown = false;
      
        [SerializeField] float bannerParentPos;
        [SerializeField] float bannerClosedPos;

        [Header(" Race Btn")]
        [SerializeField] RaceBtnCompPtrEvents raceBtnCompPtrEvents; 

        [Header("Global Var")] 
        [SerializeField] CompCharParaViewController compCharParaView; 

        void Start()
        {


        }

        private void OnEnable()
        {
            nameTxt = scrollNameGO.GetChild(0).GetComponent<TextMeshProUGUI>();
            rightBtn = scrollNameGO.GetChild(1).GetComponent<Button>();
            leftBtn = scrollNameGO.GetChild(2).GetComponent<Button>();
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            leftBtn.onClick.AddListener(OnLeftBtnPressed);

  
            compCharParaView = GetComponent<CompCharParaViewController>();


            selectRace = RaceType.None;
            bannerClosedPos = bannerImg.transform.localPosition.y;
          
        }
        private void OnDisable()
        {
            
        }

        public void Init()
        {
           
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                CharNames charName = c.charModel.charName;          
                if(charName != CharNames.Abbas)
                {                  
                    selectComp.Add(c.charModel);
                }
            }
            compCharParaView = GetComponent<CompCharParaViewController>();
         
            PopulateScollData();         
           
        }
  
        void PopulateScollData()
        {
            if (selectComp.Count == 0) return;
            nameTxt.text = selectComp[index].charName.ToString();
            compCharParaView.SetPara(selectComp[index].charName);
            CharacterSO charSO =
                    CharService.Instance.GetAllySO(selectComp[index].charName);

            charPortraitGO.GetComponent<Image>().sprite = charSO.charSprite;

            selectCult = selectComp[index].cultType;
            cultTxt.GetComponentInChildren<TextMeshProUGUI>().text =
                                                        selectCult.ToString();

            selectRace = selectComp[index].raceType; 
            raceTxt.GetComponentInChildren<TextMeshProUGUI>().text =
                                                        selectRace.ToString();
            raceBtnCompPtrEvents.Init(selectRace, this); 
        }
        public void OnRaceBtnPressed()  // to be converted to race sprite
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
            if (Time.time - prevClickRt < 0.5f) return; 
            prevClickRt = Time.time;
            if (index == -1) return;
            if (index >= selectComp.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
             
            }
            PopulateScollData();
        }
        void OnLeftBtnPressed()
        {
            if (Time.time - prevClickLeft < 0.5f) return;
            prevClickLeft = Time.time;
            if (index == -1) return;

            if (index <= 0)
            {
                index = selectComp.Count - 1;
            }
            else
            {
                index--; 
            }
            PopulateScollData();
        }



    }

}