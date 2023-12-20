using Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace Common
{
    public class TempTraitsFactory : MonoBehaviour
    {
        Dictionary<TempTraitName, Type> allTempTraits;
        [SerializeField] int tempCount = 0; 
        void Start()
        {
            allTempTraits = new Dictionary<TempTraitName, Type>();
            InitTempTraits();//  to test all temp traits in use
        }

        public void InitTempTraits()
        {

            if (allTempTraits.Count > 0) return;
            var getTempTraits = Assembly.GetAssembly(typeof(TempTraitBase)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(TempTraitBase)));

            foreach (var tempTrait in getTempTraits)
            {
                var p = Activator.CreateInstance(tempTrait) as TempTraitBase;

                allTempTraits.Add(p.tempTraitName, tempTrait);
                tempCount++;
            }
            
        }

        

        public TempTraitBase GetNewTempTraitBase(TempTraitName tempTraitName)
        {
            foreach (var trait in allTempTraits)
            {
                if (trait.Key == tempTraitName)
                {
                    var t = Activator.CreateInstance(trait.Value) as TempTraitBase; 
                    TempTraitSO tempTraitSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(tempTraitName);                  
                    return t;
                }
            }
            Debug.Log("temp trait base not found" + tempTraitName);
            return null;
        }


    }

}
