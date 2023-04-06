using Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace Common
{

    public class TempTraitData
    {
        public float castTime = -5f;
        public TempTraitName tempTraitName;
        public CharNames causedbyChar;
        public CharNames effectedChar;
        public Vector3 effectedCharPos;

        public TempTraitData(float castTime, TempTraitName tempTraitName, CharNames causedbyChar
            , CharNames effectedChar, Vector3 effectedCharPos)
        {
            this.castTime = castTime;
            this.tempTraitName = tempTraitName;
            this.causedbyChar = causedbyChar;
            this.effectedChar = effectedChar;
            this.effectedCharPos = effectedCharPos;
        }
    }

    public abstract class TempTraitBase
    {
        public abstract TempTraitName tempTraitName { get; }
        public CharController charController; 

        public virtual void TempTraitInit()
        {
            // to be completed
        }

        public abstract void OnApply(CharController charController);

        public virtual void OnTraitEnd()
        {

        }

    }

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

        //ADD
        //public void AddPermTrait(TempTraitName tempTraitName, GameObject go)
        //{
        //    if (allTempTraits.ContainsKey(tempTraitName))
        //    {
        //        // Debug.Log("permanet trait found");
        //        // do the immunity check 
        //        Type permaTrait = allTempTraits[tempTraitName];
        //        PermTraitBase trait = Activator.CreateInstance(permaTrait) as PermTraitBase;
        //        go.AddComponent(trait.GetType());
        //    }
        //}

        public TempTraitBase GetNewTempTraitBase(TempTraitName tempTraitName)
        {
            foreach (var trait in allTempTraits)
            {
                if (trait.Key == tempTraitName)
                {
                    var t = Activator.CreateInstance(trait.Value) as TempTraitBase; 
                    return t;
                }
            }
            Debug.Log("temp trait base not found" + tempTraitName);
            return null;
        }


    }

}
