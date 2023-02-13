using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{


    public class Agile : TempTraitBase
    {    // to code START AND END conditions 
        public override TempTraitName tempTraitName => TempTraitName.Agile;

        public override TempTraitType traitType => TempTraitType.Physical;
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive; 

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
          
        }






        //public override void ApplyTempTrait(CharacterController _charController )
        //{
        //    charController = _charController;
        //    if(charController!= null)
        //    {
        //        int  valueChange = Random.Range(1, 3);  // 1,2 in data 
        //        charController.ChangeStat(StatsName.Dodge,valueChange,0,0);
        //        valueChange = Random.Range(1, 3);  //1,2 in data 
        //        charController.ChangeStat(StatsName.acc, valueChange, 0, 0);                  
        //    }   

        //}

        //public override bool ChkCharImmunityfromThis(CharacterController _charController)
        //{
        //    // character controller should stock what its immune too... 
        //    return false;
        //}



    }




}

