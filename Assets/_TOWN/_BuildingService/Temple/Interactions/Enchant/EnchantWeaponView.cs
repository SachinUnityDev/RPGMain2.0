using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class EnchantWeaponView : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image weaponImg; 
        
        EnchantView enchantView;
        [SerializeField] WeaponModel weaponModel;
        [SerializeField] CharNames selectChar; 


        [SerializeField] Transform leftCharPanel;
        [SerializeField] Transform rightGemPanel; 
        private void Awake()
        {
            weaponImg = transform.GetChild(0).GetComponent<Image>();          
        }
        public void InitWeaponPanel(CharNames selectChar, WeaponModel weaponModel,EnchantView enchantView)
        {
            this.enchantView = enchantView;
            this.weaponModel = weaponModel; 
            this.selectChar = selectChar;

            WeaponSO weaponSO = WeaponService.Instance.allWeaponSO.GetWeaponSO(selectChar);
            weaponImg.sprite = weaponSO.weaponSprite;           
        }

        void ShowRightLeftPanel()
        {
            rightGemPanel.gameObject.SetActive(true);
            leftCharPanel.gameObject.SetActive(true);

            leftCharPanel.GetComponent<LeftEnchantPanelView>().InitLeftPanel(selectChar, weaponModel);
            rightGemPanel.GetComponent<RightEnchantPanelView>().InitRightPanel(selectChar, weaponModel);
        }
        void HideRightLeftPanel()
        {
            rightGemPanel.gameObject.SetActive(false);
            leftCharPanel.gameObject.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowRightLeftPanel();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideRightLeftPanel();
        }
    }
}