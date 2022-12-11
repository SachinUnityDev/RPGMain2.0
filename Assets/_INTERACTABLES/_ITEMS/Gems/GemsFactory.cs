using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using Common;
namespace Interactables
{
    public class GemsFactory : MonoBehaviour
    {
        Dictionary<GemNames, Type> allGems = new Dictionary<GemNames, Type>();
        [SerializeField] int count = 0;
        void Start()
        {
            GemsInit();
        }
        public void GemsInit()
        {
            if (allGems.Count > 0) return;
            var getGems = Assembly.GetAssembly(typeof(GemBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(GemBase)));

            foreach (var getGem in getGems)
            {
                var t = Activator.CreateInstance(getGem) as GemBase;
                
                    allGems.Add(t.gemName, getGem);
            }
            count = allGems.Count;
        }

        public GemBase GetGemBase(GemNames _gemName)
        {
            foreach (var gem in allGems)
            {
                if (gem.Key == _gemName)
                {
                    var t = Activator.CreateInstance(gem.Value) as GemBase;
                    return t;
                }
            }
            Debug.Log("Gem base class Not found" + _gemName);
            return null;
        }



    }


}


