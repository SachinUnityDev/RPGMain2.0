using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Common
{
    public abstract class LandscapeBase
    {
        public abstract LandscapeNames landscapeName { get; }
        protected List<int> buffID { get; set; }
        protected LandModel landModel { get; set;  }
        public void OnLandscapeInit(LandModel landModel)
        {
            LandscapeService.Instance.OnLandscapeEnter += OnLandscapeEnter;
            LandscapeService.Instance.OnLandscapeExit += OnLandscapeExit; 
            this.landModel = landModel;
        }
        protected abstract void OnLandscapeEnter(LandscapeNames landscapeName);        
        public virtual void TrapPositive()
        {
           
        }
        public virtual void TrapNegative()
        {
            // if fail WASD, each hero receives + 6 - 10 EXP plus:	
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                  
            }
        }
        protected abstract void OnLandscapeExit(LandscapeNames landscapeName);

    }
}