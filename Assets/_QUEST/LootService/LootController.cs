using Common;
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

        public void InitLootController(LandscapeNames landscapeName)
        {
            lootBase = LootService.Instance.lootFactory.GetLootBase(landscapeName);        
        }


    }
}
