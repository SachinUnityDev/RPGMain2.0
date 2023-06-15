using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; 


namespace Quest
{
    public class ToolView : MonoBehaviour
    {
        [Header("Slot trans")]
        [SerializeField] Transform slot1Trans;
        [SerializeField] Transform slot2Trans;

        [Header("loot list and Selected list")]
        [SerializeField] Iitems toolSlot1 = null; 
        [SerializeField] Iitems toolSlot2 = null;
        public Iitems toolSelect = null;
        public void InitTootList(ToolNames toolName1, ToolNames toolName2)
        {

            if (toolName1 != ToolNames.None)
                toolSlot1 = InvService.Instance.invMainModel
                         .GetItemFromCommInv(ItemType.Tools, (int)toolName1);
            else
                toolSlot1 = null;

            if (toolName2 != ToolNames.None)
                toolSlot2 = InvService.Instance.invMainModel
                         .GetItemFromCommInv(ItemType.Tools, (int)toolName2);
            else
                toolSlot2 = null;
            toolSelect = null; 
            FillSlot(); 
        }

        void FillSlot()
        {                            
           slot1Trans.GetComponent<ToolSlotView>().InitSlot(toolSlot1, this);
           slot2Trans.GetComponent<ToolSlotView>().InitSlot(toolSlot2, this);
        }
  
        public void OnSlotSelected(Iitems item)
        {
           if (item == null) return; 
           if(toolSlot1!= null)
               if(toolSlot1.itemName != item.itemName)
               {
                  slot1Trans.GetComponent<ToolSlotView>().OnDeSelected();               
               }
           if (toolSlot2!= null)    
               if (toolSlot2.itemName != item.itemName)
               {
                   slot2Trans.GetComponent<ToolSlotView>().OnDeSelected();               
               }
            toolSelect = item; 
        }
        public void OnSlotDeSelected(Iitems item)
        {
            if(toolSelect== null) return;   
            if(toolSelect.itemName == item.itemName)
                toolSelect = null;             
        }
        #region TO_INV_FILL
        void ClearToolFill()
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);  // go
                child.gameObject.GetComponent<LootSlotView>().ClearSlot();
            }
        }
        #endregion
    }
}