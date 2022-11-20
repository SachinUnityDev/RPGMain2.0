using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Interactables
{
    [System.Serializable]
    public class ItemData
    {
        public ItemType itemType;// should have a item ID for better control
        public int ItemName;

        public ItemData(ItemType itemType, int itemName)
        {
            this.itemType = itemType;
            ItemName = itemName;
        }
    }

    [System.Serializable]
    public class InvModel   // Deprecated 
    {
        public int charID;
        public CharNames charName;
        public int invID;
        public int commonInvSize = 0;
       
        public List<Iitems> activeInvItems = new List<Iitems>();
        // max 3 are specific for chars // potion have active slot in combat panel for interaction
        // gewgaw and potions (potions slots has {provision slot as last slot})

        public InvModel(CharModel charModel)
        {
            charID = charModel.charID;
            charName = charModel.charName;
        }




       public int GetInvSize()
        {
            int charInPlay = CharService.Instance.allyInPlay.Count;
            //Rule : Abbas 2 X 6,  each added hero has 1X6 (Locked)
            int invSize = (2 * 6) + ((charInPlay - 1) * (1 * 6)); 

            return invSize; 
        }
    }



}
