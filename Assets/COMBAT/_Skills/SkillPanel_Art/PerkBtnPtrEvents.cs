using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;
using TMPro;
using UnityEngine.UI;
using Interactables;

namespace Combat
{


    public class PerkBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] InvSkillViewMain skillViewMain;
        [SerializeField]CharController charController;
        [SerializeField]SkillController1 skillController;
        SkillViewSO skillViewSO;
        [SerializeField]PerkData perkData;
        private void Start()
        {
            InvService.Instance.OnCharSelectInvPanel += OnCharSelect;
            SkillService.Instance.OnPerkStateChg += OnPerkStateChg;
        }
        void OnPerkStateChg(PerkData _perkData)
        {
            if(perkData.perkName == _perkData.perkName)
            {
                perkData.state =_perkData.state;
                SetPerkBtnImage(_perkData.state);
            }
        }
        public void Init(PerkData _perkData, InvSkillViewMain _skillViewMain)
        {
            perkData = _perkData;
            skillViewMain = _skillViewMain;
            skillViewSO = skillViewMain.skillViewSO;
            // fill in name and btn image
            transform.GetComponentInChildren<TextMeshProUGUI>().text
                                                     = perkData.perkName.ToString();
            SetPerkBtnImage(perkData.state);
        }

        void OnCharSelect(CharModel charModel)
        {
            this.charController = InvService.Instance.charSelectController;
            skillController = charController.skillController;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (skillViewMain.isPerkClickAvail)
            {
                if(perkData.state== PerkSelectState.Clickable)
                {
                    skillController.OnPerkClicked(perkData);
                 
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
        void SetPerkBtnImage(PerkSelectState _state)
        {
            switch (_state)
            {
                case PerkSelectState.Clickable:
                    transform.GetComponent<Image>().sprite = skillViewSO.perkBtnNormal;
                    break;
                case PerkSelectState.Clicked:
                    transform.GetComponent<Image>().sprite = skillViewSO.perkBtnSelect;
                    break;
                case PerkSelectState.UnClickable:
                    transform.GetComponent<Image>().sprite = skillViewSO.perkBtnUnSelectable;
                    break;
                default:
                    break;
            }
        }
    }
}