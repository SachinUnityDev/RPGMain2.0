using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq; 

namespace Interactables
{
    [System.Serializable]
    public class InvData
    {
        public CharNames charName;      
        public Iitems item;      

        public InvData(CharNames charName, Iitems item)
        {
            this.charName = charName;
            this.item = item;
        }
    }

    [System.Serializable]
    public class InvMainModel 
    {
        public List<Iitems> commonInvItems = new List<Iitems>();
        // Abbas 3 X 6,  each added Companion has 2X6 (Locked)(Town, QuestPrepPhase, in camp, in MapInteraction)
        //public List<Iitems> persCommInvItems = new List<Iitems>(); 

        public List<Iitems> stashInvIntems = new List<Iitems>();
        // 6 X 6 behaves like a common Inventory (Open Town/House) 

        public List<Iitems> excessInvItems = new List<Iitems>();
        // 4X6 Behaves like a Excess Inv inv ()        

        public bool AddItem2CommInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM 
        {
            //if (invData.charName == CharNames.Abbas_Skirmisher)
            //{
                // check 3 X 6 limitation  
                //List<InvData> abbasInv = InvService.Instance.invMainModel.commonInvItems
                //                            .Where(t => t.charName == invData.charName).ToList();
                if (!InvService.Instance.IsCommInvFull())
                {
                    item.invSlotType = SlotType.CommonInv;
                    commonInvItems.Add(item);
                    InvService.Instance.invViewController.AddItem2InVView(item);
                    return true;
                }
                else
                {
                    Debug.Log("Inv Size is full");
                    return false;
                }
           // }
            //else
            //{
            //    List<InvData> charInv = commonInvItems
            //                            .Where(t => t.charName == invData.charName).ToList();

            //    if (InvService.Instance.IsCommInvFull())
            //    {
            //        commonInvItems.Add(invData);
            //        InvService.Instance.invViewController.AddItem2InVView(invData.item);
            //        return true;
            //    }
            //}
            //return false;
        }

        public bool RemoveItem2CommInv(Iitems item)
        {
            commonInvItems.Remove(item);
            return true;
        }

        public void MoveItems2Stash(CharNames charName)
        {
            foreach (InvData invData in commonInvItems)
            {
               
            }
        }

        public bool AddItem2Excess(Iitems item)
        {

            item.invSlotType = SlotType.ExcessInv;
            excessInvItems.Add(item);
            // excess inv drag and drop can be opened whereever inv is open
            return false;
        }

        //
        // TO DO : 
        // update this with every addition and deletion and 
        // update stash/house Inv Items, excess/junk 
        // 


    }
}

