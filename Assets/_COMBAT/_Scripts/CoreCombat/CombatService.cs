﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
using Quest;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

namespace Combat
{
    public class CombatService : MonoSingletonGeneric<CombatService>
    {
        public CombatState combatState;

        [Header(" Combat End View TBR")]
        public CombatEndView combatEndView;

        [Header("Enemy Pack")]
        public AllEnemyPackSO allEnemyPackSO;
        public EnemyPackController enemyPackController; 
        public EnemypackFactory enemyPackFactory;

        public CharController currCharOnTurn;
        public CharController currCharClicked;
        public CharController currCharHovered;

        public CharController currTargetClicked;
       //  public CharController targetController;
        public List<DynamicPosData> mainTargetDynas = new List<DynamicPosData>();
        public List<DynamicPosData> colTargetDynas = new List<DynamicPosData>(); 

        public EnemyPackName currEnemyPack; 

        public DeathAnimView deathAnimView;
        public RoundController roundController;
        public CombatHUDView combatHUDView; 

       // public int currentRound = 1;
        public int currentTurn = 0;

        public bool isAccChecked = false;
        public bool isFocusChecked = false;

        [Header("Enemy Pack")]
        public EnemyPacksSO currEnemyPackSO;

        [Header("Who drew the first blood")]
        public bool IsFirstBloodChkDone;

        [Header("Combat Result")]
        public Result combatResult; 

        void OnEnable()
        {            
          //  CombatEventService.Instance.OnEOR1 += EORActions;
            CombatEventService.Instance.OnCharOnTurnSet += SetAllCurrCharValues;
          
            SkillService.Instance.SkillSelect
                            += (CharNames _charName, SkillNames _skillName)
                            =>combatState = CombatState.INCombat_InSkillSelected;
            CharService.Instance.OnCharDeath += OnCharDeathCombatChk; 
           
            deathAnimView = GetComponent<DeathAnimView>();
            roundController = GetComponent<RoundController>();
            combatHUDView = gameObject.GetComponent<CombatHUDView>();
            // ENEMY PACK 

            enemyPackController= GetComponent<EnemyPackController>();   
            enemyPackFactory = GetComponent<EnemypackFactory>();

            // Set Abbas as main Char
            // currCharOnTurn = CharService.Instance.allCharInCombat[0]; 
           
            SetAllCurrCharValues(currCharOnTurn);    
        }
        private void OnDisable()
        {  
            CombatEventService.Instance.OnEOR1 -= EORActions;
            CombatEventService.Instance.OnCharOnTurnSet -= SetAllCurrCharValues;
          

            CharService.Instance.OnCharDeath -= OnCharDeathCombatChk;
            SkillService.Instance.SkillSelect
                                       -= (CharNames _charName, SkillNames _skillName)
                                       => combatState = CombatState.INCombat_InSkillSelected; 
        }
        public void SetAllCurrCharValues(CharController charController)  // Inits 
        {      
            currCharClicked = currCharOnTurn;
            currCharHovered = currCharOnTurn;
            combatHUDView.UpdateTurnBtn(charController); 
        }

        public void GetAllyInCombat()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                if(!CharService.Instance.allCharInCombat.Any(t=>t.charModel.charID == charCtrl.charModel.charID))
                {
                    if(charCtrl.charModel.stateOfChar != StateOfChar.Fled)
                        CharService.Instance.allCharInCombat.Add(charCtrl);                    
                }
            }
             GridService.Instance.SetAllyPreTactics();
        }

        public void SetEnemyInCombat(EnemyPackName enemyPack)
        {
            currEnemyPack = enemyPack;
            enemyPackController.InitEnemyPackController(allEnemyPackSO);
            currEnemyPackSO = allEnemyPackSO.GetEnemyPackSO(enemyPack);            
            GridService.Instance.SetEnemy(currEnemyPackSO);
        }
        public void InitOnSOC()
        {
             IsFirstBloodChkDone = false;
        }

        public void ToggleColliders(bool turnOn)
        {
            foreach (CharController charCtrl in CharService.Instance.allCharInCombat)
            {
                if (currCharOnTurn.charModel.charID != charCtrl.charModel.charID && !turnOn)
                {
                    Collider col = charCtrl.GetComponentInChildren<Collider>(); 
                    if(col != null)
                        col.enabled = turnOn;                    
                }   
            }
        }
        public void AddCombatControllers()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharInCombat)
            {
               if (charCtrl.damageController == null)
                    charCtrl.damageController=  charCtrl.gameObject.AddComponent<DamageController>();

                if (charCtrl.gameObject.GetComponent<CombatController>() == null)
                   charCtrl.combatController = charCtrl.gameObject.AddComponent<CombatController>();

                if (charCtrl.strikeController == null)
                    charCtrl.strikeController = charCtrl.gameObject.AddComponent<StrikeController>();
                charCtrl.damageController.Init(); 
                charCtrl.strikeController.Init();
                charCtrl.combatController.Init();
            }
        }
        public void EORActions(int roundNo)
        {
            //Debug.Log("STAMINA REGEN ");
            //CharacterService.Instance.CharsInPlayControllers.ForEach(t => t.RegenStamina()); 

        }

        public void EOCActions()
        {
            // all fortitude values reset to zero
            // call all combatr controller and change fortitude to zero 
            

        }

        #region COMBAT RESULT 

        public void OnCharDeathCombatChk(CharController charController)
        {
            //"Kill all enemies
            //(If Abbas fled combat you can still win Combat with remaining heroes)"	
            //Combat didnt finish before the round limit	
            //Abbas dies / Manually Flee / No Hero left on battlefield (die or flee)  // Combat DEFEAT on all flee
            CheckCombatResultWithWait(charController);
        }
       

        void CheckCombatResultWithWait(CharController charController)
        {
            if (charController.charModel.charMode == CharMode.Enemy)
            {// chk if all enemies have died // victory case here
                if (!CharService.Instance.allCharInCombat.Any(t => t.charModel.charMode == CharMode.Enemy))
                {
                    OnCombatResult(Result.Victory, CombatEndCondition.Victory_AllEnemiesDied);
                }
            }
            else if (charController.charModel.charName == CharNames.Abbas)
            { // combat lost .. Abbas Died
                OnCombatResult(Result.Defeat, CombatEndCondition.Defeat_AbbasDied);
            }
            else // ally other than abbas Died
            {
                if (!CharService.Instance.allCharInCombat.Any(t => t.charModel.charMode == CharMode.Ally))
                {
                    OnCombatResult(Result.Defeat, CombatEndCondition.Defeat_AllInCombatDiedNFled);
                }
            }
        }
       

        public void OnCombatResult(Result combatResult, CombatEndCondition combatEndCondition)
        {
            // chk combat state here

          Debug.Log(" ComBAT STATE " + combatState);    
            CombatEventService.Instance.On_EOC(combatResult);
  
            this.combatResult = combatResult;
            EnemyPackBase enemyPackBase = enemyPackController.GetEnemyPackBase(currEnemyPack); 

            switch (combatResult)
            {
                case Result.None:
                    break;
                case Result.Victory: // loot and Exp
                    enemyPackBase.EnemyPackShowLoot(); 
                    break;
                case Result.Draw: // on max Round Limit reached 
                    combatEndView.InitCombatEndView();
                    break;
                case Result.Defeat: // On Abbas Dead, All companions in Combat Dead,   
                    combatEndView.InitCombatEndView();
                    break;
            }
        }
        #endregion

        public float GetSharedExp()
        {
            
            int sharedExp = currEnemyPackSO.sharedExp;
            int allyExceptDeadNFledCount = 0;
            List<CharController> allAllyInclDeadNFled 
                                         = CharService.Instance.allCharsInPartyLocked
                                         .Where(t => t.charModel.orgCharMode == CharMode.Ally).ToList();
            
            foreach (CharController charCtrl in allAllyInclDeadNFled)
            {
                if (charCtrl.charModel.stateOfChar == StateOfChar.UnLocked) // means !not dead and fled state 
                {
                    allyExceptDeadNFledCount++;
                }
            }
            if (allyExceptDeadNFledCount > 0)
            {
                switch (combatResult)
                {
                    case Result.None:
                        return 0;                         
                    case Result.Victory:
                       return sharedExp / allyExceptDeadNFledCount;                       
                    case Result.Draw:
                        return (sharedExp /allyExceptDeadNFledCount)*0.6f;
                    case Result.Defeat:
                        return 0;                        
                    default:
                        return 0;                        
                }
            }              
           return 0;
        }
       
        public int GetHighMeritExp()
        {
            int manualExp = currEnemyPackSO.sharedExp / 6; 
            return manualExp;   
        }
    }

    public enum CombatEndCondition
    {
        None,
        Defeat_AllInCombatDiedNFled,
        Defeat_AbbasDied, 
        Defeat_AbbasFledQ, 
        Victory_AllEnemiesDied, 
        Draw_MaxRdsLmt, 
    }

}

