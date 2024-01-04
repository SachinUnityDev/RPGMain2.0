using Common;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [Serializable]  // diff in save adn kill Data structure is bcoz u can save char multiple times but can t kill a char twice
    public class CompSavedData
    {
        public CharModel strikeModel;
        public CharModel savedCompModel;
        public int savedCount;

        public CompSavedData(CharModel strikeModel, CharModel savedCompModel, int savedCount)
        {
            this.strikeModel = strikeModel;
            this.savedCompModel = savedCompModel;
            this.savedCount = savedCount;
        }
    }
    [Serializable]
    public class KilledData
    {
        public CharModel strikeModel;
        public CharModel killedModel;

        public KilledData(CharModel strikeModel, CharModel killedModel)
        {
            this.strikeModel = strikeModel;
            this.killedModel = killedModel;
        }
    }

    [Serializable]
    public class CombatModel
    {
        public QuestNames questName; 
        public ObjNames objName;


        public EnemyPackName enemyPackName;
        public CombatResult combatResult;
        public CombatEndCondition combatEndCondition; 

        [Header(" Kills and Saves")]
        public List<KilledData> allKills = new List<KilledData>();  
        public List<CompSavedData> allSaved = new List<CompSavedData>();

        public CombatModel(QuestNames questName, ObjNames objName)
        {
            this.questName = questName;
            this.objName = objName; 
        }

        public void OnCombatEnd(CombatResult combatResult, CombatEndCondition combatEndCondition)
        {
            enemyPackName = CombatService.Instance.currEnemyPack;
            this.combatResult = combatResult;
            this.combatEndCondition = combatEndCondition; 
           //CombatEventService.Instance.allCombatList // add to list 
        }

        public void AddOn_Kill(int causebyCharID, CharModel killedModel) 
        {
            CharModel strikeModel =  CharService.Instance.GetCharCtrlWithCharID(causebyCharID).charModel; 
            KilledData killData = new KilledData(strikeModel, killedModel); 
            allKills.Add(killData); 
        }
        public void AddOn_Saves(int causeByCharID, CharModel savedModel)
        {
            // find inthe list first strikeModel and saved Model combo ....if not found add new element 
            CharModel strikeModel = CharService.Instance.GetCharCtrlWithCharID(causeByCharID).charModel;
            int index = 
                allSaved.FindIndex(t=>t.strikeModel.charID == strikeModel.charID  && t.savedCompModel.charID == savedModel.charID);
            if (index != -1)
            {
                allSaved[index].savedCount++;
            }
            else
            {
                CompSavedData savedData = new CompSavedData(strikeModel, savedModel,1);
                allSaved.Add(savedData);
            }
        }

        public int GetKillsExp(int charID)
        {
            float killsExp = 0; 
            foreach(KilledData kill in allKills)
            {
                if(kill.strikeModel.charID == charID)
                {
                    killsExp += kill.killedModel.charLvl/9;     
                }
            }
            return (int)killsExp; 
        }
        public int GetSavesExp(int charID)
        {
            float saveExp = 0; 
            foreach (CompSavedData saveData in allSaved)
            {
                if(saveData.strikeModel.charID == charID)
                {
                    saveExp = (saveData.savedCompModel.charLvl * saveData.savedCount) / 6; 
                }
            }
            return (int)saveExp; 
        }
    }

}