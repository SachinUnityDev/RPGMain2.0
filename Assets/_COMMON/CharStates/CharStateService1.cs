using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CharStateService1 : MonoSingletonGeneric<CharStateService1>
    {        
       public AllCharStateSO allCharStateSO;
       public List<CharStateBase1> allCharStateBase = new List<CharStateBase1>();  

    }
}