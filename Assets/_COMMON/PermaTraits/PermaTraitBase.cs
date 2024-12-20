using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public abstract class PermaTraitBase
    {
        protected CharController charController;
        public int charID;
        public abstract PermaTraitName permaTraitName { get; }
        public PermaTraitSO permaTraitSO;

        public PermaTraitModel permaTraitModel; 
        public  List<int> allBuffIds = new List<int>();
        public List<int> allCharStateBuffId = new List<int>();
        public List<int> allLandBuffIds = new List<int>();
        public List<int> allBuffDmgAltIds = new List<int>();
        public List<int> allBuffDmgRecAltIds = new List<int>();
        public List<int> allCharStateDmgAltBuffIds = new List<int>();
        public List<int> allStatAltBuff = new List<int>();

        public virtual PermaTraitModel PermaTraitInit(PermaTraitSO permaTraitSO, CharController charController, int traitID)
        {
            this.permaTraitSO = permaTraitSO;
            this.charController = charController;
            charID = charController.charModel.charID;
           
            permaTraitModel = new PermaTraitModel(permaTraitSO, traitID, charID);
            charController.permaTraitController.allPermaModels.Add(permaTraitModel);    
            TraitBaseApply(); // check this out 
            return permaTraitModel; 
        }
        protected virtual void TraitBaseApply()
        {   
            allBuffIds.Clear();
            allLandBuffIds.Clear();
            allBuffDmgAltIds.Clear();
            allBuffDmgRecAltIds.Clear();
            allCharStateDmgAltBuffIds.Clear();
        }

        public abstract void ApplyTrait(); 
        
        public virtual void EndTrait()
        {
            ClearBuffs();
        }
        
        protected virtual void ClearBuffs()
        {
            foreach (int buffID in allBuffIds)
            {
                charController.buffController.RemoveBuff(buffID);
            }
            foreach (int LbuffID in allLandBuffIds)
            {
                charController.landscapeController.RemoveBuff(LbuffID);
            }
            foreach (int dmgRecBuffID in allBuffDmgRecAltIds)
            {
                charController.damageController.RemoveDmgReceivedAltBuff(dmgRecBuffID);
            }
            foreach (int dmgAltBuffID in allBuffDmgAltIds)
            {
                charController.strikeController.RemoveDmgAltBuff(dmgAltBuffID);
            }
            foreach (int charStateDmgAltbuffID in allCharStateDmgAltBuffIds)
            {
                charController.strikeController.RemoveDmgAltCharStateBuff(charStateDmgAltbuffID);
            }
            foreach (int statBuffID in allStatAltBuff)
            {
                charController.statBuffController.RemoveStatRecAltBuff(statBuffID);
            }
        }
    }
}