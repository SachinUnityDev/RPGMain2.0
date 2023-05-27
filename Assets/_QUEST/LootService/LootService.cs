using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public class LootService : MonoSingletonGeneric<LootService>
    {

        public LootController lootController;

        public LootFactory lootFactory;
        private void Start()
        {
            lootController= GetComponent<LootController>(); 
            lootFactory= GetComponent<LootFactory>();   
        }

        public void InitLootService()
        {
            LandscapeNames landscapeName =
                            LandscapeService.Instance.currLandscape; 
            lootController.InitLootController(landscapeName);
        }

    }
}