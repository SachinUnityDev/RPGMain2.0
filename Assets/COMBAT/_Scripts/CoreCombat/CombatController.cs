using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

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

        void Start()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR1 += ResetValues;
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnEOR1 -= ResetValues;
        }
        
  
        void ResetValues(int roundNo)
        {
            prevTurn = -5; 
        }
    
        public void Init()
        {

        }
        public void SetActionPts()
        {       
            if (CombatService.Instance.currCharOnTurn.charModel.charID != this.charController.charModel.charID)
                return;
            MoraleChk(charController);
            if (prevTurn == CombatService.Instance.currentTurn)
                return;
            prevTurn = CombatService.Instance.currentTurn;

            // MORALE CHECK
            ++actionPts; 
            if (actionPts > MAX_VAL_FOR_ACTION_PTS)
                    actionPts = MAX_VAL_FOR_ACTION_PTS;
            Debug.Log(" charName" + charController.charModel.charName + "action"+ actionPts);
            if (charController.charModel.charMode == CharMode.Enemy)
            {
                if (actionPts <= 0)
                {
                    SkillService.Instance.Move2Nextturn();
                }
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
                        if (charController.charModel.orgCharMode == CharMode.Ally)
                            --actionPts;
                    }

                }
                else if (moraleData.currValue > 6)
                {
                    if (chance.GetChance())
                    {
                        CombatEventService.Instance.On_MoraleCheck(charController, true);
                        if (charController.charModel.orgCharMode == CharMode.Ally)
                            ++actionPts;
                    }
                }            
        }

        public void UpdateActionPts(SkillModel skillModel, CharMode charMode)
        {
            if (skillModel.skillType == SkillTypeCombat.Retaliate)
                return;
            --actionPts;
            if (charMode != CharMode.Ally) return; // no action pts view for the enemy
            canvas = FindObjectOfType<Canvas>();
            actionPtsView = canvas.GetComponentInChildren<ActionPtsView>(true);
      
            actionPtsView.UpDateActionsPtsView(actionPts);
        }
        public bool IfSingleInRow(GameObject _charGO)
        {
            return false;
        }

        public bool IsLastManInHeroes(CharController _charSO)
        {
            return false;
        }
        public bool IfRaceInTeam(CultureType _cultType)
        {
            return false;

        }
    }


}
//AttribData haste_AttribData = currCharOnTurn.GetAttrib(AttribName.haste);
//int hasteBonus = (int)haste_AttribData.currValue / 6; 
//actionPts = 1+hasteBonus;