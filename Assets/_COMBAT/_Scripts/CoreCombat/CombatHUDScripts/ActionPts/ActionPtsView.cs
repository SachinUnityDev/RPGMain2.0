using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;
namespace Combat
{
    public class ActionPtsView : MonoBehaviour
    {
        [Header(" TBR")]
        [SerializeField] ActionPtsPtrEvents actionPtsPtrEvents;
        [SerializeField] GameObject endTurnBtn; 
        [SerializeField] int actionPts;
      //  [SerializeField] Transform actionPtsDOT; 


        private void OnEnable()  // view subscriptions not to be disabled
        {
            // can' t write on disable as it happens often
            //CombatEventService.Instance.OnCharOnTurnSet -= ShowActionPtsDsply;
            //CombatEventService.Instance.OnEOT -= HideActionPtsDsply;
            HideActionPtsDOTs();
    
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            if (arg1.name == "COMBAT")
            {
                actionPtsPtrEvents = FindObjectOfType<ActionPtsPtrEvents>(true);
                CombatEventService.Instance.OnCharOnTurnSet += ShowActionPtsDsply;
                CombatEventService.Instance.OnEOT += HideActionPtsDsply;

                CombatEventService.Instance.OnSOTactics += ShowEndTurnBtn;
                CombatEventService.Instance.OnSOR1 += OnRdStart_ShowEndTurn;
            }
            else
            {
                CombatEventService.Instance.OnSOR1 -= OnRdStart_ShowEndTurn;
                CombatEventService.Instance.OnCharOnTurnSet -= ShowActionPtsDsply;
                CombatEventService.Instance.OnEOT -= HideActionPtsDsply;
            }
        }
        private void OnDestroy()
        {
            // not in OnDisable as it happens often at EOT
            CombatEventService.Instance.OnSOR1 -= OnRdStart_ShowEndTurn;
            CombatEventService.Instance.OnCharOnTurnSet -= ShowActionPtsDsply;
            CombatEventService.Instance.OnEOT -= HideActionPtsDsply;
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            CombatEventService.Instance.OnSOTactics -= ShowEndTurnBtn;
        }

        private void OnDisable()
        {
//            CombatEventService.Instance.OnSOTactics -= ShowEndTurnBtn;
        }
    
        void OnRdStart_ShowEndTurn(int rd)
        {
            ShowEndTurnBtn();
        }

        void ShowEndTurnBtn()
        {
            endTurnBtn.SetActive(true);
        }

        void HideActionPtsDOTs()
        {
          //  actionPtsPtrEvents.Init(this);
            actionPtsPtrEvents = FindObjectOfType<ActionPtsPtrEvents>(true);  
            foreach (Transform child in actionPtsPtrEvents.transform)
            {
                child.gameObject.SetActive(false); 
            }
        }

       public void ShowActionPtsDsply(CharController charController)
       {
            Debug.Log("all action pts display" +charController.charModel.charName); 
            if(charController.charModel.charMode == CharMode.Ally)
            {
                actionPts = charController.GetComponent<CombatController>().GetAP();
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
            Debug.Log("Action PTS UPDATED>>>>>" + actionPts); 
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
            if (gameObject)
                gameObject.SetActive(false);
            else
                Debug.Log(" AP Game Object is null"); 
        }
    }
}