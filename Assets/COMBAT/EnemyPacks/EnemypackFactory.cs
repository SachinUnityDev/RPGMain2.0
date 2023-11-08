using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using Quest; 

namespace Combat
{

    public class EnemypackFactory : MonoBehaviour
    {
        public Dictionary<EnemyPackName, Type> allEnemyPackType = new Dictionary<EnemyPackName, Type>();
        [SerializeField] int enemyPackCount = 0;


        public void InitEnemyPack()
        {
            if (allEnemyPackType.Count > 0) return;

            var getAllEPacks = Assembly.GetAssembly(typeof(EnemyPackBase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(EnemyPackBase)));

            foreach (var ePacks in getAllEPacks)
            {
                var t = Activator.CreateInstance(ePacks) as EnemyPackBase;
                allEnemyPackType.Add(t.enemyPackName, ePacks);
            }
            enemyPackCount = allEnemyPackType.Count;
        }

        public EnemyPackBase GetEnemyPack(EnemyPackName enemyPackName)
        {
            foreach (var enemyPType in allEnemyPackType)
            {
                if (enemyPType.Key == enemyPackName)
                {
                    var t = Activator.CreateInstance(enemyPType.Value) as EnemyPackBase;
                    return t;
                }
            }
            Debug.Log("Enemy Pack base not found!" + enemyPackName);
            return null;
        }
    }
}