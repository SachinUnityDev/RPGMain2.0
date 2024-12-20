﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using System;
using Combat; 

namespace Common
{
    public class PermaTraitsFactory : MonoBehaviour
    {
        public Dictionary<PermaTraitName, Type> allPermTraits = new Dictionary<PermaTraitName, Type>();
        [SerializeField] int permaTraitCount = 0;

        void Start()
        {
           InitPermTraits();
            permaTraitCount = 0; 
        }
        public void InitPermTraits()
        {
            if(allPermTraits.Count >0) return; 
           
            var getPermTraits = Assembly.GetAssembly(typeof(PermaTraitBase)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract
                                   && myType.IsSubclassOf(typeof(PermaTraitBase))); 
                                    
            foreach(var permTrait in getPermTraits)
            {
                var p = Activator.CreateInstance(permTrait) as PermaTraitBase;

                allPermTraits.Add(p.permaTraitName, permTrait); 
            }
            permaTraitCount = allPermTraits.Count;
        }
        public PermaTraitBase GetNewPermaTraitBase(PermaTraitName PermaTraitName)
        {
            foreach (var trait in allPermTraits)
            {
                if (trait.Key == PermaTraitName)
                {
                    var t = Activator.CreateInstance(trait.Value) as PermaTraitBase;
                    return t;
                }
            }
            Debug.LogError("Perma trait base not found" + PermaTraitName);
            return null;
        }
    }
}



