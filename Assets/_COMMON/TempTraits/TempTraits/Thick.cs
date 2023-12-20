using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class Thick : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Thick; 
        //-3 Dodge
        public override void OnApply()
        {
           
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.dodge, -3, TimeFrame.Infinity, -1, true);
        }
        public override void EndTrait()
        {
            base.EndTrait();    
        }
    }
}