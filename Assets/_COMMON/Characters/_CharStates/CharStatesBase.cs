using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.Linq;
using Quest;

namespace Common
{

    public abstract class CharStatesBase 
    {
        public abstract CharStateName charStateName { get; }
      //  public CharStateSO1 charStateSO { get; set; }
        public CharStateModel charStateModel { get; set; }
        public CharController charController { get; set; } // set this  base apply 

        public int charID { get; set; }

        protected GameObject charGO;
        
        public int stateID { get; set; }

        protected string str0, str1, str2, str3, str4, str5;

        public List<string> allStateFxStrs = new List<string>();
        public abstract StateFor stateFor { get; }        
        public abstract int castTime { get; protected set; }

        public  TimeFrame timeFrame;
        public List<int> allBuffIds { get; set; } = new List<int>();
        public List<int> allImmunityBuffs { get; set; } = new List<int>();

        public abstract float chance { get; set; }
        public virtual void SetCastTime(int value)
        {
            if(value > 0)
            {  
                if(charStateModel != null)
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

        // Init to apply char State occurs in an instance so code bundled up for init, baseApply, applyFx etc 
        public virtual void StateInit(CharStateSO1 stateSO, CharController charController,
            TimeFrame _timeframe, int _castTime, int stateID)
        {

            // exclusion
            if (stateSO.stateFor == StateFor.Heroes)
                if (charController.charModel.orgCharMode != CharMode.Ally)
                    return;

            this.stateID = stateID;
            timeFrame = _timeframe;
            castTime = _castTime;

            charStateModel = new CharStateModel(stateSO, stateID);             
            charController.charStateController.allCharStateModels.Add(charStateModel);

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
                CombatEventService.Instance.OnEOR1 += RoundTick; 
            }
            if (timeFrame == TimeFrame.EndOfCombat)
            {
                CombatEventService.Instance.OnEOC += CombatTick;
            }
            if(timeFrame== TimeFrame.Infinity)
            {
                castTime = 100; 
            }
            if (timeFrame == TimeFrame.EndOfQuest)
            {
                QuestEventService.Instance.OnEOQ += QuestTick;
            }

            startRound = CombatService.Instance.currentRound;   
            allBuffIds.Clear();
            allStateFxStrs.Clear();
            allStateFxStrs.AddRange(new List<string>() { str0, str1, str2, str3, str4, str5 });
            foreach (string str in allStateFxStrs.ToList())
            {
                if(str == "")
                {
                    allStateFxStrs.Remove(str);
                }
            }
        }
        public abstract void StateDisplay();
        public abstract void StateApplyFX();
        public abstract void StateApplyVFX();
        public virtual void PostCharStateApply()
        {

        }
        protected virtual void RoundTick(int roundNo)
        {
            int roundCounter = roundNo - startRound;
            if (charStateName == CharStateName.Faithful || charStateName == CharStateName.Fearful)
                if (roundCounter >= 2)
                    EndState(); 

            if (roundCounter >= castTime)
                EndState();
        }
        protected virtual void CombatTick()
        {          
            // on EOC all DOT are destroyed
            if(charStateName == CharStateName.BurnHighDOT || charStateName== CharStateName.BurnLowDOT ||
               charStateName == CharStateName.BleedHighDOT || charStateName == CharStateName.BleedLowDOT ||
               charStateName == CharStateName.PoisonedHighDOT || charStateName == CharStateName.PoisonedLowDOT
               || charStateName == CharStateName.FirstBlood || charStateName == CharStateName.Fearful
               || charStateName == CharStateName.Faithful || charStateName == CharStateName.CheatedDeath
               || charStateName == CharStateName.FlatFooted)
                 EndState(); 

            if(timeFrame == TimeFrame.EndOfCombat)
                EndState();
        }
        protected virtual void QuestTick()
        {
            if (timeFrame == TimeFrame.EndOfQuest)
                EndState();
        }
        public virtual void EndState()
        {   
            charController.charStateController.RemoveCharState(charStateName);
            CombatEventService.Instance.OnEOC -= CombatTick;
            CombatEventService.Instance.OnEOR1 -= RoundTick;
            QuestEventService.Instance.OnEOQ -= QuestTick;
        }
        public virtual void ClearBuffs()
        {
            foreach (int buffID in allBuffIds)
            {
                charController.buffController.RemoveBuff(buffID);
            }
            foreach (ImmunityBuffData immuneBuffData in charController.charStateController.allImmunityBuffs.ToList())
            {
                if (allImmunityBuffs.Any(t => t == immuneBuffData.immunityID))
                {
                    charController.charStateController.RemoveImmunityBuff(immuneBuffData.immunityID);
                }
            }
        }
    }



}
