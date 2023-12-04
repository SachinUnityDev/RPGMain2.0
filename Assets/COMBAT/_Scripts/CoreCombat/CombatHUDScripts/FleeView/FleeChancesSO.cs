using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FleeChancesSO", menuName = "Character Service/FleeChancesSO")]
public class FleeChancesSO : ScriptableObject
{
    public List<FleeBehaviourNChance> allFleeBehaveNChances = new List<FleeBehaviourNChance>();

    public bool GetFleeChance(FleeBehaviour fleeBehaviour)
    {
        int index = allFleeBehaveNChances.FindIndex(t=>t.fleeBehaviour == fleeBehaviour);
        if(index != -1)
        {
            float chance = allFleeBehaveNChances[index].fleeChance;
            bool result = chance.GetChance();
            return result;
        }
        Debug.Log(" Flee chances Not found" + fleeBehaviour);
        return false;
    }
}
[Serializable]
public class FleeBehaviourNChance
{
    public FleeBehaviour fleeBehaviour;
    public float fleeChance;
}
