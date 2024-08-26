using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{


    public abstract class MapEbase
    {
        public abstract MapENames mapEName { get; }       

        public MapEModel mapEModel;

        public string resultStr;
        public CombatResult combatResult;
        public string strFX;
        public bool mapEResult= false;
        public bool isCombatToBePlayed = false; 
        public virtual void MapEInit(MapEModel mapEModel)
        {
           this.mapEModel= mapEModel;
            isCombatToBePlayed = false;

        }
        public abstract void OnChoiceASelect();
        public abstract void OnChoiceBSelect();    
        public abstract void MapEContinuePressed(); 
        
    }
}