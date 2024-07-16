using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Common
{

    [CreateAssetMenu(fileName = "NotifySO", menuName = "Common/NotifySO")]

    public class NotifySO : ScriptableObject
    {
        public NotifyName NotifyName;
        [TextArea(5, 10)]
        public string notifyStr = "";
        public bool isDontShowAgainTicked = false;
    }



    public enum NotifyName
    {
        None, 
        RosterLock, 
        RosterDisband, 
        UnSocket,
        Enchant, 
        LootTaken, 
        BountyQUnLock,
        CombatEnd, 
        ClearProfile, 


    }
}