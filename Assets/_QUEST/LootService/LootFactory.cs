using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using Common;

namespace Quest
{
    public class LootFactory : MonoBehaviour
    {

        [Header("Loot Factory")]
        Dictionary<LandscapeNames, Type> allLoot = new Dictionary<LandscapeNames, Type>();
        [SerializeField] int LootCount = 0;
        private void Start()
        {
            LootInit();
        }

        void LootInit()
        {
            if (allLoot.Count > 0) return;

            var getAllLoot = Assembly.GetAssembly(typeof(LootBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(LootBase)));

            foreach (var loot in getAllLoot)
            {
                var t = Activator.CreateInstance(loot) as LootBase;
                allLoot.Add(t.landscapeName, loot);
            }
            LootCount = allLoot.Count;
        }

        public LootBase GetLootBase(LandscapeNames landscapeName)
        {
            foreach (var loot in allLoot)
            {
                if (loot.Key == landscapeName)
                {
                    var t = Activator.CreateInstance(loot.Value) as LootBase;
                    t.InitLootTable();
                    return t;
                }
            }
            Debug.LogError("Loot base Not found" + landscapeName);
            return null;
        }


    }
}