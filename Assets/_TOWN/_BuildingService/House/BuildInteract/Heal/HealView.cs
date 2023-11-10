using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class HealView : MonoBehaviour, IPanel
    {
        [Header("No one Sick trans")]
        [SerializeField] Transform NoOneSickTrans;
        [Header("HEAL VIEW MAIN")]
        [SerializeField] Transform healViewTrans; 

        [SerializeField] Transform sicknessView;
        [Header("Heal Slot Container")]
        [SerializeField] Transform herbSlots; 

        [Header("Scroll related")]
        [SerializeField] TextMeshProUGUI healDesctxt;    
        [SerializeField] CharNames charSelect;
        
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
        [SerializeField] Button closeBtn;

        [Header(" Heal Btn")]
        [SerializeField] HealBtnPtrEvents healBtn;

        [Header("Char Scroll var")]
        [SerializeField] int index;
        [SerializeField] float prevLeftClick;
        [SerializeField] float prevRightClick;
        [SerializeField] Image charImg;
        [SerializeField] TextMeshProUGUI nameTxt;

        [Header("Global var")]
        public List<TempTraitBuffData> sicknessTraits = new List<TempTraitBuffData>();

        [Header("Herb/ heal Slot related")]
        [SerializeField] SicknessData sicknessData;
        [SerializeField] int slotSelect;
        [SerializeField] HerbNQuantity herbNQuantitySelect; 


        [Header(" Sickness related")]
        TempTraitController tempTraitController;
        TempTraitBuffData selectTrait;

        private void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
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


        // Init for this view 
        public void FillCharTraits()
        {
            charSelect = CharService.Instance.allCharModels[index].charName;
            BuildingIntService.Instance.selectChar = charSelect;

            CharController charController = CharService.Instance.GetCharCtrlWithName(charSelect);

            tempTraitController = charController.GetComponent<TempTraitController>();
            if(GetSicknessTraits() > 0)
            {
                FillSicknessTraits();
                FillCharInfo();
                healBtn.GetComponent<HealBtnPtrEvents>()
                                    .InitBtnEvents(charSelect, tempTraitController, this);
            }
            else
            {
                if (ChkIsAnyOneSick())
                {
                    OnRightBtnPressed();
                }
            }
        }
        bool ChkIsAnyOneSick()
        {
            bool isAnyOneSick = TempTraitService.Instance.IsAnyOneSick();
            if (isAnyOneSick)
            {
                ToggleMainHealView();
            }
            else
            {
                ToggleNoOneSick();
            }
            return isAnyOneSick; 
        }
        public void HerbSlotSelect(int slotSelect)
        {
            this.slotSelect = slotSelect;
            if (slotSelect == 1)
                herbNQuantitySelect = sicknessData.herb1;
            if (slotSelect == 2)
                herbNQuantitySelect = sicknessData.herb2;
            healDesctxt.text = $"Resting Period {sicknessData.restTimeInday} days"; 

        }
        public void OnHealBtnPressed()
        {
            tempTraitController.OnHealBtnPressed(selectTrait);
        
            // remove items from inv too 
            for (int i = 0; i < herbNQuantitySelect.qty; i++)
            {
                ItemData itemData = new ItemData(ItemType.Herbs, (int)herbNQuantitySelect.herbName);
                InvService.Instance.invMainModel.RemoveItemFrmCommInv(itemData); 
            }
            FillCharTraits();
        }
        public void SetStateHealBtn(bool isClickable)
        {
            Debug.Log("Is clickable" + isClickable);
            healBtn.GetComponent<HealBtnPtrEvents>().SetState(isClickable);
        }
        public void OnSicknessSelect(TempTraitName tempTraitName)
        {
            TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(tempTraitName);
            sicknessData = tempSO.sicknessData;            
                herbSlots.GetComponent<HerbsSlotView>().InitHealSlots(this, sicknessData); 
            selectTrait = sicknessTraits.Find(t=>t.tempTraitName== tempTraitName);
        }
        void FillCharInfo()
        {
            CharacterSO charSO = CharService.Instance.allCharSO.GetAllySO(charSelect);
            charImg.sprite = charSO.charSprite;
            nameTxt.text = charSO.charNameStr;
        }
        int GetSicknessTraits()
        {
            sicknessTraits.Clear();         
            foreach (TempTraitBuffData model in tempTraitController.alltempTraitApplied)
            {
                TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(model.tempTraitName);
                if (tempSO.tempTraitType == TempTraitType.Sickness)
                {
                    sicknessTraits.Add(model);
                }
            }
            return sicknessTraits.Count;
        }
        void FillSicknessTraits()
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < sicknessTraits.Count)
                {
                    sicknessView.GetChild(i).gameObject.SetActive(true);
                    sicknessView.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                                              sicknessTraits[i].tempTraitName.ToString().CreateSpace();
                }
                else
                {
                    sicknessView.GetChild(i).gameObject.SetActive(false);
                }
                sicknessView.GetComponent<SicknessView>().InitSicknessNames(this, sicknessTraits);
            }            
            if (sicknessTraits.Count == 0)
            {
                healDesctxt.gameObject.SetActive(false);
                healBtn.GetComponent<HealBtnPtrEvents>().SetState(false);
            }
            else
            {
                healDesctxt.gameObject.SetActive(true);
                healBtn.GetComponent<HealBtnPtrEvents>().SetState(true);                
                OnSicknessSelect(sicknessTraits[0].tempTraitName); 
            }
        }
        public void Init()
        {
            
        }
        public void Load()
        {
            index = 0;
            FillCharTraits();
            ChkIsAnyOneSick(); 
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
        public void ToggleNoOneSick()
        {
            healViewTrans.gameObject.SetActive(false);
            NoOneSickTrans.gameObject.SetActive(true);
        }
        public void ToggleMainHealView()
        {
            healViewTrans.gameObject.SetActive(true);
            NoOneSickTrans.gameObject.SetActive(false);
        }

    }
}