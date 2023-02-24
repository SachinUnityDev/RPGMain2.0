using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Common
{
    public class CharStateFactory1 : MonoBehaviour
    {

        Dictionary<CharStateName, Type> allCharStates;
        [SerializeField] int charStateCount = 0;
        void Start()
        {
            allCharStates = new Dictionary<CharStateName, Type>();
            InitCharState();//  to test all temp traits in use
        }

        public void InitCharState()
        {

            if (allCharStates.Count > 0) return;
            var getCharStates = Assembly.GetAssembly(typeof(CharStateBase1)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(CharStateBase1)));

            foreach (var charState in getCharStates)
            {
                var p = Activator.CreateInstance(charState) as CharStateBase1;

                allCharStates.Add(p.CharStateName, charState);
                charStateCount++;
            }
        }

        public CharStateBase1 GetNewCharStateBase(CharStateName charStateName)
        {
            foreach (var charState in allCharStates)
            {
                if (charState.Key == charStateName)
                {
                    var t = Activator.CreateInstance(charState.Value) as CharStateBase1;
                    return t;
                }
            }
            Debug.Log("temp trait base not found" + charStateName);
            return null;
        }
    }
}