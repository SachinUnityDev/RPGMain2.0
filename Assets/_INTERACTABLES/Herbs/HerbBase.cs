using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat; 


namespace Interactables
{
    public abstract class HerbBase
    {
        public HerbNames herbName { get;  }        
     

        protected CharController charController;
        public abstract CharNames charName { get; set; }
        public abstract int charID { get; set; }
        public abstract HerbModel herbModel { get; set; }
        public virtual HerbModel HerbInit(HerbSO herbSO, CharController charController)
        {
            Iitems item = this as Iitems;
            item.maxInvStackSize = herbSO.maxInventoryStack;
            // CONTROLLERS AND MODELS
            this.charController = charController;
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;

            herbModel = new HerbModel(herbSO, charController);
            return herbModel;

        }

        public int maxInvStackSize { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public SlotType invType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        // apply fx method=> remove sickNess after a timedelay
        // herb model cost,inventory slot  and herb .. no max world instance
        // 


    }
}
