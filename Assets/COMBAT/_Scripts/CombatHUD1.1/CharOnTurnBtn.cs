using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;

namespace Combat
{
    public class CharOnTurnBtn : MonoBehaviour
    {
   
        private void Start()
        {
            CombatEventService.Instance.OnSOC += () => UIControlServiceCombat.Instance.TurnOnOff(gameObject, true);
            CombatEventService.Instance.OnSOTactics += () => UIControlServiceCombat.Instance.TurnOnOff(gameObject, false);
            gameObject.GetComponent<Button>().onClick
                            .AddListener(OnClickedONCharInNormalCombatState);
            CombatEventService.Instance.OnCharClicked += Check4DiffChar;
        }

        void OnClickedONCharInNormalCombatState()
        {
            if (CombatService.Instance.combatState == CombatState.INCombat_normal)
             CombatEventService.Instance.On_CharClicked(CombatService.Instance.currCharOnTurn.gameObject);
        }

        public void Check4DiffChar(CharController charController)
        {
            if (CombatService.Instance.combatState == CombatState.INCombat_normal)
            {
                if (charController.gameObject == null)
                {
                  //  UIControlService.Instance.TurnOnOff(gameObject, false); return; 
                }
                if (charController.gameObject != CombatService.Instance.currCharOnTurn.gameObject)
                {
                    UIControlServiceCombat.Instance.TurnOnOff(gameObject, true);
                }
                else
                    UIControlServiceCombat.Instance.TurnOnOff(gameObject, false);
            }
        }
    }

}
