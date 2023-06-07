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
        public TempTraitSO tempTraitSO;

        public int castTime; 
        public virtual void TempTraitInit(TempTraitSO tempTraitSO)
        {
           this.tempTraitSO = tempTraitSO;
            castTime = UnityEngine.Random.Range(tempTraitSO.minCastTime, tempTraitSO.maxCastTime);  
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

        

        public TempTraitBase GetNewTempTraitBase(TempTraitName tempTraitName)
        {
            foreach (var trait in allTempTraits)
            {
                if (trait.Key == tempTraitName)
                {
                    var t = Activator.CreateInstance(trait.Value) as TempTraitBase; 
                    TempTraitSO tempTraitSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(tempTraitName);
                    t.TempTraitInit(tempTraitSO); 
                    return t;
                }
            }
            Debug.Log("temp trait base not found" + tempTraitName);
            return null;
        }


    }

}
