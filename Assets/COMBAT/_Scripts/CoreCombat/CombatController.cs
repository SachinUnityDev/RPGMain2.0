using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;

namespace Combat
{   
    

    public class CombatController : MonoBehaviour
    {

        int MAX_VAL_FOR_ACTION_PTS = 4; 

        public DynamicPosData selectedLoc;
        public DynamicPosData TargetLoc;

        public bool isMoraleChk = false;
        public int actionPts = 0;
        CharController charController;

        [SerializeField] Canvas canvas;
        [SerializeField] ActionPtsView actionPtsView;


        [SerializeField] int prevTurn =-1;

        private void OnEnable()
        {
            charController = GetComponent<CharController>();
        }
        void Start()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR1 += ResetValues;
          //  CombatEventService.Instance.OnSOT += ONSOT; 
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnEOR1 -= ResetValues;
           // CombatEventService.Instance.OnSOT -= ONSOT;
        }

        void ResetValues(int roundNo)
        {
            prevTurn = -5; actionPts= 0;
            
        }
    
        public void Init()
        {

        }



        public void SetActionPts()  // SOT ONLY 
        {
            MoraleChk(charController);
            if (charController.charModel.orgCharMode == CharMode.Ally)
                actionPts +=2;
            else
                ++actionPts;
            if (actionPts > MAX_VAL_FOR_ACTION_PTS)
                    actionPts = MAX_VAL_FOR_ACTION_PTS;
            if(actionPts <= 0)
                    actionPts= 0;

          //  Canvas canvas = FindObjectOfType<Canvas>();
          //  SkillView skillView = canvas.GetComponentInChildren<SkillView>(); 
          ////  skillView.SetSkillsPanel(charController);

            Debug.Log("ACTION PTS SET FOR: " + charController.charModel.charName + "action"+ actionPts);
            if (charController.charModel.charMode == CharMode.Enemy)
            {
                if (actionPts <= 0)
                {
                    SkillService.Instance.Move2Nextturn();
                }
            }
            else
            {
                FillActionPtsView(charController.charModel.charMode); 
            }
        }
        void MoraleChk(CharController charController)
        {
            AttribData moraleData = charController.GetAttrib(AttribName.morale);
            StatsVsChanceSO statChanceSO = CharService.Instance.statChanceSO;
            float chance = (float)statChanceSO.GetChanceForStatValue(AttribName.morale, (int)moraleData.currValue);
            
                if (moraleData.currValue < 6)
                {
                    if (chance.GetChance())
                    {
                        CombatEventService.Instance.On_MoraleCheck(charController, false);
                            --actionPts;
                    }

                }
                else if (moraleData.currValue > 6)
                {
                    if (chance.GetChance())
                    {
                        CombatEventService.Instance.On_MoraleCheck(charController, true);                      
                            ++actionPts;
                    }
                }            
        }

        public void SubtractActionPtOnSkilluse(SkillModel skillModel, CharMode charMode)
        {
            if (skillModel.skillType == SkillTypeCombat.Retaliate)
                return;
            --actionPts;
            FillActionPtsView(charMode); 
        }

        void FillActionPtsView(CharMode charMode)
        {
            if (charMode != CharMode.Ally) return; // no action pts view for the enemy
            canvas = FindObjectOfType<Canvas>();
            actionPtsView = canvas.GetComponentInChildren<ActionPtsView>(true);
            actionPtsView.UpDateActionsPtsView(actionPts);
        }

  
    }


}
//AttribData haste_AttribData = currCharOnTurn.GetAttrib(AttribName.haste);
//int hasteBonus = (int)haste_AttribData.currValue / 6; 
//actionPts = 1+hasteBonus;



//void SetActionOnSOT()
//{
//    if (GameService.Instance.gameModel.gameState == GameState.InCombat)
//    {
//        CombatEventService.Instance.OnCharOnTurnSet -= OnCharSetOnTurn;
//        CombatEventService.Instance.OnCharOnTurnSet += OnCharSetOnTurn;
//    }
//}
//void OnCharSetOnTurn(CharController charController)
//{
//    if (charController.charModel.charID != CombatService.Instance.currCharOnTurn.charModel.charID)
//        return;
//    CombatController combatController = GetComponent<CombatController>();
//    if (combatController != null)
//        combatController.SetActionPts();

//    CombatEventService.Instance.OnCharOnTurnSet -= OnCharSetOnTurn;
//}
