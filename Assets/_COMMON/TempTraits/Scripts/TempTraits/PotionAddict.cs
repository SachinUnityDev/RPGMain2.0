using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables; 

namespace Common
{

    public class PotionAddict : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.PotionAddict;

        public override TempTraitType traitType => throw new System.NotImplementedException();

        public override TraitBehaviour traitBehaviour => throw new System.NotImplementedException();

        public override void ApplyTempTrait(CharController _charController)
        {

        }

        public override void ChkCharImmunityfromThis(CharController _charController)
        {

        }

        public override void EndConditionCheck(CharController _charController)
        {

        }

        public override void RemoveTempTrait(CharController _charController)
        {

        }

        public override void StartConditionCheck(CharController _charController)
        {
           // PotionService.Instance.allPotionController.Find(t => t.PotionAddictCheck() == true);
        }
        void Start()
        {

        }
    }

    
}
