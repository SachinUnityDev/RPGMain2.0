using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using Common;

namespace Interactables
{
    public class ArmorFactory : MonoBehaviour
    {
        [Header("ArmorBase")]
        Dictionary<ArmorType, Type> allArmor = new Dictionary<ArmorType, Type>();
        [SerializeField] int armorCount = 0;
        private void OnEnable()
        {
            ArmorInit();
        }

        void ArmorInit()
        {
            if (allArmor.Count > 0) return;

            var getAllArmor = Assembly.GetAssembly(typeof(ArmorBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(ArmorBase)));

            foreach (var armor in getAllArmor)
            {
                var t = Activator.CreateInstance(armor) as ArmorBase;
                allArmor.Add(t.armorType, armor);
            }
            armorCount = allArmor.Count;
        }

       public ArmorBase GetArmorBase(ArmorType _armorType)
        {
            foreach (var armor in allArmor)
            {
                if (armor.Key == _armorType)
                {
                    var t = Activator.CreateInstance(armor.Value) as ArmorBase;
                    
                    return t;
                }
            }
            Debug.LogError("armor base Not found" + _armorType);
            return null;
        }

    
    }
}