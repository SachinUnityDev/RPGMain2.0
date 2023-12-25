﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq; 

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

        public List<CharController> allEnemiesInCombat = new List<CharController>();
        public List<CharController> allAlliesInCombat = new List<CharController>();

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

        public int currentRound = 1;
        public int currentTurn = 0;

        public bool isAccChecked = false;
        public bool isFocusChecked = false;

        [Header("Result")]
        public bool isVictory;
        [Header("Enemy Pack")]
        public EnemyPacksSO currEnemyPackSO; 

        void OnEnable()
        {            
          //  CombatEventService.Instance.OnEOR1 += EORActions;
            CombatEventService.Instance.OnCharOnTurnSet += SetAllCurrCharValues;
            SkillService.Instance.SkillSelect
                            += (CharNames _charName, SkillNames _skillName)
                            =>combatState = CombatState.INCombat_InSkillSelected;
            
           
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
        public void ToggleColliders(bool turnOn)
        {
            foreach (CharController charCtrl in CharService.Instance.allCharInCombat)
            {
                if(currCharOnTurn.charModel.charID != charCtrl.charModel.charID && !turnOn)
                     charCtrl.GetComponent<BoxCollider2D>().enabled = turnOn;
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

    }
}

