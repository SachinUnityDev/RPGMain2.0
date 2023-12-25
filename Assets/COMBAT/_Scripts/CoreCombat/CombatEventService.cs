using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using Interactables;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Combat
{
    public class CombatEventService : MonoSingletonGeneric<CombatEventService>
    {
        public event Action OnSOTactics; 
        public event Action OnSOT;
        public event Action OnEOT;
        public event Action <int> OnSOR1;// round no
        public event Action <int> OnEOR1;
        public event Action OnSOC;  
        public event Action OnCombatInit;       
        public event Action OnEOC;      
        public event Action<CharController> OnCombatFlee;
        public event Action<CharController> OnDeathInCombat;
        public event Action<CharController> OnCombatRejoin;
        public event Action<StrikeData> OnStrikeFired;
       // public event Action<DmgData> OnDmgDelivered;
        public event Action<DmgAppliedData> OnDamageApplied;

        public event Action<GameObject> OnCharRightClicked;
        public event Action<CharController> OnCharOnTurnSet;
        public event Action<bool> OnCombatLoot;
        public event Action OnCombatEnd;

        // Strike FX 

        public event Action<DmgAppliedData> OnDodge; // from target perspective
        public event Action<CharController> OnMisfire; // from striker perspective

        public event Action<CharController> OnHasteCheck;
        public event Action<CharController, bool> OnMoraleCheck; 


        public event Action <CharController> OnCharClicked;
        public event Action OnCharHovered;

        public event Action <DynamicPosData, CellPosData> OnTargetClicked;

        public event Action<CharController, PotionNames> OnPotionConsumedInCombat;


        RoundController roundController; 

        // Start is called before the first frame update
        void Start()
        {
         
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

        }
        // on scene Load
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
              
                CombatService.Instance.GetAllyInCombat(); 
                On_CombatInit(CombatState.INTactics);
            }
        }

        public void On_StrikeFired(StrikeData strikeData)
        {
            OnStrikeFired?.Invoke(strikeData); 

        }
        public void On_CombatInit(CombatState startState)
        {
            OnCombatInit?.Invoke();
            Sequence seq = DOTween.Sequence();
            if (startState== CombatState.INTactics) 
            {
                seq
               .AppendInterval(0.2f)
               .AppendCallback(() => On_SOTactics())
               ;
                seq.Play();
            }
            if(startState == CombatState.INCombat_normal)
            {
                seq
               .AppendInterval(0.2f)
               .AppendCallback(() => On_SOC())
               ;
                seq.Play();
            }
        }

        public void On_CombatLoot(bool isVictory)
        {
            CombatService.Instance.isVictory = isVictory;
            CombatService.Instance.combatState = CombatState.INCombat_Loot; 
            OnCombatLoot?.Invoke(isVictory); 
        }

        public void On_CharOnTurnSet()
        {       
            CharController charCtrl = CombatService.Instance.currCharOnTurn;
            DynamicPosData dynaOnTurn = GridService.Instance.GetDyna4GO(charCtrl.gameObject);
            GridService.Instance.gridView.CharOnTurnHL(dynaOnTurn);
            charCtrl.RegenStamina();
            charCtrl.HPRegen();
            charCtrl.skillController.UpdateAllSkillState(charCtrl); 
            Debug.Log("CHAR SET ON TURN >>>>" + charCtrl.charModel.charName);
         
            OnCharOnTurnSet?.Invoke(charCtrl);
            On_CharClicked(charCtrl.gameObject);
        }
     
        public void On_SOTactics()
        {
            CombatService.Instance.combatState = CombatState.INTactics; // skip one frame for Inits to occur
            // combat Services
            CombatService.Instance.currCharOnTurn = CharService.Instance.allCharInCombat[0];

            OnSOTactics?.Invoke(); 
        }
        public void On_SOC()
        {
            roundController = CombatService.Instance.roundController; 
            CombatService.Instance.combatState = CombatState.INCombat_normal;
            CombatService.Instance.SetEnemyInCombat(EnemyPackName.RatPack3);
            CombatService.Instance.AddCombatControllers();
            SkillService.Instance.InitSkillControllers();// For enemies 
            CombatService.Instance.currCharOnTurn = CharService.Instance.allCharInCombat[0]; 
            OnSOC?.Invoke();           


            Sequence seq = DOTween.Sequence(); 
            seq.AppendInterval(2.5f)
                .AppendCallback(()=> On_SOR(1))
                ;
            seq.Play();
        }

        public void On_EOC()
        {
            Debug.Log("EOC triggered");
            FortReset2FortOrg();         
            CombatService.Instance.combatEndView.ShowCombatEndView();
        

            OnEOC?.Invoke();
            CharService.Instance.allCharInCombat.Clear();
        }
        public void On_CombatFlee(CharController charController)
        {
            OnCombatFlee?.Invoke(charController); 
        }
        private void FortReset2FortOrg()
        {
            foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
            {
                if(c.charModel.orgCharMode == CharMode.Ally)
                {
                    float fortOrg = c.GetAttrib(AttribName.fortOrg).currValue;                   

                    c.SetCurrStat(CauseType.CombatOver, -1, c.charModel.charID,AttribName.fortOrg,0); 
                    
                }
            }
        }
        public void On_SOR(int roundNo)
        {
            Debug.Log("SOR Triggered" + roundNo);
            roundController.OnRoundStart(roundNo);
            On_SOT();

            OnSOR1?.Invoke(roundNo);
        }
        public void On_EOR(int roundNo)
        {
            if(CombatService.Instance.combatState == CombatState.INCombat_normal)
            {
                Debug.Log("EOR triggered" + roundNo);                 
                OnEOR1?.Invoke(roundNo);
            }       
        }

        public void On_EOT()
        {
            Debug.Log("EOT CALLED");
            OnEOT?.Invoke();
        }

        public void On_SOT()
        {
            Debug.Log("SOT CALLED");
            roundController.SetNextCharOnTurn(); 
            OnSOT?.Invoke(); 
        }
        public void Move2NextRds()
        {    
            int roundNo = CombatService.Instance.currentRound;
            Debug.Log(roundNo + "Check end of round.........................");
            On_EOR(roundNo);
            Debug.Log("Check end of round" + roundNo);
            int MAX_RD_LIMIT = GameService.Instance.gameController.GetMaxRoundLimit();
            if (roundNo >= MAX_RD_LIMIT)
            {
               
                On_EOC();
            }   
            else
            {
                roundNo = ++CombatService.Instance.currentRound;
                On_SOR(roundNo);
            }
        }
        //bool CheckEndOFRound()
        //{
        //    int currTurn = CombatService.Instance.currentTurn;
        //    int charCount = CharService.Instance.charsInPlay.Count;
            
        //    if (currTurn >= (charCount)) 
        //        return true;
        //    else 
        //       return false; 
        //}
        public void On_DmgApplied(DmgAppliedData dmgAppliedData)
        {
           OnDamageApplied?.Invoke(dmgAppliedData);
        }
        public void On_targetClicked(DynamicPosData _targetDyna, CellPosData cellPosData)
        {
            if (CombatService.Instance.combatState == CombatState.INCombat_Pause
                || CombatService.Instance.combatState == CombatState.INTactics
                ) return; // patch fix as perk selpanel was not blocking raycast

            int currCharID = CombatService.Instance.currCharOnTurn.charModel.charID; 
            SkillModel skillModel = SkillService.Instance.GetSkillModel(currCharID
                             , SkillService.Instance.currSkillName);
            if (skillModel == null) return;
            if (_targetDyna != null)  // this happens only in move and remote skills{when applied on tile} 
            {
                Debug.Log("Target Dyna " + _targetDyna.charGO.GetComponent<CharController>().charModel.charName);
                CharController targetController = _targetDyna.charGO.GetComponent<CharController>();
                CombatService.Instance.currTargetClicked = targetController;

                if (targetController.charStateController.HasCharState(CharStateName.Cloaked)
                  && CombatService.Instance.mainTargetDynas.Count == 1)
                {
                    return;
                }

                if (targetController.charStateController.HasCharState(CharStateName.Guarded)
                   && CombatService.Instance.mainTargetDynas.Count ==1)
                {                
                    CharStateBuffData charStateBuffData = targetController.charStateController
                                                    .GetCharStateBuffData(CharStateName.Guarded);

                    int guardingCharID = charStateBuffData.charStateModData.causeByCharID; 
                    CharController charController = CharService.Instance
                                                        .GetCharCtrlWithCharID(guardingCharID);
                    DynamicPosData newTargetDyna = GridService.Instance.GetDyna4GO(charController.gameObject);

                    _targetDyna = newTargetDyna;
                    CombatService.Instance.currTargetClicked = newTargetDyna.charGO.GetComponent<CharController>();
                    skillModel.targetPos.Clear();
                    skillModel.targetPos.Add(new CellPosData(newTargetDyna.charMode,newTargetDyna.currentPos)); 
                }
                OnTargetClicked?.Invoke(_targetDyna, null);
            }
            else
            {
     
                if (skillModel.skillType == SkillTypeCombat.Move)
                {
                    GameObject charGO = CombatService.Instance.currCharOnTurn.gameObject;
                    _targetDyna = GridService.Instance.GetDyna4GO(charGO);
                    CombatService.Instance.currTargetClicked = CombatService.Instance.currCharOnTurn;
                    SkillService.Instance.currentTargetDyna = _targetDyna;
                    // In move skill if a empty tile is clicked
                    if (!skillModel.targetPos.Any(t => t.pos == cellPosData.pos && t.charMode == cellPosData.charMode))
                        return;
                    OnTargetClicked?.Invoke(_targetDyna, cellPosData);
                }
                if (skillModel.attackType == AttackType.Remote)
                {
                    if (!skillModel.targetPos.Any(t => t.pos == cellPosData.pos && t.charMode == cellPosData.charMode))
                        return;
                    SkillService.Instance.SetRemoteSkills(skillModel, cellPosData);

                }
            }
        }

        public void On_CharClicked(GameObject _charClickedGO)
        {
            CombatService.Instance.currCharClicked = _charClickedGO?.GetComponent<CharController>();  
            OnCharClicked?.Invoke(CombatService.Instance.currCharClicked); 
        }

        public void On_CharRightClicked(GameObject _charRightClickedGO)
        {
            OnCharRightClicked?.Invoke(_charRightClickedGO); 
        }

        public void On_CharHovered(GameObject _charHoveredGO)
        {
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
           CombatService.Instance.currCharHovered = _charHoveredGO?.GetComponent<CharController>(); 

        }

        public void IsActionSubcribed()
        {
            foreach (Action del in OnSOR1.GetInvocationList())
            {
                Debug.Log("SOR Subs" + del.Method.Name);
            }
        }


        #region STRIKE FX EVENTS

        public void On_Dodge(DmgAppliedData dmgAppliedData)
        {
            OnDodge?.Invoke(dmgAppliedData);
        }

        #endregion

        #region Checks Broadcast

        public void On_HasteCheck(CharController charController)
        {
            OnHasteCheck?.Invoke(charController);   
        }
        public void On_MoraleCheck(CharController charController, bool posChk)
        {
            OnMoraleCheck?.Invoke(charController, posChk);
        }
        #endregion

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                On_CombatInit(CombatState.INTactics);
                Debug.Log("ON COmbat Init");
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                On_SOTactics();
                Debug.Log("SOTactics");
            }

            //if (Input.GetKeyDown(KeyCode.I))
            //{
            //    On_SOC();
            //    Debug.Log("SOC");

            //}
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    Debug.Log("SOR");
            //    On_SOR(1);
            //}
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    Debug.Log("SOT");
            //    On_SOT();
            //}

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Debug.Log("On CharOn Turn Set");
            //    On_CharOnTurnSet();
            //}

            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    Debug.Log("On CharOn Turn Set");
            //    On_EOT();
            //}
            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    IsActionSubcribed();
            //}



        }
    }
}

