using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Fragile : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Fragile;
        //Always triggers Min Armor
        public override void OnApply()
        {
            int buffID = 
                charController.buffController.SetDmgORArmor2Min(CauseType.TempTrait, (int)tempTraitName
                , charID, AttribName.armorMin, TimeFrame.Infinity,1);        
            allBuffIds.Add(buffID); 
        }

   
    }
}