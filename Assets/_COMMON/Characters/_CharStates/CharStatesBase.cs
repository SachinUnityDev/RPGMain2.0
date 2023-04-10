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
        public CharStateSO1 charStateSO { get; set; }
        public abstract CharController charController { get; set; } // set this  base apply 

        protected GameObject charGO;
        public abstract int charID { get; set; }

        protected CharStateData CharStateData = new CharStateData(); // broadcasting data

        protected string str0, str1, str2, str3, str4, str5;

        public List<string> charStateCardStrs = new List<string>();
        public abstract StateFor stateFor { get; }        
        public abstract int castTime { get; protected set; }

        public  TimeFrame timeFrame;
        public List<int> allBuffIds { get; set; } = new List<int>();
        public List<int> allImmunityBuffs { get; set; } = new List<int>();
        public virtual void SetCastTime(int value)
        {
            if(value > 0)
            {
                castTime = value;
                if(charStateSO != null)
                    castTime = value; 
            }
        }
        public virtual void IncrCastTime(int incrBy)
        {
            SetCastTime(castTime + incrBy);
            foreach (int buffID in allBuffIds)
            {
                charController.buffController.IncrBuffCastTime(buffID, incrBy);
            }
        }
        public virtual int startRound { get; set; }
        public virtual float dmgPerRound { get; set; }
        public virtual void ResetState()
        {
            startRound = CombatService.Instance.currentRound; 
            charController.charStateController.ResetCharStateBuff(charStateName); 
        }        
        public virtual void StateInit(CharStateSO1 stateSO, CharController charController,
            TimeFrame _timeframe, int _castTime)
        {
            this.charStateSO = stateSO;
            this.charController = charController;
            this.charID = charController.charModel.charID;
            if (_timeframe != TimeFrame.None)   // applied time frame 
            {               
              timeFrame = _timeframe;
            }            
            if (_castTime > 0)
            {
                SetCastTime(castTime);
            }            
            StateDisplay();
            StateBaseApply(); 
        }
        public virtual void StateBaseApply()
        {
            if (timeFrame == TimeFrame.EndOfRound
                && castTime > 0)
            {
                CombatEventService.Instance.OnEOR += RoundTick; 
            }
            if (timeFrame == TimeFrame.EndOfCombat)
            {
                CombatEventService.Instance.OnEOC += CombatTick;
            }
            if(timeFrame== TimeFrame.Infinity)
            {
                castTime = 100; 
            }
            startRound = CombatService.Instance.currentRound;   
            allBuffIds.Clear();
        }
        public abstract void StateDisplay();
        public abstract void StateApplyFX();
        public abstract void StateApplyVFX();
        public virtual void PostCharStateApply()
        {

        }
        protected virtual void RoundTick()
        {
            int roundCounter = CombatService.Instance.currentRound - startRound;
            if (roundCounter >= castTime)
                EndState();
        }
        protected virtual void CombatTick()
        {          
            // on EOC all DOT are destroyed
            if(charStateName == CharStateName.BurnHighDOT || charStateName== CharStateName.BurnLowDOT ||
               charStateName == CharStateName.BleedHighDOT || charStateName == CharStateName.BleedLowDOT ||
               charStateName == CharStateName.PoisonedHighDOT || charStateName == CharStateName.PoisonedLowDOT)
            EndState(); 
        }

        public virtual void EndState()
        {
           //charController.charStateController.RemoveCharState(charStateName);
           // to be updated 
           //if(charController.charModel.InCharStatesList.Any(t => t == charStateName))
           //{
           //     charController.charModel.InCharStatesList.Remove(charStateName);
           //     // should also delete the char State base from the base list
           //}
            foreach(int buffID in allBuffIds)
            {
                charController.buffController.RemoveBuff(buffID);
            }
            foreach (ImmunityBuffData immuneBuffData in charController.charStateController.allImmunityBuffs)
            {

                if(allImmunityBuffs.Any(t=>t == immuneBuffData.immunityID))
                {
                    charController.charStateController.RemoveImmunityBuff(immuneBuffData.immunityID); 
                }
            }

            CombatEventService.Instance.OnEOR -= CombatTick;
            CombatEventService.Instance.OnEOR -= RoundTick;
        }


    }



}
