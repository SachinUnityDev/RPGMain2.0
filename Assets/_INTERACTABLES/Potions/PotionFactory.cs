using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq; 

namespace Interactables
{
    public class PotionFactory : MonoBehaviour
    {
   
        public Dictionary<PotionName, Type> allPotions;
        void Start()
        {
            allPotions = new Dictionary<PotionName, Type>();
            InitPotions();  // ON GAME START 
        }

        public void InitPotions()
        {

            if (allPotions.Count > 0) return;

            var getAllPotions = Assembly.GetAssembly(typeof(PotionsBase)).GetTypes()
                                 .Where(myType => myType.IsClass 
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(PotionsBase)));

            foreach (var potion in getAllPotions)
            {
                var t = Activator.CreateInstance(potion) as PotionsBase;
                allPotions.Add(t.potionName, potion);
            }
        }

        public PotionsBase GetPotionBase(PotionName _PotionName)
        {
            foreach (var potion in allPotions)
            {
                if (potion.Key == _PotionName)
                {
                    var t = Activator.CreateInstance(potion.Value) as PotionsBase;
                    return t;
                }
            }
            Debug.Log("Potion class Not found"+_PotionName);
            return null;
        }

    }




}



