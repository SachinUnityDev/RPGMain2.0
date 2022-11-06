using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class Initiator :  TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Initiator;

        public override TempTraitType traitType => throw new System.NotImplementedException();

        public override TraitBehaviour traitBehaviour => throw new System.NotImplementedException();

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

