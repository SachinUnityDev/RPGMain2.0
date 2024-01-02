using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Combat;
using Quest;
using System;
using System.Security.Policy;

namespace Common
{
    [Serializable]
    public class CombatLootSizeData
    {
        public CombatLootSize combatLootSize; 
        public List<CombatLootVsQMode> allLootSizeVsQMode = new List<CombatLootVsQMode>();    
    }

    [Serializable]
    public class CombatLootVsQMode
    {
        public QuestMode questMode;
        public int size; 
    }


    [CreateAssetMenu(fileName = "AllEnemySO", menuName = "Combat/AllEnemySO")]

    public class AllEnemyPackSO : ScriptableObject
    {
        public List<ItemType> lootTypes = new List<ItemType>();
        public List<EnemyPacksSO> allEnemyPack = new List<EnemyPacksSO>();
        public List<CombatLootSizeData> allCombatLootSizeData = new List<CombatLootSizeData>();    



        public List<ItemType> GetAllItemType(EnemyPackName enemyPackName, QuestMode questMode)
        {
            EnemyPacksSO enemyPackSO = GetEnemyPackSO(enemyPackName);
            CombatLootSize combatLootSize = enemyPackSO.combatLootSize;
            int lootSize = GetLootSize(questMode,combatLootSize);
            return GetItemLs(lootSize);            
        }
        int GetLootSize(QuestMode questMode, CombatLootSize combatLootSize)
        {
            int index = allCombatLootSizeData.FindIndex(t=>t.combatLootSize== combatLootSize);
            if(index != -1)
            {
                int index2 = allCombatLootSizeData[index].allLootSizeVsQMode.FindIndex(t=>t.questMode==questMode);
                if(index2 != -1)
                {
                    return allCombatLootSizeData[index].allLootSizeVsQMode[index2].size;
                }
                else
                {
                    Debug.LogError(" loot size not found!"); 
                        return 0; 
                }
            }
            else
            {
                Debug.LogError(" loot size not found");
                return 0;
            }
        }

        public EnemyPacksSO GetEnemyPackSO(EnemyPackName enemyPackName, bool isBoss = false)
        {
            int index = allEnemyPack.FindIndex(t=>t.enemyPack == enemyPackName && t.isBossPack == isBoss);
            if (index != -1)
                return allEnemyPack[index];
            else
                Debug.Log("Enemy pack Not found"); 
            return null; 
        }
        public List<ItemType> GetItemLs(int lootSize)
        {
            lootTypes.Clear();

            if (70f.GetChance())  //1
                lootTypes.Add(ItemType.GenGewgaws);
            else
                lootTypes.Add(ItemType.PoeticGewgaws);

            if (60f.GetChance())  //2
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Fruits);

            if (50f.GetChance())//3
                lootTypes.Add(ItemType.Potions);
            else
                lootTypes.Add(ItemType.Herbs);

            if (10f.GetChance()) //4
                lootTypes.Add(ItemType.Pouches);
            else if (50f.GetChance())
                lootTypes.Add(ItemType.Tools);
            else
                lootTypes.Add(ItemType.Gems);

         //   if (40f.GetChance()) //5
           //     lootTypes.Add(ItemType.Scrolls);
            //else
                lootTypes.Add(ItemType.LoreBooks);

            if (50f.GetChance())  //6
                lootTypes.Add(ItemType.GenGewgaws);
            else if (50f.GetChance())
                lootTypes.Add(ItemType.PoeticGewgaws);
            else
                lootTypes.Add(ItemType.SagaicGewgaws);

            if (50f.GetChance())  //7
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Potions);

            if (50f.GetChance()) //8
                lootTypes.Add(ItemType.Fruits);
            else
                lootTypes.Add(ItemType.Herbs);

            if (50f.GetChance())  //9
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Potions);

            if (20f.GetChance()) //10
                lootTypes.Add(ItemType.SagaicGewgaws);
            else
                lootTypes.Add(ItemType.GenGewgaws);

            if (10f.GetChance())  //11
                lootTypes.Add(ItemType.Pouches);
            else if (50f.GetChance())
                lootTypes.Add(ItemType.Tools);
            else
                lootTypes.Add(ItemType.Gems);

            if (40f.GetChance())  //12
                lootTypes.Add(ItemType.Potions);
            else if (50f.GetChance())
                lootTypes.Add(ItemType.Herbs);
            else
                lootTypes.Add(ItemType.Foods);

            List<ItemType> list = new List<ItemType>();
            for (int i = 0; i < lootSize; i++)
            {
                list.Add(lootTypes[i]); 
            }
            return list;
        }

    }
}