using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
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
       

        private void Start()
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
        }
        void FillPotions()
        {
            CharNames charName = charController.charModel.charName; 
            ActiveInvData activeInvData = InvService.Instance.invMainModel.GetActiveInvData(charName); 
           if(activeInvData != null) 
            for (int i = 0; i < container.childCount;  i++)
            {
                if(i < activeInvData.potionActivInv.Count)
                    container.GetChild(i).GetComponent<PotionSlotInCombatView>()
                                .Init(activeInvData.potionActivInv[i]);
                else
                    container.GetChild(i).GetComponent<PotionSlotInCombatView>()
                               .Init(null);
            }
           else
            for (int i = 0; i < container.childCount; i++)
            {
                container.GetChild(i).GetComponent<PotionSlotInCombatView>()
                            .Init(null);
            }
                
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