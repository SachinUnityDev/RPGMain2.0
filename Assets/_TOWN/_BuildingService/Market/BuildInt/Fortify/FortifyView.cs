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
        UnFortifiable,
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
            CharNames selectChar = CharService.Instance.allCharModels[index].charName;
            BuildingIntService.Instance.selectChar = selectChar;

            CharController charController = CharService.Instance.GetCharCtrlWithName(selectChar);
            ArmorController armorController = charController.armorController;
            ArmorModel armorModel = armorController.armorModel;

            centerTrans.GetComponent<FortifyViewCenter>().InitFortifyPanel(selectChar, armorModel, this);
            statusBtn.GetComponent<FortifyBtnPtrEvents>().InitFortifyBtn(selectChar, armorModel, this);

            currTrans.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
            LocationName locName = TownService.Instance.townModel.currTown; 
            statustxt.text = armorModel.GetArmorDataVsLoc(locName).armorState.ToString(); 

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