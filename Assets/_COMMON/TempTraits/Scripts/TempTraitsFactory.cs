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

    //public abstract class TempTraitBase
    //{
    //    public abstract TempTraitName tempTraitName { get; }


    //    public virtual  void Init(CharController charController)
    //    {

    //    }
        
    //    public abstract void OnApply();

    //    public abstract void OnEnd();

    //}


    public abstract class TempTraitBase : MonoBehaviour
    {
        public abstract TempTraitName tempTraitName { get; }
        public abstract TempTraitType traitType { get;  }
        public abstract TraitBehaviour traitBehaviour { get; }

        public abstract void ApplyTempTrait(CharController _charController);

        public abstract void RemoveTempTrait(CharController _charController);

        public abstract void StartConditionCheck(CharController _charController);

        public abstract void EndConditionCheck(CharController _charController);

        public abstract void ChkCharImmunityfromThis(CharController _charController);
    }

    public class TempTraitsFactory : MonoBehaviour
    {
        Dictionary<TempTraitName, Type> allTempTraits;

        void Start()
        {
            allTempTraits = new Dictionary<TempTraitName, Type>();
           // InitTempTraits();  to test all temp traits in use
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

                 gameObject.AddComponent(p.GetType()); 
            }
        }

        //ADD
        public void AddPermTrait(TempTraitName tempTraitName, GameObject go)
        {
            if (allTempTraits.ContainsKey(tempTraitName))
            {
                // Debug.Log("permanet trait found");
                // do the immunity check 
                Type permaTrait = allTempTraits[tempTraitName];
                PermTraitBase trait = Activator.CreateInstance(permaTrait) as PermTraitBase;
                go.AddComponent(trait.GetType());
            }
        }




    }

}
