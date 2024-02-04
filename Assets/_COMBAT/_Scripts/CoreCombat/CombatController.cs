using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;
using System;

namespace Combat
{   
    

    public class CombatController : MonoBehaviour
    {

        int MAX_VAL_FOR_ACTION_PTS = 1; 

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

        public void CnvrtRmgAP2StmGain()
        {
            if(actionPts > 0)
            {
                charController.ChangeStat(CauseType.ActionsPts, (int)0, charController.charModel.charID
                                                               , StatName.stamina, +actionPts * 2);
                actionPts = 0; 
            }            
        }

        public void SetActionPts()  // SOT ONLY 
        {
            actionPts= 0;
            MoraleChk(charController);
            if (charController.charModel.charMode == CharMode.Ally)
                actionPts ++;
            else
                ++actionPts;
            if (actionPts > MAX_VAL_FOR_ACTION_PTS)
                    actionPts = MAX_VAL_FOR_ACTION_PTS;
            if(actionPts <= 0)
                    actionPts= 0;

          //  Canvas canvas = FindObjectOfType<Canvas>();
          //  SkillView skillView = canvas.GetComponentInChildren<SkillView>(); 
          ////  skillView.SetSkillsPanel(charController);

            Debug.Log("ACTION PTS SET FOR: " + charController.charModel.charName +charController.charModel.charID + "action :"+ actionPts);
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
          //  Debug.Log(" ACTION PTS SUBTRACT" + skillModel.charID + skillModel.skillName); 
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



