using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class SwampyCramp :  TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.SwampyCramp;

        public override TempTraitType traitType => TempTraitType.Mental;

        public override TraitBehaviour traitBehaviour => TraitBehaviour.Negative;

        public override void ApplyTempTrait(CharController _charController)
        {
            throw new System.NotImplementedException();
        }

        public override void ChkCharImmunityfromThis(CharController _charController)
        {
            throw new System.NotImplementedException();
        }

        public override void EndConditionCheck(CharController _charController)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveTempTrait(CharController _charController)
        {
            throw new System.NotImplementedException();
        }

        public override void StartConditionCheck(CharController _charController)
        {
            throw new System.NotImplementedException();
        }



        // Start is called before the first frame update
        void Start()
        {

        }


    }
}
