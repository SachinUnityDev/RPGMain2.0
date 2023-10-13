using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common; 
namespace Combat
{
    public class ActionPtsView : MonoBehaviour
    {
        [Header(" TBR")]
        [SerializeField] ActionPtsPtrEvents actionPtsPtrEvents;
        
        public int actionPts; 

        private void Awake()  // view subscriptions not to be disabled
        {
            CombatEventService.Instance.OnCharOnTurnSet -= ShowActionPtsDsply;
            CombatEventService.Instance.OnEOT -= HideActionPtsDsply;
            CombatEventService.Instance.OnCharOnTurnSet += ShowActionPtsDsply;
            CombatEventService.Instance.OnEOT += HideActionPtsDsply;
        }
 

       public void ShowActionPtsDsply(CharController charController)
       {    
            if(charController.charModel.charMode == CharMode.Ally)
            {
                actionPts = charController.GetComponent<CombatController>().actionPts;
                gameObject.SetActive(true);

                UpDateActionsPtsView(actionPts); 
            }
            else
            {
                HideActionPtsDsply();
            }
        }
        
        public void UpDateActionsPtsView(int actionPts)
        {
            actionPtsPtrEvents.Init(this); 
            for (int i = 0; i < actionPtsPtrEvents.transform.childCount; i++)
            {
                if (i < actionPts)
                {
                    actionPtsPtrEvents.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    actionPtsPtrEvents.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        void HideActionPtsDsply()
        {
            gameObject.SetActive(false);    
        }
    }
}