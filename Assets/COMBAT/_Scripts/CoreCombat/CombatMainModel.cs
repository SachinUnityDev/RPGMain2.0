using Quest;
using System;
using System.Collections;
using System.Collections.Generic;



namespace Combat
{
    [Serializable]
    public class CombatModel
    {
        public QuestNames questName; 
        public ObjNames ObjNames;
        public List<CombatResultData> resultData = new List<CombatResultData>();

        public void AddCombatResult(CombatResult combatResult, int enemiesKilled,int companionsDied)
        {
            CombatResultData combatResultData = resultData.Find(t=>t.combatResult== combatResult);
            combatResultData.count++;
            combatResultData.enemiesKilled += enemiesKilled; 
            combatResultData.companionsDied += companionsDied;   
        }


        public CombatModel()
        {
            for (int i = 1; i <= 3; i++)
            {
                CombatResultData combatResultData = new CombatResultData((CombatResult)i, 0);   
                resultData.Add(combatResultData);
            }
        }
    }
    [Serializable]
    public class CombatResultData
    {
        public CombatResult combatResult;
        public int count;

        public int enemiesKilled;
        public int companionsDied;
        public int companionsFled; 
        public CombatResultData(CombatResult combatResult, int count)
        {
            this.combatResult = combatResult;
            this.count = count;
        }
    }
}