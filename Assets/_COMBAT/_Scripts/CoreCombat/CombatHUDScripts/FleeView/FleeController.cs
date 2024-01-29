using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class FleeController : MonoBehaviour
    {
        CharController charController;
        [SerializeField] bool isChkDone = false; 
        [SerializeField] bool combatFleeResult = false;

        [Header(" On Quest flee")]
        int currDayCount = 0;
        int netDaysFled = 0; 
        void Start()
        {
          
            CombatEventService.Instance.OnSOC += OnSOC;
            charController = GetComponent<CharController>();

        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnSOC -= OnSOC;
        }
        public bool OnCombatFleeChk()
        {     
            if (isChkDone) return combatFleeResult;

            charController = GetComponent<CharController>();
            FleeBehaviour fleebehaviour =  charController.charModel.fleeBehaviour;

            FleeChancesSO fleeChancesSO = CharService.Instance.fleeChancesSO;
            combatFleeResult = fleeChancesSO.GetFleeChance(fleebehaviour);
            isChkDone = true; 
            return combatFleeResult;
        }
        #region ON QUEST FLED 

        public void InitOnDayFledQ(int netDaysFled)
        {
            currDayCount = 0;
            this.netDaysFled = netDaysFled;
            CalendarService.Instance.OnStartOfCalDay += OnFledDayTick;
            charController.charModel.availOfChar = AvailOfChar.UnAvailable_WhereAboutsUnKnown;
        }
        void OnFledDayTick(int dayInGame)
        {
            if (netDaysFled <= currDayCount)
            { // available at base loc and Unlocked in roster
                charController.charModel.availOfChar = AvailOfChar.Available;
                charController.charModel.stateOfChar = StateOfChar.UnLocked; 
                charController.charModel.currCharLoc = charController.charModel.baseCharLoc;
                CalendarService.Instance.OnStartOfCalDay -= OnFledDayTick;
            }
            else
            {
                currDayCount++; 
            }
        }


        #endregion
        void OnSOC()
        {
            isChkDone= false;
        }
        
    }
}