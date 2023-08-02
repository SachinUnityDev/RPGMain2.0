using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    public class HerbsSlotView : MonoBehaviour
    {

        [Header("slot TBR")]
        [SerializeField] Transform slot1;
        [SerializeField] Transform slot2;

        List<Transform> slots;

        [Header("Global var")]
        HealView healView;
        [SerializeField] SicknessData sicknessData; 
        public bool hasAllItemsSlot1;
        public bool hasAllItemsSlot2;

        private void Awake()
        {

        }
        public void InitHealSlots(HealView healView, SicknessData sicknessData)
        {
            slots = new List<Transform>() { slot1, slot2};
            hasAllItemsSlot1 = true;
            hasAllItemsSlot2 = true;
            this.healView = healView;
            slots[1].GetComponent<HerbSlotPtrEvents>().InitHealSlot(healView, this, sicknessData.herb2);
            slots[0].GetComponent<HerbSlotPtrEvents>().InitHealSlot(healView, this, sicknessData.herb1);
            // to make 1 selected if it has item

            if (hasAllItemsSlot1 || hasAllItemsSlot2)
            {
                healView.SetStateHealBtn(true);
            }
            else
            {
                healView.SetStateHealBtn(false);
            }
        }
        public void OnSlotSelect(int slotNum, bool hasItem)
        {
            if(slotNum == 1 && hasItem)
            {
                healView.HerbSlotSelect(slotNum);
                slots[0].GetComponent<HerbSlotPtrEvents>().SlotSelect(); // when 1 is select 2 is toggled off 
                slots[1].GetComponent<HerbSlotPtrEvents>().SlotDeSelect(); // when 1 is select 2 is toggled off 
            }
            else if (slotNum == 2 && hasItem)
            {
                healView.HerbSlotSelect(slotNum);
                slots[0].GetComponent<HerbSlotPtrEvents>().SlotDeSelect(); // when 1 is select 2 is toggled off 
                slots[1].GetComponent<HerbSlotPtrEvents>().SlotSelect();
            }
            else 
            {
                slots[0].GetComponent<HerbSlotPtrEvents>().SlotDeSelect();
                slots[1].GetComponent<HerbSlotPtrEvents>().SlotDeSelect();
                healView.SetStateHealBtn(false);
            }
        }



    }
}