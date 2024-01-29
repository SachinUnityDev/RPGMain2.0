using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Common;

namespace Combat
{
    public abstract class EnemyPackBase
    {
        public abstract EnemyPackName enemyPackName { get; }        
        public EnemyPacksSO enemyPacksSO { get; set; }
        public List<ItemDataWithQty> lootData = new List<ItemDataWithQty>();        
        protected int val1, val2, val3, val4;
        public abstract void InitEnemyPack();        
        public virtual void EnemyPackShowLoot()
        {
            CombatEndView combatEndView = CombatService.Instance.combatEndView;
            Transform trans = combatEndView.transform;

            QuestMode questMode = QuestMissionService.Instance.currQuestMode;
            enemyPacksSO = CombatService.Instance.allEnemyPackSO.GetEnemyPackSO(enemyPackName);

            List<ItemType> types = CombatService.Instance.allEnemyPackSO.GetAllItemType(enemyPackName, questMode);

            LootService.Instance.lootController.ShowLootTableInCombat(types, enemyPackName, trans);
        }

        





    }
}