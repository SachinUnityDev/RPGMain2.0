using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

namespace Combat
{
    public class CharStatesView : MonoBehaviour
    {

        public AllCharStateSO allCharStateSO;

        public GameObject posStatesCard;
        public GameObject negStatesCard; 

        private void OnEnable() 
        {
            CharStatesPanelIconsClear();
            CombatEventService.Instance.OnSOTactics += CharStatesPanelIconsClear;
            CombatEventService.Instance.OnSOC += CharStatesPanelIconsClear;
            CharStatesService.Instance.OnCharStateStart += UpdateCharStateChg;
            CombatEventService.Instance.OnCharOnTurnSet += SetCombatStatesDisplay;
            CombatEventService.Instance.OnCharClicked += SetCombatStatesDisplay;
            CombatEventService.Instance.OnCharOnTurnSet += OnCharSet;
            CombatEventService.Instance.OnEOT += OnEOT;
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnSOTactics -= CharStatesPanelIconsClear;
            CombatEventService.Instance.OnSOC -= CharStatesPanelIconsClear;
            CharStatesService.Instance.OnCharStateStart -= UpdateCharStateChg;
            CombatEventService.Instance.OnCharOnTurnSet -= SetCombatStatesDisplay;
            CombatEventService.Instance.OnCharOnTurnSet -= OnCharSet;
            CombatEventService.Instance.OnEOT -= OnEOT;
        }
      
        void OnEOT()
        {
            posStatesCard.SetActive(false); 
            negStatesCard.SetActive(false);
        }
        void OnCharSet(CharController charController)
        {
            gameObject.SetActive(true);

        }
        public void InitCharStatesView()
        {
            CharStatesPanelIconsClear();
        }
        public GameObject GetCharStateCard(CharStateBehavior statesBehaviour)
        {
            if(statesBehaviour == CharStateBehavior.Positive)
            {
                return posStatesCard;
            }
            if(statesBehaviour == CharStateBehavior.Negative)
            {
                return negStatesCard;   
            }
            Debug.Log(" CharStates card Not found" + statesBehaviour.ToString());   
            return null; 
        }


        void UpdateCharStateChg(CharStateModData charStateModData)
        {
            CharController charController = CombatService.Instance.currCharClicked;

            SetCombatStatesDisplay(charController);
        }
        public void SetCombatStatesDisplay(CharController charController)
        {
            // get reference to Icon SO 
            int k = 0;

            List<CharStatesBase> allCharStateBases = charController.charStateController.allCharBases;
            CharStatesPanelIconsClear();

            for (int i = 0; i < allCharStateBases.Count; i++)
            {
                CharStateName charStateName = allCharStateBases[i].charStateName; 
                CharStateSO1 stateSO = allCharStateSO.GetCharStateSO(charStateName);
                
                CharStateBehavior charStateType = stateSO.charStateBehavior;
                k = (charStateType == CharStateBehavior.Positive) ? 0 : 1;

                // Debug.Log("CHAR STATES " + data.charStateName);
                if (i < 4)// level 1
                {
                    Transform ImgTrans = transform.GetChild(k).GetChild(0).GetChild(i);
                    ImgTrans.gameObject.SetActive(true);
                    ImgTrans.GetChild(0).GetComponent<Image>().sprite = stateSO.iconSprite;
                    ImgTrans.GetComponent<CharStatePanelEvents>().InitCard(stateSO, allCharStateBases[i], this,0);

                }
                if (i >= 4 && i < 7)
                {
                    Transform ImgTrans = transform.GetChild(k).GetChild(0).GetChild(i - 4);
                    ImgTrans.gameObject.SetActive(true);
                    ImgTrans.GetChild(0).GetComponent<Image>().sprite = stateSO.iconSprite;
                    ImgTrans.GetComponent<CharStatePanelEvents>().InitCard(stateSO, allCharStateBases[i], this,1);
                }
                if (i >= 7 && i < 9)
                {
                    Transform ImgTrans = transform.GetChild(k).GetChild(0).GetChild(i - 7);
                    ImgTrans.gameObject.SetActive(true);
                    ImgTrans.GetChild(0).GetComponent<Image>().sprite = stateSO.iconSprite;
                    ImgTrans.GetComponent<CharStatePanelEvents>().InitCard(stateSO, allCharStateBases[i], this,2);

                }

            }
        }
        void CharStatesPanelIconsClear()
        {
            for (int j = 0; j < 2; j++) // first two are pos negative icons
            {
                Transform posNegTransform = transform.GetChild(j);
                for (int l = 0; l < posNegTransform.childCount; l++)
                {
                    Transform lvls = posNegTransform.GetChild(l);
                    for (int m = 0; m < lvls.childCount; m++)
                    {
                        Transform stateIcons = lvls.GetChild(m);
                        stateIcons.gameObject.SetActive(false);
                    }
                }
            }
        }



    }
}