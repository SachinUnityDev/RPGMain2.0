using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using Common;

namespace Interactables
{
    public class HerbsFactory : MonoBehaviour
    {
        Dictionary<HerbNames, Type> allHerbs = new Dictionary<HerbNames, Type>();
        [SerializeField] int count = 0;
        void Start()
        {
            HerbsInit();
        }
        public void HerbsInit()
        {
            if (allHerbs.Count > 0) return;
            var getHerbs = Assembly.GetAssembly(typeof(HerbBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(HerbBase)));

            foreach (var getHerb in getHerbs)
            {
                var t = Activator.CreateInstance(getHerb) as HerbBase;

                allHerbs.Add(t.herbName, getHerb);
            }
            count = allHerbs.Count;
        }

        public HerbBase GetHerbBase(HerbNames _herbName)
        {
            foreach (var herb in allHerbs)
            {
                if (herb.Key == _herbName)
                {
                    var t = Activator.CreateInstance(herb.Value) as HerbBase;
                    return t;
                }
            }
            Debug.Log("Herb base class Not found" + _herbName);
            return null;
        }


    }
}

