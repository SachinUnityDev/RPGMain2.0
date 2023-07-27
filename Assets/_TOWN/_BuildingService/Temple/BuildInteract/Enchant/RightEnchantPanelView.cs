using Interactables;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class RightEnchantPanelView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI gemNameTxt;
        [SerializeField] TextMeshProUGUI rechargeTxt;
        [SerializeField] TextMeshProUGUI weaponStateTxt;
        [SerializeField] Image gemImg;

        [SerializeField] CharNames charName; 

        private void Awake()
        {
            gemNameTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); 
            rechargeTxt = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            weaponStateTxt = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            gemImg = transform.GetChild(4).GetComponent<Image>();   
        }

        public void InitRightPanel(CharNames charName, WeaponModel weaponModel)
        {
            this.charName = charName;
            WeaponState weaponState = weaponModel.weaponState;
            GemSO gemSO;
            //        Unidentified: Identify first
            //  Identified: Enchantable by Ruri
            //  Enchanted: Enchanted with Ruri
            //  Rechargeable : Rechargeable by Ruri
        
            switch (weaponState)
            {
                case WeaponState.UnEnchantable:
                    rechargeTxt.text = "No Charges";
                    weaponStateTxt.text = "Enchantable";
                    gemImg.gameObject.SetActive(false);
                    gemNameTxt.gameObject.SetActive(false);
                    break;
                case WeaponState.Unidentified:
                    rechargeTxt.text = weaponModel.chargeRemaining.ToString() + "/12";
                    weaponStateTxt.text = "Identify first";
                    gemImg.gameObject.SetActive(false);
                    gemNameTxt.gameObject.SetActive(false);
                    break;
                case WeaponState.Identified:
                    rechargeTxt.text = weaponModel.chargeRemaining.ToString() + "/12";
                    weaponStateTxt.text = $"Enchantable by {weaponModel.gemName}";

                    gemImg.gameObject.SetActive(true);
                    gemNameTxt.gameObject.SetActive(true);
                    gemSO = ItemService.Instance.GetGemSO(weaponModel.gemName);
                    gemNameTxt.text = gemSO.gemName.ToString();
                    gemImg.sprite = gemSO.iconSprite;
                    break;
                case WeaponState.Enchanted:
                    rechargeTxt.text = weaponModel.chargeRemaining.ToString() + "/12";
                    weaponStateTxt.text = $"Enchantable by {weaponModel.gemName}";

                    gemImg.gameObject.SetActive(true);
                    gemNameTxt.gameObject.SetActive(true);
                    gemSO = ItemService.Instance.GetGemSO(weaponModel.gemName);
                    gemNameTxt.text = gemSO.gemName.ToString();
                    gemImg.sprite = gemSO.iconSprite;
                    break;
                case WeaponState.Rechargeable:      
                    rechargeTxt.text = weaponModel.chargeRemaining.ToString() + "/12";
                    weaponStateTxt.text = $"Enchantable by {weaponModel.gemName}";

                    gemImg.gameObject.SetActive(true);
                    gemNameTxt.gameObject.SetActive(true);
                    gemSO = ItemService.Instance.GetGemSO(weaponModel.gemName);
                    gemNameTxt.text = gemSO.gemName.ToString();
                    gemImg.sprite = gemSO.iconSprite;
                    break;
            }
        }
    }
}