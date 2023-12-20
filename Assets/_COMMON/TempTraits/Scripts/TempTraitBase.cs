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

        public CharController charController { get; set; } // set this  base apply 
        public int charID { get; set; }
        public int traitID { get; set; }
        public int castTime { get; protected set; }
        public List<int> allBuffIds { get; set; } = new List<int>();
        public List<int> allLandBuffIds { get; set; } = new List<int>(); 

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
            TraitBaseApply();
        }
        public virtual void TraitBaseApply()
        {
            CalendarService.Instance.OnStartOfCalDay += DayTick;
            startDay = CalendarService.Instance.dayInGame;
            allBuffIds.Clear();
        }
        public abstract void OnApply();

        void DayTick(int day)
        {
            if (castTime == -1) return; // NA case
            int dayCounter = day - startDay;
            if (dayCounter >= castTime)
                EndTrait();
        }
        public virtual void EndTrait()
        {
            CalendarService.Instance.OnStartOfCalDay -= DayTick;
            ClearBuffs();
            charController.tempTraitController.RemoveTraitByName(tempTraitName); 
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
        }
    }
}