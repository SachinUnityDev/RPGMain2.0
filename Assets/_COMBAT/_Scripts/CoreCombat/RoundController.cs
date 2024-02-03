using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
using System;

namespace Combat
{
    public class RoundController : MonoBehaviour
    {
     
        [Header("global var")]
        [SerializeField] int index;
        [SerializeField] int newIndex = 0;

        [SerializeField] int charCount = 0; 
        public CharMode TurnMode;
        [SerializeField] float prevVal;
        [SerializeField] AttribName prevAttribName; 

        public List<CharController> allyTurnOrder = new List<CharController>();
        public List<CharController> enemyTurnOrder = new List<CharController>();
        public List<CharController> charTurnOrder = new List<CharController>();
        
        
        List<AttribName> StatOrder = new List<AttribName>() { AttribName.haste, AttribName.focus, AttribName.morale, AttribName.luck };

        private void Start()
        {
            index = -1;          
           // CombatEventService.Instance.OnSOT += SetNextCharOnTurn;
          //  CombatEventService.Instance.OnSOR1 += OnRoundStart;

        }
        private void OnDisable()
        {
        //    CombatEventService.Instance.OnSOT -= SetNextCharOnTurn;
         //   CombatEventService.Instance.OnSOR1 -= OnRoundStart;

        }
        public void SetSameCharOnTurn()
        {
            Debug.Log("SAME CHAR ON TURN SET" + index);
            if (CombatService.Instance.combatState != CombatState.INCombat_normal)
            {
                return;
            }
            CombatEventService.Instance.On_CharOnTurnSet();
        }

        public void SetNextCharOnTurn()
        {
            if (CombatService.Instance.combatState == CombatState.INCombat_normal ||
                    CombatService.Instance.combatState == CombatState.INCombat_InSkillSelected)
            {
                index++;
                Debug.Log("CHAR INCR" + index); 
                charCount = charTurnOrder.Count;
                if (index < charCount && index > -1)
                {                    
                    CombatService.Instance.currCharOnTurn = charTurnOrder[index];                  
                    SetAP(charTurnOrder[index]);
                    CombatService.Instance.currentTurn = index;
                    CombatEventService.Instance.On_CharOnTurnSet();
                }
                else // next round 
                {
                    index = -1;
                    CombatEventService.Instance.Move2NextRds();
                    return; 
                }               
            }
        }
        void SetAP(CharController charController)
        {
            CombatController combatController = charController.GetComponent<CombatController>();
            combatController.SetActionPts(); 
        }
       public void OnRoundStart(int roundNo)
       {
            Debug.Log("Round Start Triggerd" + roundNo); 
            SetTurnOrder();
       }

        void UpdateCharList()
        {
            charCount= CharService.Instance.allCharInCombat.Count;
            charTurnOrder = CharService.Instance.allCharInCombat.Where(t => t.charModel.stateOfChar == StateOfChar.UnLocked).ToList();

            allyTurnOrder = charTurnOrder.Where(t => t.charModel.charMode == CharMode.Ally).ToList();
            enemyTurnOrder = charTurnOrder.Where(t => t.charModel.charMode == CharMode.Enemy).ToList();
        }
        public void ReorderAfterCharDeathOnEOT(CharController charController)
        {   
            CharController currCharOnTurn = CombatService.Instance.currCharOnTurn; 
         
            UpdateCharList(); // update all three list charturnOrder, AllyTurnOrder and EnemyTurnOrder
            
            // if char who is on turn dies => ..end the turn and start the next char in seq
            // 


            newIndex = charTurnOrder
                .FindIndex(t => t.charModel.charID == currCharOnTurn.charModel.charID);
            if(newIndex != -1)
            {
                index = newIndex;                
            }   
            else // char on turn died
            {       
                SetNextCharOnTurn();
            }
        }
        public void SetTurnOrder()
        {
            UpdateCharList();
            OrderByRecursion2(AttribName.haste);
        }

        public void OrderByRecursion2(AttribName _statName)
        {
            List<CharController> newOrder = new List<CharController>();
            int ran = UnityEngine.Random.Range(0, charTurnOrder.Count);
            newOrder = charTurnOrder
                        .OrderBy(i => Guid.NewGuid())// shuffle
                        .OrderByDescending(x => x.GetAttrib(AttribName.haste).currValue)
                        .ThenByDescending(t => t.GetAttrib(AttribName.morale).currValue)
                        .ThenByDescending(t => t.GetAttrib(AttribName.focus).currValue)
                        .ThenByDescending(t => t.GetAttrib(AttribName.luck).currValue)
                        .ToList();
            charTurnOrder = newOrder;
        }
        
    }

}
































//void OrderBYRecursion(StatsName _statName)
//{  
//    // check its state .. main list 

//    allyTurnOrder = CharacterService.Instance.allyInPlayControllers.
//                        OrderByDescending(x => x.GetStat(_statName).currValue).ToList();
//    enemyTurnOrder = CharacterService.Instance.enemyInPlayControllers.
//                        OrderByDescending(x => x.GetStat(_statName).currValue).ToList();

//    if (CheckForSameStat(_statName, allyTurnOrder))
//    {
//        Debug.Log("Inside loop" + _statName); 
//        int index = StatOrder.FindIndex(t => t == _statName);
//        if (index +1 >= StatOrder.Count) return;
//            OrderBYRecursion(StatOrder[index + 1]); 
//    }
//    if (CheckForSameStat(_statName, enemyTurnOrder))
//    {
//        int index = StatOrder.FindIndex(t => t == _statName);
//        if (index + 1 >= StatOrder.Count) return;
//        OrderBYRecursion(StatOrder[index + 1]);
//    }

//}

//bool CheckForSameStat(StatsName _statname, List<CharController> _orderedList)
//{
//    for (int i = 0; i < _orderedList.Count-1; i++)  // extra minus 1 as i+1 is tested 
//    {
//        if (_orderedList[i].GetStat(_statname).currValue == _orderedList[i + 1].GetStat(_statname).currValue)
//        {
//            return true; 
//        } 
//    }
//    return false; 
//}
//SetTurnOrder();
//if (index == (charCount - 1))
//{
//    index = -1;
//    gameObject.GetComponent<CombatHUDView>().SetDefaultTurnOrder();
//    // gameObject.GetComponent<TopPortraitsController>().BuildCharPosData();
//    SetCharOnTurn();
//}
//else
//    index--;


//public void OnRoundStart()
//{
//    //UpdateAllyEnemyList();
//    //SetTurnOrder();            
//    //    index = 0;
//    //Debug.Log("Round incr" + index);
//    //CombatService.Instance.currCharOnTurn = char[0];         
//}

//public void IncrementTurnOrder()
//{
//    index++;
//    Debug.Log("Index incr" + index); 

//    // current turn value to range form 1to 6

//}
// 
//private void Update()
//{
//    if (Input.GetKeyDown(KeyCode.B))
//    {

//        allyTurnOrder.RemoveAt(0);
//    }
//}




//if (index < allyCount)
//{
//    CombatService.Instance.currCharOnTurn = allyTurnOrder[index];
//    CombatEventService.Instance.On_CharOnTurnSet(CombatService.Instance.currCharOnTurn);
//}
//else if (index >= allyCount && index < allyCount + enemyCount)
//{
//    CombatService.Instance.currCharOnTurn = enemyTurnOrder[index - allyCount];
//    CombatEventService.Instance.On_CharOnTurnSet(CombatService.Instance.currCharOnTurn);
//}