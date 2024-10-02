using Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
    public class ActionPtsPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
       [SerializeField] ActionPtsView actionPtsView;

        [SerializeField] TextMeshProUGUI actionsPtsTxt; 
        public void Init(ActionPtsView actionPtsView)
        {
            this.actionPtsView = actionPtsView;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (CombatService.Instance.combatState == CombatState.INTactics)
                return;
            CharController charController = CombatService.Instance.currCharOnTurn;

            int actionPts = charController.combatController.GetAP(); 
            if(actionPts > 0)
            {
                actionsPtsTxt.gameObject.SetActive(true); 
                actionsPtsTxt.text = $"{actionPts} AP";
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            actionsPtsTxt.gameObject.SetActive(false);
        }
    }
}