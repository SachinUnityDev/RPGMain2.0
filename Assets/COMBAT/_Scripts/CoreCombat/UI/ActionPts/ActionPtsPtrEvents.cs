using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
    public class ActionPtsPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        ActionPtsView actionPtsView;

        [SerializeField] TextMeshProUGUI actionsPtsTxt; 
        public void Init(ActionPtsView actionPtsView)
        {
            this.actionPtsView = actionPtsView;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           int actionPts= actionPtsView.actionPts;
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