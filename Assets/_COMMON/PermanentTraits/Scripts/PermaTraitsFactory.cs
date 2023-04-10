using System.Collections;
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
        void Awake()
        {
          
                
           
        }
 


        public void InitPermTraits()
        {
            if(allPermTraits.Count >0) return; 
           
            var getPermTraits = Assembly.GetAssembly(typeof(PermaTraitBase)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(PermaTraitBase))); 
                                    
            foreach(var permTrait in getPermTraits)
            {
                var p = Activator.CreateInstance(permTrait) as PermaTraitBase;

                allPermTraits.Add(p.permTraitName, permTrait); 
            }
        }

        //ADD
        public void AddPermTrait(PermaTraitName permaTraitName, GameObject go)
        {
            if (allPermTraits.ContainsKey(permaTraitName))
            {
               // Debug.Log("permanet trait found");
                Type permaTrait = allPermTraits[permaTraitName];
                PermaTraitBase trait = Activator.CreateInstance(permaTrait) as PermaTraitBase;
                go.AddComponent(trait.GetType());
            }
        }

        //APPLY
    }
}



