using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class TempTraitModel
    {
        public int tempTraitID;

        public TempTraitName tempTraitName;        
        public TempTraitType tempTraitType;
        public TraitBehaviour temptraitBehavior;
       
        public int castTime = -1;
        [Header("Description")]
        public string traitNameStr = "";

        public TempTraitModel(TempTraitSO tempTraitSO, int casttime, int tempTraitID)
        {
            tempTraitName = tempTraitSO.tempTraitName;
            tempTraitType= tempTraitSO.tempTraitType;
            temptraitBehavior = tempTraitSO.temptraitBehavior;

            this.castTime = casttime;
            this.tempTraitID = tempTraitID;
     
        }
    }
}