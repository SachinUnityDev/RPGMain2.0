using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq; 
namespace Common
{
    #region Abstract_Definitions
    public abstract class MonthEventBase : MonoBehaviour
    {
        public virtual MonthName monthName { get; set; }
        public virtual string MonthShortName { get; set; }
        public virtual void ApplyMonthSpecs(CharController charController)
        {

        }
    }

    public abstract class  WeekEventBase :MonoBehaviour
    {
        public virtual WeekName weekName { get;  set; }
        public virtual void OnWeekEnter(CharController charController) { }
        public virtual void OnWeekTick(CharController characterController) { }
        public virtual void OnWeekExit(CharController charController) { }
        
    }
    public abstract class DayEventBase : MonoBehaviour
    {
        public virtual DayName dayName { get; set; }
        public virtual string dayStr { get; set; }
        public virtual void ApplyDaySpecs(CharController charController)
        {

        }
    }

    #endregion
   
    
    public class CalendarFactory : MonoBehaviour
    {
        public Dictionary<WeekName, Type> AllWeekEvents = new Dictionary<WeekName, Type>();

        void Start()
        {
            InitWeekEvents(); 
        }

        public void InitWeekEvents()
        {
            
            if (AllWeekEvents.Count > 0) return;
            
            var getWeekEvents = Assembly.GetAssembly(typeof(WeekEventBase)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(WeekEventBase)));

            foreach (var weekEvent in getWeekEvents)
            {
               
                var p = Activator.CreateInstance(weekEvent) as WeekEventBase;
                AllWeekEvents.Add(p.weekName, weekEvent);
                //Debug.Log("week events added" + getWeekEvents);
            }
        }

        public void AddWeekEvent(WeekName _weekName, GameObject go)
        {
            if (AllWeekEvents.ContainsKey(_weekName))
            {
                Type weekEvent = AllWeekEvents[_weekName];
                WeekEventBase  weekInst = Activator.CreateInstance(weekEvent) as WeekEventBase;
                Debug.Log("Event Added " + weekInst.weekName); 
                go.AddComponent(weekInst.GetType());
            }
        }
    }


}



//namespace Common
//{
//    public abstract class TempTraitBase : MonoBehaviour
//    {
//        public virtual TempTraitName tempTraitName { get; set; }
//        public virtual void ApplyTrait(GameObject _charGO)
//        {

//        }
//    }

//    public abstract class PermTraitBase : MonoBehaviour
//    {
//        public virtual PermanentTraitName permTraitName { get; set; }
//        public virtual TraitBehaviour traitBehaviour { get; set; }
//        public virtual void ApplyTrait(CharacterController charController)
//        {

//        }
//    }



//    public class TraitsFactory : MonoSingletonGeneric<TraitsFactory>
//    {
//        public List<TraitsDataSO> allPermTraitsList;
//        public Dictionary<PermanentTraitName, Type> allPermTraits = new Dictionary<PermanentTraitName, Type>();
//        public Dictionary<TempTraitName, Type> allTempTraits;


//        void Start()
//        {
//            InitTempTraits();

//        }

//        public void InitPermTraits()
//        {
//            if (allPermTraits.Count > 0) return;

//            var getPermTraits = Assembly.GetAssembly(typeof(PermTraitBase)).GetTypes()
//                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(PermTraitBase)));

//            foreach (var permTrait in getPermTraits)
//            {

//                var p = Activator.CreateInstance(permTrait) as PermTraitBase;
//                allPermTraits.Add(p.permTraitName, permTrait);
//            }
//        }

//        public void InitTempTraits()
//        {
//            allTempTraits = new Dictionary<TempTraitName, Type>();
//            var getTempTraits = Assembly.GetAssembly(typeof(TempTraitBase)).GetTypes()
//                                 .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(TempTraitBase)));

//            foreach (var tempTrait in getTempTraits)
//            {
//                var t = Activator.CreateInstance(tempTrait) as TempTraitBase;
//                allTempTraits.Add(t.tempTraitName, tempTrait);
//            }
//        }

//        public void TempTraitsFactory(GameObject go, TempTraitName tempTraitName)
//        {

//            if (allTempTraits.ContainsKey(tempTraitName))
//            {
//                Type tempTrait = allTempTraits[tempTraitName];
//                TempTraitBase trait = Activator.CreateInstance(tempTrait) as TempTraitBase;
//                go.AddComponent(trait.GetType());

//            }
//        }

//        public void PermTraitsFactory(GameObject go)
//        {

//            CharacterController charctrl = go.GetComponent<CharacterController>();
//            foreach (TraitsDataSO charptraits in allPermTraitsList)   // list of sos
//            {
//                // Debug.Log("permanet trait found");
//                if (charctrl.charModel.racetype == charptraits.racetype)
//                {
//                    foreach (PermTraitsINChar traitvalue in charptraits.PermTraitsINCharList)
//                    {
//                        AddPermTrait(traitvalue.permanentTraitName, go);
//                    }
//                }
//            }

//            ApplyPermTraits(go);
//        }

//        public void AddPermTrait(PermanentTraitName permaTraitName, GameObject go)
//        {

//            if (allPermTraits.ContainsKey(permaTraitName))
//            {

//                Type permaTrait = allPermTraits[permaTraitName];
//                PermTraitBase trait = Activator.CreateInstance(permaTrait) as PermTraitBase;
//                go.AddComponent(trait.GetType());


//            }

//        }

//        public void ApplyPermTraits(GameObject go)
//        {
//            CharacterController charController = go?.GetComponent<CharacterController>();

//            PermTraitBase[] permaTraits = go.GetComponents<PermTraitBase>();

//            foreach (PermTraitBase p in permaTraits)
//            {
//                p.ApplyTrait(charController);
//            }

//        }

//    }
//}

