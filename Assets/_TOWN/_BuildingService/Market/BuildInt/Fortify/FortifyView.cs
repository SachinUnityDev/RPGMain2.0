using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Town
{
    public enum ArmorState
    {
        None,
        Fortifiable, 
        Fortified,
        Unfortifiable,
    }
    public class FortifyView : MonoBehaviour, IPanel
    {
        FortifyMainView fortifyMainView; 

        [Header("left right panels")]
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;


        [SerializeField] Button reverseBtn;

        [Header("Center img and Slot")]
        [SerializeField] Transform centerTrans;  

        [Header("btm currency and status update btn")]
        [SerializeField] Transform statusBtn;
        [SerializeField] Transform currTrans;
        [SerializeField] TextMeshProUGUI statustxt;
        [SerializeField] TextMeshProUGUI buffTxt; 

        [Header("Curr Selects")]
        [SerializeField] CharNames charSelect;

        [Header("char Scroll var")]
        [SerializeField] int index;
        [SerializeField] float prevLeftClick;
        [SerializeField] float prevRightClick;



        private void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            reverseBtn.onClick.AddListener(OnReverseBtnPressed);
            index = 0; 
        }

        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.1f) return;
            if (index == 0)
            {
                index = CharService.Instance.allCharModels.Count - 1;
                FillCharPlanks();
            }
            else if (index > 0)
            {
                --index; FillCharPlanks();
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.1f) return;
            if (index == CharService.Instance.allCharModels.Count - 1)
            {
                index = 0;
                FillCharPlanks();
            }
            else
            {
                ++index; FillCharPlanks();
            }
            prevRightClick = Time.time;
        }

        public void FillCharPlanks()
        {
           int  selectCharID = CharService.Instance.allCharModels[index].charID;
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(selectCharID);
            BuildingIntService.Instance.selectChar = charController.charModel.charName;

            ArmorController armorController = charController.armorController;
            ArmorModel armorModel = armorController.armorModel;

            centerTrans.GetComponent<FortifyViewCenter>().InitFortifyPanel(charController, armorModel, this);
            statusBtn.GetComponent<FortifyBtnPtrEvents>().InitFortifyBtn(charController, armorModel, this);

            currTrans.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
            statustxt.text = armorModel.armorState.ToString();
            FillBuffStr(armorModel, charController);
        }
        void FillBuffStr(ArmorModel armorModel, CharController charController)
        {
            switch (armorModel.armorState)
            {
                case ArmorState.None:
                    buffTxt.text = ""; 
                    break;
                case ArmorState.Fortifiable:
                    ArmorSO armorSO = ArmorService.Instance.allArmorSO.GetArmorSOWithType(armorModel.armorType); 
                    buffTxt.text = armorSO.allLines[0].ToString();
                    break;
                case ArmorState.Fortified:
                    ArmorBase armorBase = charController.armorController.armorBase;
                    buffTxt.text = armorBase.allLines[0].ToString();
                    break;
                case ArmorState.Unfortifiable:
                    buffTxt.text = "";
                    break;
                default:
                    buffTxt.text = "";
                    break;
            }
      
        }
        void OnReverseBtnPressed()
        {
            fortifyMainView.OnReverseBtnPressed(); 
        }
        public void InitFortifyView(FortifyMainView fortifyMainView)
        {
            this.fortifyMainView = fortifyMainView;
            currTrans.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
        }

        public void OnFortifyBtnPressed(CharController charController,ArmorModel armorModel)
        {
            ArmorService.Instance.OnArmorFortifyPressed(charController, armorModel);
            FillCharPlanks();
        }
        public void Init()
        {
            index = 0;
        }

        public void Load()
        {
            index = 0;
            FillCharPlanks();
        }
        

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }


    }
}