using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class GewgawSlotViewControllerParent : MonoBehaviour
    {
        // to be attached to the parent active inv Controller 

        // rules for the gewgaw fill up

        // you cannot have two slot type(back, neck etc ..) among the three active slots 
        // you cannot have more that more one SAGAIC in active inv 
        // Can have more than poetic, lyric, folkloric, epic in active inv
        // one slot can have item 

        public bool IsGewgawPlaceable(Iitems item)
        {
        




            return false;
        }
        public bool AddItemtoActiveSlot(Iitems item, int slotID)  // NEW ITEM ADDED
        {
            Transform child = transform.GetChild(slotID);
            iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
            if (iSlotable.ItemsInSlot.Count == 0)
            {
                iSlotable.AddItem(item);
                return true;
            }
            return false;
        }

 



    }




}
