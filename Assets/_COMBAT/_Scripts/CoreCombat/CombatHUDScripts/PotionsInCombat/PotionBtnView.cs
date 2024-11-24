using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Combat
{

    public class PotionBtnView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("To be ref")]
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteNA;

        [SerializeField] Transform container; 
        CharController charController;
        Image img;

        [SerializeField] TextMeshProUGUI apWarningTxt;
        [Header(" Item card")]
        [SerializeField] GameObject itemCardRef; 
        private void OnEnable()
        {
            CombatEventService.Instance.OnCharClicked -= InitPotionSlot;
            CombatEventService.Instance.OnCharClicked += InitPotionSlot;
            img = GetComponent<Image>();            
            img.sprite = spriteN;
            container.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnCharClicked -= InitPotionSlot;
        }
        void InitPotionSlot(CharController charController)
        {
           itemCardRef = ItemService.Instance.itemCardGO;
            container.gameObject.SetActive(false);  
            this.charController = charController;
            if (charController.charModel.orgCharMode == CharMode.Enemy)
            {
                img.sprite = spriteNA;                
            }
            else
            {
                img.sprite = spriteN;
            }
            ToggleNoApWarningTxt(false);
        }
        void FillPotions()
        {
            int  charID = charController.charModel.charID; 
            ActiveInvData activeInvData = InvService.Instance.invMainModel.GetActiveInvData(charID); 
           if(activeInvData != null) 
            for (int i = 0; i < container.childCount;  i++)
            {
                if(i < activeInvData.potionActiveInv.Length)
                    container.GetChild(i).GetComponent<PotionSlotInCombatView>()
                                .Init(activeInvData.potionActiveInv[i], this);
                if(i == 2)
                    container.GetChild(i).GetComponent<PotionSlotInCombatView>()
                               .Init(activeInvData.provisionSlot, this);
            }
            else
            {
                for (int i = 0; i < container.childCount; i++)
                {
                    container.GetChild(i).GetComponent<PotionSlotInCombatView>()
                                .Init(null, this);
                }
            }   
        }
        public void ToggleNoApWarningTxt(bool show)
        {
            apWarningTxt.gameObject.SetActive(show);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (charController.charModel.orgCharMode == CharMode.Enemy)
            {
                img.sprite = spriteNA;
                return;
            }
            if (!container.gameObject.activeInHierarchy)
            {
                container.gameObject.SetActive(true);
                img.sprite = spriteHL; 
                FillPotions();
            }
            else
            {
                container.gameObject.SetActive(false);
                img.sprite = spriteN;
            }
            itemCardRef.SetActive(false);   
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(" POINTER ENTER POTION"); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log(" POINTER EXIT POTION");
        }
    }
}