using Common;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class LootService : MonoSingletonGeneric<LootService>
    {
        public Action<bool> OnLootDsplyToggle;        

        public LootController lootController;
        public LootFactory lootFactory;
        public LootView lootView;

        [Header("Global Var")]
        public bool isLootDsplyed = false;
        public void InitLootService()
        {
            lootController = GetComponent<LootController>();
            lootFactory = GetComponent<LootFactory>();
            lootFactory.LootInit();
            //LandscapeNames landscapeName =
            //                LandscapeService.Instance.currLandscape; 
            LandscapeNames landscapeName = LandscapeNames.Sewers; 
            lootController.InitLootController(landscapeName);
        }

        public void On_LootDsplyToggle(bool isDsplyed)
        {
            OnLootDsplyToggle?.Invoke(isDsplyed);
            isLootDsplyed= isDsplyed;            
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                lootController.ShowLootTableInLandscape(new List<ItemType>()
                {ItemType.Potions, ItemType.GenGewgaws, ItemType.PoeticGewgaws, ItemType.Gems, ItemType.SagaicGewgaws,
                 ItemType.Fruits, ItemType.Foods, ItemType.TradeGoods}, FindObjectOfType<Canvas>().transform);

            }
        }
    }
}