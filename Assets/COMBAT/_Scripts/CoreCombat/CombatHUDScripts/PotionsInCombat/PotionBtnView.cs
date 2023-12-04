using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Combat
{
    public enum PotionBtnState
    {   
        Normal, 
        Clicked, 
        NA, 
    }
    public class PotionBtnView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteNA;

        [SerializeField] Transform container; 

        [SerializeField] PotionBtnState state;
        CharController charController;
        Image img;
        bool isClicked = false; 

        private void Start()
        {
            CombatEventService.Instance.OnCharClicked += InitPotionSlot;
            img = GetComponent<Image>();
        }

        void InitPotionSlot(CharController charController)
        {
            container.gameObject.SetActive(false);  
            this.charController = charController;
            if (charController.charModel.orgCharMode == CharMode.Enemy)
                state = PotionBtnState.NA;
            else            
                state = PotionBtnState.Normal;
        }
        void FillPotions()
        {
            int itemCount = charController.charModel.activeInvItems.Count; 
            if(itemCount > 0) 
            for (int i = 0; i < container.childCount;  i++)
            {
                if(i< itemCount)
                container.GetChild(i).GetComponent<PotionSlotInCombatView>()
                                .Init(charController.charModel.activeInvItems[i]);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (state == PotionBtnState.NA)
                return;
            
            if (state == PotionBtnState.Normal)
            {
                container.gameObject.SetActive(true);
                state = PotionBtnState.Clicked; 
                FillPotions();
            }
            else
            {
                container.gameObject.SetActive(false);
                state = PotionBtnState.Normal;
            }
        }
    }
}