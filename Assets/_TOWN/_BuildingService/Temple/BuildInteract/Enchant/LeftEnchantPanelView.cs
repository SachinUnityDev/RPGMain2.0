using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{

    public class LeftEnchantPanelView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI charNameTxt;        
        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Image portImg;
        [SerializeField] Transform rechargeCurrTrans;


        [SerializeField] CharNames charName;

        [Header("Weapon State")]
        [SerializeField] WeaponState weaponState;
        [SerializeField] Currency rechargeCost; 


        void Awake()
        {
            charNameTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            descTxt = transform.GetChild(2).GetComponent<TextMeshProUGUI>(); 
            portImg = transform.GetChild(3).GetComponent<Image>();
        }
        public void InitLeftPanel( CharNames charName, WeaponModel weaponModel)
        {            
            this.charName = charName;
            CharacterSO charSO = CharService.Instance.GetCharSO(charName);
            
            portImg.sprite = charSO.charSprite;
            charNameTxt.text = charSO.charNameStr;

            // desc txt
            Currency currInStash = EcoService.Instance.GetMoneyAmtInPlayerStash().DeepClone(); 
            int bronzifyStash = currInStash.BronzifyCurrency();

            weaponState = weaponModel.weaponState; 
            if(weaponState== WeaponState.Rechargeable)
            {
                rechargeCost = WeaponService.Instance.allWeaponSO.rechargeValue.DeepClone();
            }
            else if(weaponState == WeaponState.Enchanted)
            {
                rechargeCost = WeaponService.Instance.allWeaponSO.rechargeValue.DeepClone();
            }
            else
            {
                rechargeCost = new Currency(0,0); 
            }
            
            switch (weaponState)
            {
                case WeaponState.Unenchantable:
                    descTxt.text = "Weapon cannot be Enchanted";
                    break;
                case WeaponState.Unidentified:
                    descTxt.text = "Identify first"; 
                    break;
                case WeaponState.Identified:
                    if (bronzifyStash > rechargeCost.BronzifyCurrency())
                        descTxt.text = "There is enough denari to Enchant";
                    else
                        descTxt.text = "Not enough denari to Enchant";                      
                        break;
                case WeaponState.Enchanted:
                    descTxt.text = $"Enchanted with {weaponModel.gemName}";
                    break;
                case WeaponState.Rechargeable:
                    if (bronzifyStash > rechargeCost.BronzifyCurrency())
                        descTxt.text = "There is enough denari to Recharge"; 
                    else
                        descTxt.text = "Not enough denari to Recharge";
                    break;
            }
               
        }
    }
}

// print cost
// Unidentified: Identify first
//Identified: There is enough denari to Enchant   / Not enough denari to Enchant
//Enchanted: Enchanted with Ruri

//Rechargeable : There is enough denari to Recharge / Not enough denari to Recharge
