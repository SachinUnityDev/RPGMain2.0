using Common;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class ClearmindView : MonoBehaviour, IPanel
    {
        [SerializeField] Transform posTraitTrans;
        [SerializeField] Transform negTraitTrans;
        [SerializeField] TextMeshProUGUI clearMindtxt; 

        [SerializeField] Transform charLvl;
        [SerializeField] Image portImg;
        [SerializeField] CharNames charSelect;

        [Header("left right panels")]
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;

        [SerializeField] Button closeBtn;

        [SerializeField] Button clearMindBtn; 

        [Header("char Scroll var")]
        [SerializeField] int index;
        [SerializeField] float prevLeftClick;
        [SerializeField] float prevRightClick;
        [SerializeField] Image charImg;
        [SerializeField] TextMeshProUGUI nameTxt;

        [Header("Money requrired")]
        [SerializeField] Transform currTrans; 

        [Header("Global var")]
        public List<TempTraitBuffData> posMentalTraits= new List<TempTraitBuffData>();
        public List<TempTraitBuffData> negMentalTraits = new List<TempTraitBuffData>();

        TempTraitController tempTraitController; 
        private void Awake()
        {
            
        }
        private void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            closeBtn.onClick.AddListener(closeBtnPressed);
        }


        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
            if (index == 0)
            {
                index = CharService.Instance.allCharModels.Count - 1;
                FillCharTraits();
            }
            else if (index > 0)
            {
                --index; FillCharTraits();
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if (index == CharService.Instance.allCharModels.Count - 1)
            {
                index = 0;
                FillCharTraits();
            }
            else
            {
                ++index; FillCharTraits();
            }
            prevRightClick = Time.time;
        }
        void closeBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
        public void OnClearMindPressed()
        {
            GetMentalTraits();
            FillMentalTraits();          
        }
        public void FillCharTraits()
        {
             charSelect = CharService.Instance.allCharModels[index].charName;
            BuildingIntService.Instance.selectChar = charSelect;

            CharController charController = CharService.Instance.GetCharCtrlWithName(charSelect);
          
            tempTraitController = charController.GetComponent<TempTraitController>();
         
            FillStashMoney();
            GetMentalTraits(); 
            FillMentalTraits();
            FillCharInfo();
            clearMindBtn.GetComponent<ClearmindBtnPtrEvents>()
                            .InitBtnEvents(charSelect, tempTraitController, this); 

        }
            
        void FillCharInfo()
        {
            CharacterSO charSO = CharService.Instance.allCharSO.GetCharSO(charSelect);
            charImg.sprite = charSO.charSprite;
            nameTxt.text = charSO.charNameStr; 

        }
        void GetMentalTraits()
        {
            posMentalTraits.Clear(); 
            negMentalTraits.Clear();
            foreach (TempTraitBuffData model in tempTraitController.alltempTraitApplied)
            {
                TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(model.tempTraitName); 
                if(tempSO.tempTraitType == TempTraitType.Mental)
                {
                    if(tempSO.temptraitBehavior == TraitBehaviour.Positive)
                    {
                        posMentalTraits.Add(model);
                    }
                    if (tempSO.temptraitBehavior == TraitBehaviour.Negative)
                    {
                        negMentalTraits.Add(model);
                    }
                }
            }
        }
        void FillMentalTraits()
        {
         
                for (int i = 0; i < 3; i++)
                {
                    if(i < posMentalTraits.Count)
                    {
                        posTraitTrans.GetChild(i).gameObject.SetActive(true);  
                        posTraitTrans.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                                             posMentalTraits[i].tempTraitName.ToString().CreateSpace();
                    }
                    else
                    {
                        posTraitTrans.GetChild(i).gameObject.SetActive(false);
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (i < negMentalTraits.Count)
                    {
                        negTraitTrans.GetChild(i).gameObject.SetActive(true);
                        negTraitTrans.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                                             negMentalTraits[i].tempTraitName.ToString().CreateSpace();
                    }
                    else
                    {
                        negTraitTrans.GetChild(i).gameObject.SetActive(false);
                    }
                }
                if(negMentalTraits.Count ==0 && posMentalTraits.Count ==0)
                        clearMindtxt.gameObject.SetActive(true);
                else
                    clearMindtxt.gameObject.SetActive(false);


        }
        void FillStashMoney()
        {
            LvlNExpSO lvlExpSO = CharService.Instance.lvlNExpSO;
            Currency amtReq = lvlExpSO
                    .ClearMindMoneyNeeded(tempTraitController.charController.charModel.charLvl);

            currTrans.GetChild(0).GetComponent<DisplayCurrency>().Display(amtReq);
        }
        public void Init()
        {
            
        }

        public void Load()
        {
            index = 0;
            FillCharTraits();
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true); 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}