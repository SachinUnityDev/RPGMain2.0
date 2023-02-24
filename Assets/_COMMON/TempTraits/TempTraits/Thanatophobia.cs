using Ink;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class Thanatophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Thanatophobia; 
        public override void OnApply()
        {
           // When Last Drop of Blood: -3 to Utility Stats
          // if(CharStatesService.Instance)


        }

        public override void OnEnd()
        {
            
        }
    }
}
