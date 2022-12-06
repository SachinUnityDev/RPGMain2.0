using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.Linq; 


namespace Common
{

    public abstract class CharStatesBase 
    {
        public abstract CharStateName charStateName { get; }
        public abstract CharStateModel charStateModel { get; set; }
        public abstract CharController charController { get; set; } // set this  base apply 

        protected GameObject charGO;
        public abstract int charID { get; set; }

        protected CharStateData CharStateData = new CharStateData(); // broadcasting data

        protected string str0, str1, str2, str3, str4, str5; 
        public abstract StateFor stateFor { get; }        
        public abstract int castTime { get; protected set; }
        public List<int> allBuffs { get; set; }
        public virtual void SetCastTime(int value)
        {
            if(value > 0)
            {
                castTime = value;
                if(charStateModel != null)
                    charStateModel.castTime = value; 
            }
        }

        public virtual int startRound { get; set; }
        public virtual float dmgPerRound { get; set; }

        public virtual void ResetState()
        {
            startRound = CombatService.Instance.currentRound; 
            charController.charStateController.ResetCharStateBuff(charStateName); 
        }
        
        public virtual void StateInit(CharStateModel charStateModel, CharController charController,
            TimeFrame _timeframe, int _castTime)
        {
            this.charStateModel = charStateModel;
            this.charController = charController;
            this.charID = charController.charModel.charID;
            if (_timeframe != TimeFrame.None)   // applied time frame 
            {               
              charStateModel.timeFrame = _timeframe;
            }            
            if (_castTime > 0)
            {
                SetCastTime(castTime);
            }
            charStateModel.charStateID = UnityEngine.Random.Range(1, 500);
            charStateModel.charStateCardStrs.Clear();
            StateDisplay(); 
            CharStatesService.Instance.allCharStateModel.Add(charStateModel); 
            
        }
        public virtual void StateBaseApply()
        {
            if (charStateModel.timeFrame == TimeFrame.EndOfRound
                && charStateModel.castTime > 0)
            {
                CombatEventService.Instance.OnEOR += RoundTick; 
            }
            if (charStateModel.timeFrame == TimeFrame.EndOfCombat)
            {
                CombatEventService.Instance.OnEOR += CombatTick;
            }           
            charStateModel.startRound = CombatService.Instance.currentRound;   
            allBuffs.Clear();
        }
        public abstract void StateDisplay();
        public abstract void StateApplyFX();

        public abstract void StateApplyVFX();

        public virtual void PostCharStateApply()
        {

        }

        protected virtual void RoundTick()
        {
            int roundCounter = CombatService.Instance.currentRound - charStateModel.startRound;
            if (roundCounter >= castTime)
                EndState();
        }
        protected virtual void CombatTick()
        {          
            EndState(); 
        }

        public virtual void EndState()
        {
           //charController.charStateController.RemoveCharState(charStateName);
           // to be updated 
           if(charController.charModel.InCharStatesList.Any(t => t == charStateName))
           {
                charController.charModel.InCharStatesList.Remove(charStateName);
                // should also delete the char State base from the base list
           }


            foreach(int buffID in allBuffs)
            {
                charController.buffController.RemoveBuff(buffID);
            }  
            
        }


    }



}
