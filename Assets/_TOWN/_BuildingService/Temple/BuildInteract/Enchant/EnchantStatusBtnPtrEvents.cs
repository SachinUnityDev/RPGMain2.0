using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{
    public class EnchantStatusBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        // Unidentified: Identify first
        //Identified: There is enough denari to Enchant   / Not enough denari to Enchant
        //Enchanted: Enchanted with Ruri
        //Rechargeable : There is enough denari to Recharge / Not enough denari to Recharge

        //? => unidentified to identify
        // hand icon => indentify to enchant
        // lighting bolt => rechanrgeable to enchant 
        EnchantView enchantView;

        [SerializeField] Image btnImg;
        [SerializeField] TextMeshProUGUI weaponStateTxt;
        [SerializeField] Transform rechargeCostTrans;
        [SerializeField] TextMeshProUGUI rechargeCostTxt; 

        [SerializeField] WeaponStateImgNColor weaponStateImgNColor; 


        [SerializeField] CharNames charName;
        [SerializeField] WeaponState weaponState;
        [SerializeField] WeaponModel weaponModel;
        [SerializeField] Currency rechargeCost;

        private void Awake()
        {
            btnImg = transform.GetChild(0).GetComponent<Image>();
            weaponStateTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            rechargeCostTrans = transform.GetChild(2); 
            rechargeCostTxt =  rechargeCostTrans.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
     
        public void InitBtnEvents(CharNames charName, WeaponModel weaponModel, EnchantView enchantView)
        {
            this.enchantView = enchantView;
            this.charName = charName;
           this.weaponModel = weaponModel; 
            weaponState = weaponModel.weaponState;
            weaponStateImgNColor 
                = WeaponService.Instance.allWeaponSO.GetWeaponStateSpecs(weaponModel.weaponState);

            btnImg.sprite = weaponStateImgNColor.stateImg;
            weaponStateTxt.text = weaponState.ToString();
            weaponStateTxt.color = weaponStateImgNColor.stateColor;

            FillRechargeCost(); 
        }
        
        void FillRechargeCost()
        {
            if (weaponState == WeaponState.Unidentified)
            {
                rechargeCostTxt.text = "Identify Cost";
                rechargeCost = new Currency(0, 0);
            }else if (weaponState == WeaponState.Identified)
            {
                rechargeCostTxt.text = "Enchanted Cost";
                rechargeCost = WeaponService.Instance.allWeaponSO.enchantValue.DeepClone();
            }
            else if (weaponState == WeaponState.Rechargeable)
            {
                rechargeCostTxt.text = "Recharge Cost"; 
                rechargeCost = WeaponService.Instance.allWeaponSO.rechargeValue.DeepClone();
            }
            else if (weaponState == WeaponState.UnEnchantable)
            {
                rechargeCostTxt.text = " UnEnchantable";
                rechargeCost = new Currency(0, 0);
            }

            rechargeCostTrans.GetChild(1).GetComponent<DisplayCurrency>().Display(rechargeCost);
        }

        void OnEnchantBtnPressed()
        {
            int stashCurr = EcoServices.Instance.GetMoneyAmtInPlayerStash().DeepClone().BronzifyCurrency(); 

            switch (weaponState)
            {
                case WeaponState.UnEnchantable:
                    return; 
                case WeaponState.Unidentified:
                    if(stashCurr >= rechargeCost.BronzifyCurrency())
                    {
                        weaponModel.weaponState = WeaponState.Identified;
                        enchantView.FillCharPlanks(); // check stack overflow here               
                    }
                    break;
                case WeaponState.Identified:
                    if (stashCurr > rechargeCost.BronzifyCurrency())
                    {
                        weaponModel.weaponState = WeaponState.Enchanted;
                        EcoServices.Instance.DebitPlayerStash(rechargeCost);
                        enchantView.FillCharPlanks();
                    }
                    break;
                case WeaponState.Enchanted:
                    break;
                    
                case WeaponState.Rechargeable:
                    if (stashCurr > rechargeCost.BronzifyCurrency() && weaponModel.IsChargeZero())
                    {
                        weaponModel.weaponState = WeaponState.Enchanted;
                        EcoServices.Instance.DebitPlayerStash(rechargeCost);
                        enchantView.FillCharPlanks();
                    }
                    break;
                default:
                    break;                    
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnEnchantBtnPressed();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (weaponState == WeaponState.Enchanted) return;
            rechargeCostTrans.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rechargeCostTrans.gameObject.SetActive(false);
        }
    }
}