using Combat;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class TempTraitBase
    {
        public abstract TempTraitName tempTraitName { get; }
        public TempTraitSO tempTraitSO;

        public TempTraitModel tempTraitModel;
        protected int startDay; // day in the game 

        // resting
        protected int startDayForResting;
        protected int restTime = -1;
        protected AvailOfChar preRestAvailOfChar; 
        public CharController charController { get; set; } // set this  base apply 
        public int charID { get; set; }
        public int traitID { get; set; }
        public int castTime { get; protected set; }

        public List<int> allBuffIds = new List<int>();
        public List<int> allLandBuffIds = new List<int>();        
        public List<int> allBuffDmgAltIds  = new List<int>();
        public List<int> allBuffDmgRecAltIds = new List<int>();
        public List<int> allCharStateDmgAltBuffIds = new List<int>();
        public List<int> allStatAltBuff = new List<int>();

        public virtual void TempTraitInit(TempTraitSO tempTraitSO, CharController charController, int traitID)
        {
            this.tempTraitSO = tempTraitSO;
            this.charController = charController;
            this.charID = charController.charModel.charID;
            if (castTime != -1)  // NA case
                castTime = UnityEngine.Random.Range(tempTraitSO.minCastTime, tempTraitSO.maxCastTime + 1);
            else
                castTime = -1; 
            tempTraitModel = new TempTraitModel(tempTraitSO, traitID, castTime);
            charController.tempTraitController.allTempTraitModels.Add(tempTraitModel);
            TraitBaseApply(); // chekc this out 
        }
        public virtual void TraitBaseApply()
        {
            CalendarService.Instance.OnStartOfCalDay += DayTick;
            startDay = CalendarService.Instance.calendarModel.dayInGame;
            allBuffIds.Clear();
            allLandBuffIds.Clear();
            allBuffDmgAltIds.Clear();
            allBuffDmgRecAltIds.Clear();
            allCharStateDmgAltBuffIds.Clear();
        }
        public abstract void OnApply();

        void DayTick(int day)
        {
            if (castTime == -1) return; // NA case
            int dayCounter = day - startDay;
            if (dayCounter >= castTime)
            {
                OnEndConvert();
                EndTrait();
            }
        }
        public virtual void EndTrait()
        {
            CalendarService.Instance.OnStartOfCalDay -= DayTick;
            CalendarService.Instance.OnStartOfCalDay -= DayTickResting;

            ClearBuffs();
            charController.tempTraitController.RemoveTraitByName(tempTraitName); 
        }
        public virtual void OnEndConvert()
        {
            
        }

        public virtual void OnHealApplied()
        {
            CalendarService.Instance.OnStartOfCalDay += DayTickResting;
            startDayForResting= CalendarService.Instance.calendarModel.dayInGame;
            restTime = tempTraitModel.sicknessData.restTimeInday;
            preRestAvailOfChar = charController.charModel.availOfChar; 
            charController.charModel.availOfChar = AvailOfChar.UnAvailable_Resting;
            DayTickResting(CalendarService.Instance.calendarModel.dayInGame); 
        }

        void DayTickResting(int day)
        {            
            int dayCounter = day - startDayForResting;
            
            if (dayCounter >= restTime)
            {
                charController.charModel.availOfChar = preRestAvailOfChar; 
                OnEndConvert();
                EndTrait();
            }
        }

        public virtual void ClearBuffs()
        {
            foreach (int buffID in allBuffIds)
            {
                charController.buffController.RemoveBuff(buffID);
            }
            foreach (int LbuffID in allLandBuffIds)
            {
                charController.landscapeController.RemoveBuff(LbuffID);
            }
            foreach (int dmgRecBuffID in allBuffDmgRecAltIds)
            {
                charController.damageController.RemoveDmgReceivedAltBuff(dmgRecBuffID);
            }
            foreach (int dmgAltBuffID in allBuffDmgAltIds)
            {
                charController.strikeController.RemoveDmgAltBuff(dmgAltBuffID);
            }
            foreach (int charStateDmgAltbuffID in allCharStateDmgAltBuffIds)
            {
                charController.strikeController.RemoveDmgAltCharStateBuff(charStateDmgAltbuffID);
            }
            foreach (int statBuffID in allStatAltBuff)
            {
                charController.statBuffController.RemoveStatRecAltBuff(statBuffID);
            }
        }
    }
}