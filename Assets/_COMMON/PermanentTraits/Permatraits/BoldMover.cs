using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace Common
{
    public class BoldMover : PermaTraitBase
    {
        // Gain 3 fortitude each dodge(6 when night)  
        //Double fortitude gain at night(except the Fortitude potion)
        public override PermaTraitName permaTraitName => PermaTraitName.BoldMover;
        public override void ApplyTrait(CharController _charController)
        {         
            charController.OnAttribCurrValSet += DodgeCheck;
            charController.OnStatChg += IncreaseFortitude;         
        }

        void DodgeCheck(AttribModData charModData)
        {
            if (charModData.attribModified == AttribName.dodge)
            {
                // Gain 3 fortitude each dodge(6 when night)  
                if (QuestEventService.Instance.questTimeState == TimeState.Night)
                {
                    charController.ChangeStat(CauseType.PermanentTrait, (int)permaTraitName, charID
                            , StatName.fortitude, 6.0f, false);
                   
                }else            
                {                  
                    charController.ChangeStat(CauseType.PermanentTrait, (int)permaTraitName, charID, StatName.fortitude, 3.0f,false);
                }
            }
        }

        void IncreaseFortitude(StatModData statModData)
        {
            //Double fortitude gain at night(except the Fortitude potion)
            // to code except the fortitude potion or in fortitude potion add false
            if (statModData.statModified == StatName.fortitude)
            {
                if (QuestEventService.Instance.questTimeState == TimeState.Night)                
                    charController.ChangeStat(CauseType.PermanentTrait, (int)permaTraitName, charID
                        , StatName.fortitude, statModData.modVal, false); 
                //sending only x1 as one increase would have taken place before
                //reaching here

            }
        }

      
    }


}

