using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{


    public class LeftPanelArmorView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI charNameTxt;
        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Image portImg;
        [SerializeField] Transform rechargeCurrTrans;


        [SerializeField] CharController charController;

        [Header("Armor State")]
        [SerializeField] ArmorState armorState;
        [SerializeField] Currency fortifyCost;


        void Awake()
        {
            charNameTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            descTxt = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            portImg = transform.GetChild(3).GetComponent<Image>();
        }
        public void InitLeftPanel(CharController charController, ArmorModel armorModel)
        {
            this.charController = charController;
            CharacterSO charSO = CharService.Instance.GetCharSO(charController.charModel.charName);

            portImg.sprite = charSO.charSprite;
            charNameTxt.text = charSO.charNameStr;

            // desc txt
            Currency currInStash = EcoService.Instance.GetMoneyAmtInPlayerStash().DeepClone();
            int bronzifyStash = currInStash.BronzifyCurrency();

            descTxt.text = armorModel.armorTypeStr; 

            armorState = armorModel.GetArmorDataVsLoc(LocationName.Nekkisari).armorState; 
            if (armorState == ArmorState.Fortifiable)
            {
               // rechargeCost = WeaponService.Instance.allWeaponSO.rechargeValue.DeepClone();
            }
            else if (armorState == ArmorState.Unfortifiable)
            {
                //rechargeCost = WeaponService.Instance.allWeaponSO.rechargeValue.DeepClone();
            }
            else
            {
                //rechargeCost = new Currency(0, 0);
            }
            switch (armorState)
            {
                case ArmorState.None:
                    break;
                case ArmorState.Fortifiable:
                    break;
                case ArmorState.Fortified:
                    break;
                case ArmorState.Unfortifiable:
                    break;
                default:
                    break;
            }

            //switch (armorState)
            //{
            //    case WeaponState.UnEnchantable:
            //        descTxt.text = "Weapon cannot be Enchanted";
            //        break;
            //    case WeaponState.Unidentified:
            //        descTxt.text = "Identify first";
            //        break;
            //    case WeaponState.Identified:
            //        if (bronzifyStash > fortifyCost.BronzifyCurrency())
            //            descTxt.text = "There is enough denari to Enchant";
            //        else
            //            descTxt.text = "Not enough denari to Enchant";
            //        break;
            //    case WeaponState.Enchanted:
            //        descTxt.text = $"Enchanted with {armorModel.gemName}";
            //        break;
            //    case WeaponState.Rechargeable:
            //        if (bronzifyStash > fortifyCost.BronzifyCurrency())
            //            descTxt.text = "There is enough denari to Recharge";
            //        else
            //            descTxt.text = "Not enough denari to Recharge";
            //        break;
            //}
        }
    }
}