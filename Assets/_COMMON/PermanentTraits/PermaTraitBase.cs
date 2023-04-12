using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


namespace Common
{


    public abstract class PermaTraitBase
    {
        protected CharController charController; 
        public abstract PermaTraitName permaTraitName { get; }
        public PermaTraitSO permaTraitSO { get; set; }
        public  int charID { get; set; }
        public  List<int> allbuffID { get; set; } = new List<int>();
        public List<int> allStateBuffId { get; set; } = new List<int>();

        
        public virtual void ApplyTrait(CharController charController)
        {
            permaTraitSO =
                    PermaTraitsService.Instance.allPermaTraitSO.GetPermaTraitSO(permaTraitName);
            this.charController = charController;
            this.charID = charController.charModel.charID; 
        }
        
        public virtual void EndTrait()
        {
            // when char Dies
            // when char Flees quest or combat 
            for (int i = 0; i < allbuffID.Count; i++)
            {
                charController.buffController.RemoveBuff(allbuffID[i]);
            }

        }

    }
}