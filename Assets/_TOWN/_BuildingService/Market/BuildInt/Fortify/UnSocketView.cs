using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public enum UnsocketStatus
    {
        Socketed,
        UnSocketed,
    }

    public class UnSocketView : MonoBehaviour, IPanel
    {

        FortifyMainView fortifyMainView;


        [Header("left right panels TBR")]
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;


        [SerializeField] Button reverseBtn;

        [Header("Center img and Slot")]
        [SerializeField] Transform centerTrans;

        [Header("btm currency and status update btn")]
        [SerializeField] Transform statusBtn;
        [SerializeField] Transform CurrTrans;
        [SerializeField] TextMeshProUGUI statusTxt; 

        [Header("Curr Selects")]
        [SerializeField] CharNames charSelect;

        [Header("char Scroll var")]
        [SerializeField] int index;
        [SerializeField] float prevLeftClick;
        [SerializeField] float prevRightClick;
        List<CharController> availChars = new List<CharController>();

        private void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            reverseBtn.onClick.AddListener(OnReverseBtnPressed);
            index = 0;
        }

        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
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
            if (Time.time - prevRightClick < 0.3f) return;
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
        void ToggleDsply(bool isNotEmpty)
        {


        }
        public void FillCharPlanks()
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

            CharController charController = availChars[index];
            BuildingIntService.Instance.selectChar = charController.charModel.charName; 
            ArmorController armorController = charController.armorController;
            ArmorModel armorModel = armorController.armorModel;

            centerTrans.GetComponent<UnSocketViewCenter>().InitUnSocketCenter(charController, armorModel, this);
            statusBtn.GetComponent<UnSocketBtnPtrEvents>().InitUnSocketBtnPtrEvents(charController, armorModel, this);

            CurrTrans.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
        }
        void OnReverseBtnPressed()
        {
            fortifyMainView.OnReverseBtnPressed();
        }
        public void InitUnSocketView(FortifyMainView fortifyMainView)
        {
            this.fortifyMainView = fortifyMainView;
            CurrTrans.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
        }

        public void StatusUpdate(UnsocketStatus unsocketStatus)
        {
            switch (unsocketStatus)
            {
                case UnsocketStatus.Socketed:
                    statusTxt.text = "Socketed"; 
                    break;
                case UnsocketStatus.UnSocketed:
                    statusTxt.text = "Unsocketed";
                    break;           
                default:
                    break;
            }


        }
        public void Init()
        {
            index = 0;
            FillCharPlanks();
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