using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Claustraphobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Claustrophobia;

        public override void OnApply()
        {
            // -8 Fortitude Org in Sewers, Cave, Catacombs
            int Id = charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait
                         , (int)tempTraitName, LandscapeNames.Sewers, AttribName.fortOrg, -8);
            allLandBuffIds.Add(Id);

            Id = charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait
                         , (int)tempTraitName, LandscapeNames.Cave, AttribName.fortOrg, -8);
            allLandBuffIds.Add(Id);

            Id = charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait
                    , (int)tempTraitName, LandscapeNames.DesertCave, AttribName.fortOrg, -8);
            allLandBuffIds.Add(Id);

        }
     
    }

}

