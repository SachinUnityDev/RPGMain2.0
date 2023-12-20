using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class JungleFreak : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.JungleFreak;

        public override void OnApply()
        {            
            // -3 Morale in Jungle
           int Id =  charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                               LandscapeNames.Jungle , AttribName.morale, -3); 
            allLandBuffIds.Add(Id);
        }
     
    }
}