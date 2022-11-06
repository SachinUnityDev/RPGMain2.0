using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;


namespace Common
{
    public class CharStatesFactory : MonoBehaviour
    {
        public Dictionary<CharStateName, Type> allCharStates;
        void Start()
        {
            allCharStates = new Dictionary<CharStateName, Type>();
            InitCharStates();  // ON START OF QUEST 
        }

        public void InitCharStates()
        {

            if (allCharStates.Count > 0) return;

            var getCharStates = Assembly.GetAssembly(typeof(CharStatesBase)).GetTypes()
                                 .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(CharStatesBase)));

            foreach (var charState in getCharStates)
            {            
                var t = Activator.CreateInstance(charState) as CharStatesBase;
                allCharStates.Add(t.charStateName, charState);            
            }       
        }

        public CharStatesBase GetCharState(CharStateName _charStateName)
        {
            foreach (var charState in allCharStates)
            {
                if (charState.Key == _charStateName)
                {
                    var t = Activator.CreateInstance(charState.Value) as CharStatesBase;                    
                    return t;
                }
            }
            Debug.Log("Char States class Not found"); 
            return null;
        }

        //public CharStatesBase AddCharState(GameObject _charGO, CharStateName _charStateName)
        //{
        //    foreach (var charState in allCharStates)
        //    {
        //        if (charState.Key == _charStateName)
        //        {
        //            var t = Activator.CreateInstance(charState.Value) as CharStatesBase;
        //            CharStatesService.Instance.allCharStates.Add(t);
        //            var temp = _charGO.AddComponent(t.GetType());
        //            Debug.Log("Compoent added" + temp.name + "char State" + _charStateName);
        //          //  return temp as CharStatesBase;
        //        }
        //    }
        //    return null;

        //}


    }
}
