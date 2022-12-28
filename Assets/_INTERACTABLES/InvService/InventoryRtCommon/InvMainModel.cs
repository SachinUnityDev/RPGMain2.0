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

        public List<InvData> potionActivInv = new List<InvData>();
        public List<InvData> gewgawActivInv = new List<InvData>();

        #region COMMON INV
        public bool AddItem2CommInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM // Add to model => view
        {          
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
        }

        public bool RemoveItem2CommInv(Iitems item) // view=> model 
        {
            commonInvItems.Remove(item);
            return true;
        }
        #endregion


        #region  ACTIVE INV 

        public bool AddItem2PotionActInv(Iitems item)
        {


            return false; 
        }
        public bool RemoveItem2ActInv(Iitems Item)
        {

            return false; 
        }

        #endregion

























        #region STASH
        public void MoveItems2Stash(CharNames charName)
        {
            foreach (InvData invData in commonInvItems)
            {
               
            }
        }

        #endregion




        #region EXCESS INV
        public bool AddItem2Excess(Iitems item)
        {
            item.invSlotType = SlotType.ExcessInv;
            excessInvItems.Add(item);
            // excess inv drag and drop can be opened whereever inv is open
            return false;
        }
        #endregion 
        //
        // TO DO : 
        // update this with every addition and deletion and 
        // update stash/house Inv Items, excess/junk 
        // 


    }
}

