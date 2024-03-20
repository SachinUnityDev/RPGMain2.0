using DG.Tweening;
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

        [SerializeField] TextMeshProUGUI statusDsplyText;
        [SerializeField] bool hasGem; 

        private void Awake()
        {
            btnImg = transform.GetChild(0).GetComponent<Image>();
            weaponStateTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            rechargeCostTrans = transform.GetChild(3); 
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
            statusDsplyText.gameObject.SetActive(false);

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
                rechargeCostTxt.text = "Enchantment Cost";
                rechargeCost = WeaponService.Instance.allWeaponSO.enchantValue.DeepClone();
            }
            else if (weaponState == WeaponState.Rechargeable)
            {
                rechargeCostTxt.text = "Recharge Cost"; 
                rechargeCost = WeaponService.Instance.allWeaponSO.rechargeValue.DeepClone();
            }
            else if (weaponState == WeaponState.Unenchantable)
            {
                rechargeCostTxt.text = " Unenchantable";
                rechargeCost = new Currency(0, 0);
            }

            rechargeCostTrans.GetChild(1).GetComponent<DisplayCurrency>().Display(rechargeCost);
        }

        void OnEnchantBtnPressed()
        {
            int currency = EcoServices.Instance.GetMoneyFrmCurrentPocket().DeepClone().BronzifyCurrency(); 

            switch (weaponState)
            {
                case WeaponState.Unenchantable:
                    statusDsplyText.text = $"{charName} has no magic tendency"; 
                    
                    return; 
                case WeaponState.Unidentified:
                    if(currency >= rechargeCost.BronzifyCurrency())
                    {
                        statusDsplyText.text = $"Now you can Enchant the weapon";
                        weaponModel.weaponState = WeaponState.Identified;
                        enchantView.FillCharPlanks(); // check stack overflow here               
                    }
                    else
                    {
                        statusDsplyText.text = $"Not enough money";
                    }
                    break;
                case WeaponState.Identified:
                    if (currency > rechargeCost.BronzifyCurrency())
                    {
                        weaponModel.SetRechargeValue();
                        weaponModel.weaponState = WeaponState.Enchanted;
                        EcoServices.Instance.DebitMoneyFrmCurrentPocket(rechargeCost);
                        enchantView.FillCharPlanks();
                        statusDsplyText.text = $"Weapon Enchanted with {weaponModel.gemName}";
                    }
                    else
                    {
                        statusDsplyText.text = $"Not enough money";
                    }
                    break;
                case WeaponState.Enchanted:
                    break;
                    
                case WeaponState.Rechargeable:
                    if (currency > rechargeCost.BronzifyCurrency())
                    {
                        if (weaponModel.IsChargeZero())
                        {
                            if(weaponModel.noOfTimesRecharged < 4)
                            {
                                weaponModel.SetRechargeValue();
                                EcoServices.Instance.DebitMoneyFrmCurrentPocket(rechargeCost);
                                enchantView.FillCharPlanks();
                                statusDsplyText.text = $"Weapon Recharged";
                            }
                            else // >=4
                            {
                               hasGem = weaponModel.ExhaustedGem();
                                if (hasGem)
                                {
                                    statusDsplyText.text = $"Weapon Enchanted with {weaponModel.gemName}";
                                }
                                else
                                {
                                    statusDsplyText.text = $"{weaponModel.gemName} not found in Inventory";
                                }
                            }
                        }
                    }
                    else
                    {
                        statusDsplyText.text = $"Not enough money";
                    }
                    break;
                
                default:
                    break;                    
            }
            statusDsplyText.gameObject.SetActive(true);
            Sequence seq = DOTween.Sequence();
            seq
               .AppendCallback(() => statusDsplyText.GetComponent<TextRevealer>().Reveal())
               .AppendInterval(3.0f)
               .AppendCallback(() => statusDsplyText.GetComponent<TextRevealer>().Unreveal())
               ;
            seq.Play();

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(weaponModel.weaponState != WeaponState.Enchanted)
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