using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using System;
using Combat; 

namespace Common
{
   
     public abstract class PermTraitBase : MonoBehaviour
    {
        public virtual PermanentTraitName permTraitName { get; set; }
        public virtual TraitBehaviour traitBehaviour { get; set; }

        public virtual int charID { get; set; }

       // protected CharModData charModData; 
        public virtual void ApplyTrait(CharController charController)
        {

        }
    }

    public class PermaTraitsFactory : MonoBehaviour
    {
        public Dictionary<PermanentTraitName, Type> allPermTraits = new Dictionary<PermanentTraitName, Type>();
        public Dictionary<TempTraitName, Type> allTempTraits;

        void Awake()
        {
          
                
           
        }
        private void Start()
        {
        }
        #region PERMANENT_TRAITS

        // INIT

        public void InitPermTraits()
        {
            if(allPermTraits.Count >0) return; 
           
            var getPermTraits = Assembly.GetAssembly(typeof(PermTraitBase)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(PermTraitBase))); 
                                    
            foreach(var permTrait in getPermTraits)
            {
                var p = Activator.CreateInstance(permTrait) as PermTraitBase;

                allPermTraits.Add(p.permTraitName, permTrait); 
            }
        }

        //ADD
        public void AddPermTrait(PermanentTraitName permaTraitName, GameObject go)
        {
            if (allPermTraits.ContainsKey(permaTraitName))
            {
               // Debug.Log("permanet trait found");
                Type permaTrait = allPermTraits[permaTraitName];
                PermTraitBase trait = Activator.CreateInstance(permaTrait) as PermTraitBase;
                go.AddComponent(trait.GetType());
            }
        }

        //APPLY
        public void ApplyPermTraits(GameObject go)
        {
            CharController charController = go?.GetComponent<CharController>();
            PermTraitBase[] permaTraits = go.GetComponents<PermTraitBase>();
            foreach (PermTraitBase p in permaTraits)
            {
                p.ApplyTrait(charController);
            }
        }
        #endregion 
    }
}



