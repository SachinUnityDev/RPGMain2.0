using Common;
using Interactables;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class ClearmindView : MonoBehaviour, IPanel
    {
        [Header("TBR")]
        [SerializeField] Transform posTraitTrans;
        [SerializeField] Transform negTraitTrans;
        [SerializeField] TextMeshProUGUI clearMindtxt;
        [SerializeField] TextMeshProUGUI lvlTxt;
        [SerializeField] TextMeshProUGUI lsEmptyTxt; 

        [SerializeField] Transform charLvl;
    
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
        [SerializeField] float prevCMClick; 
        [SerializeField] Image charImg;
        [SerializeField] TextMeshProUGUI nameTxt;

 

        [Header("Global var")]
        public List<TempTraitBuffData> posMentalTraits= new List<TempTraitBuffData>();
        public List<TempTraitBuffData> negMentalTraits = new List<TempTraitBuffData>();

        TempTraitController tempTraitController;

        [Header("List of Avail char")]
        List<CharController> availChars = new List<CharController>();

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
                index = availChars.Count - 1;
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
            if (index == availChars.Count - 1)
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
            if(Time.time - prevCMClick > 0.3f)
            {
                GetMentalTraits();
                FillMentalTraits();
                FilllvlTxt();
            }
        }

       void ToggleDsply(bool isNotEmpty)
        {            
            posTraitTrans.gameObject.SetActive(isNotEmpty);
            negTraitTrans.gameObject.SetActive(isNotEmpty);
            clearMindtxt.gameObject.SetActive(isNotEmpty); 
            lvlTxt.gameObject.SetActive(isNotEmpty);            
            leftBtn.gameObject.SetActive(isNotEmpty);
            rightBtn.gameObject.SetActive(isNotEmpty);
            closeBtn.gameObject.SetActive(isNotEmpty);
            clearMindBtn.gameObject.SetActive(isNotEmpty);                
            charImg.gameObject.SetActive(isNotEmpty);
            nameTxt.gameObject.SetActive(isNotEmpty);
            lsEmptyTxt.gameObject.SetActive(!isNotEmpty);
           
        }

        public void FillCharTraits()
        {
            availChars.Clear();
            availChars = CharService.Instance.allyInPlayControllers.Where(t => (t.charModel.availOfChar == AvailOfChar.Available ||                                  
                                   t.charModel.availOfChar == AvailOfChar.UnAvailable_InParty ||
                                   t.charModel.availOfChar == AvailOfChar.UnAvailable_Prereq) 
                                   && t.charModel.charLvl > 1 && t.charModel.stateOfChar == StateOfChar.UnLocked
                                   ).ToList();

            if (availChars.Count == 0)
            {
                ToggleDsply(false);
                return;
            }
            else
            {
                ToggleDsply(true);
            }

            if (index >= availChars.Count)
            {
                index = 0;
            }

            charSelect = availChars[index].charModel.charName;
            BuildingIntService.Instance.selectChar = charSelect;

            CharController charController = availChars[index];

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
            CharacterSO charSO = CharService.Instance.allCharSO.GetAllySO(charSelect);
            charImg.sprite = charSO.charSprite;
            nameTxt.text = charSO.charNameStr;
            FilllvlTxt(); 
        }
        void FilllvlTxt()
        {
            CharController charController = availChars[index];
            int currentLvl = charController.charModel.charLvl;
            lvlTxt.text = $"Level{currentLvl} -> Level{currentLvl-1}"; 
        }
        void GetMentalTraits()
        {
            posMentalTraits.Clear(); 
            negMentalTraits.Clear();
            foreach (TempTraitBuffData model in tempTraitController.alltempTraitBuffData)
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
            if (negMentalTraits.Count == 0 && posMentalTraits.Count == 0)
                clearMindtxt.gameObject.SetActive(true);
            else
                clearMindtxt.gameObject.SetActive(false);

            if (negMentalTraits.Count ==0 && posMentalTraits.Count == 0)
            {
                if(ChklvlAboveOne())
                clearMindBtn.GetComponent<ClearmindBtnPtrEvents>().SetState(true);
            }
            else
            {
                if (tempTraitController.HasTempTrait(TempTraitName.FastLearner))
                    clearMindBtn.GetComponent<ClearmindBtnPtrEvents>().SetState(false);
                else
                    clearMindBtn.GetComponent<ClearmindBtnPtrEvents>().SetState(true);
            }
        }

        bool ChklvlAboveOne()
        {
            CharController charController = availChars[index];
            if(charController.charModel.charLvl == 1)
            {
                availChars.RemoveAt(index);
                FillCharTraits();
                return false; 
            }
            return true; 
        }
        void FillStashMoney()
        {
            LvlNExpSO lvlExpSO = CharService.Instance.lvlNExpSO;
            Currency amtReq = lvlExpSO
                    .ClearMindMoneyNeeded(tempTraitController.charController.charModel.charLvl);
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