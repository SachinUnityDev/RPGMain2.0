using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{

    public class LootController : MonoBehaviour
    {

        public LootBase lootBase; 
        void Start()
        {

        }
        public void ShowLootTable(List<ItemType> allItemType, Transform parentTrans)
        {
            LandscapeNames landscapeNames = LandscapeNames.Sewers;
            lootBase = LootService.Instance.lootFactory.GetLootBase(landscapeNames);
            List<ItemDataWithQty> itemLS = lootBase.GetLootList(allItemType);

             LootService.Instance.lootView.InitLootList(itemLS, parentTrans);
        }
        public void InitLootController(LandscapeNames landscapeName)
        {
            if (landscapeName == LandscapeNames.None) return;
            lootBase = LootService.Instance.lootFactory.GetLootBase(landscapeName);        
        }


    }
}
