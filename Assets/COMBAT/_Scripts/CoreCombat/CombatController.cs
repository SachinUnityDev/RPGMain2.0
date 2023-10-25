using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.SceneManagement;

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
            prevTurn = -1; 
        }
    
        public void Init()
        {

        }
        public int SetActionPts()
        {
            // get haste val 
            CharController currCharOnTurn = CombatService.Instance.currCharOnTurn;
            if (charController.charModel.charID != currCharOnTurn.charModel.charID)
                return 0;
            if (prevTurn == CombatService.Instance.currentTurn)
                return actionPts; 

            prevTurn= CombatService.Instance.currentTurn;
            actionPts= 0;
            AttribData haste_AttribData = currCharOnTurn.GetAttrib(AttribName.haste);
            int hasteBonus = (int)haste_AttribData.currValue / 6; 
            actionPts = 1+hasteBonus;
            Debug.Log(" Action Points" + actionPts); 
            // MORALE CHECK
            AttribData moraleData = CombatService.Instance.currCharOnTurn.GetAttrib(AttribName.morale);
            StatsVsChanceSO statChanceSO =  CharService.Instance.statChanceSO;
            float chance = (float)statChanceSO.GetChanceForStatValue(AttribName.morale, (int)moraleData.currValue);
            if (chance.GetChance())
            {
                if (moraleData.currValue < 6)
                    --actionPts;
                else if (moraleData.currValue > 6)
                    ++actionPts;
            }          
            if (actionPts > MAX_VAL_FOR_ACTION_PTS)
                    actionPts = MAX_VAL_FOR_ACTION_PTS; 

            return actionPts;
        }

        public void UpdateActionPts(SkillModel skillModel)
        {
            if (skillModel.skillType == SkillTypeCombat.Retaliate)
                return; 
            canvas = FindObjectOfType<Canvas>();
            actionPtsView = canvas.GetComponentInChildren<ActionPtsView>(true);
            --actionPts; 
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
