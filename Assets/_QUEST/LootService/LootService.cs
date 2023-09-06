using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class LootService : MonoSingletonGeneric<LootService>
    {
        public LootController lootController;

        public LootFactory lootFactory;
        public LootView lootView;

        [Header("Global Var")]
        public bool isLootOpen = false;


        private void Start()
        {
           
        }

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



        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {               
                //lootController.ShowLootTable(new List<ItemType>()
                //{ItemType.Potions, ItemType.GenGewgaws, ItemType.PoeticGewgaws, ItemType.Gems, ItemType.SagaicGewgaws,
                //ItemType.Fruits, ItemType.Foods, ItemType.TradeGoods});
                //
            }
        }
    }
}