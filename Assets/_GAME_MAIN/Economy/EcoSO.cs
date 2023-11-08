using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Common;


[CreateAssetMenu(fileName = "EcoSO", menuName = "EcoNBankServices/EcoSO")]

public class EcoSO : ScriptableObject
{
        public Currency moneyInStash;
        public Currency moneyInInv;
        public Currency moneyNet;

        public List<NPCMoneyData> allNPCMoneyData = new List<NPCMoneyData>();
}
