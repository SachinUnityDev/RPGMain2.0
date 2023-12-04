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
        void Start()
        {
          
            CombatEventService.Instance.OnSOC += OnSOC;
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

        void OnSOC()
        {
            isChkDone= false;
        }
        
    }
}