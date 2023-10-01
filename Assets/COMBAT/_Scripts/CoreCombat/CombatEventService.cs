using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common; 

namespace Combat
{
    public class CombatEventService : MonoSingletonGeneric<CombatEventService>
    {
        public event Action<CharController> OnCharDeath;
        public event Action OnSOTactics; 
        public event Action OnSOT;
        public event Action OnEOT;
        public event Action OnSOR;
        public event Action OnEOR;
        public event Action OnSOC;
        public event Action<CombatState> OnSOC1;
        public event Action OnCombatInit;       
        public event Action OnEOC;      
        public event Action<CharController> OnFleeInCombat;
        public event Action<CharController> OnDeathInCombat;
        public event Action<CharController> OnCombatRejoin;
        public event Action<StrikeData> OnStrikeFired;
        public event Action<DmgData> OnDmgDelivered;
        public event Action<GameObject> OnCharRightClicked;
        public event Action<CharController> OnCharOnTurnSet;
        public event Action<bool> OnCombatLoot;
        public event Action OnCombatEnd;

        public event Action<CharController> OnDodge; // from target perspective
        public event Action<CharController> OnMisfire; // from striker perspective

        public event Action<CharController> OnReceivingCrit;
        public event Action<CharController> OnDeliveringCrit; 

        public event Action<CharController> OnReceivingFeeble;
        public event Action<CharController> OnDeliveringFeeble;

        public event Action<CharController> OnMoraleAction;
        public event Action<CharController> OnLosingAction;

        public event Action<CharController> OnHasteCheck; 



        public event Action OnCharClicked;
        public event Action OnCharHovered;

        public event Action <DynamicPosData>OnTargetClicked;
        public CombatController combatController;

        // Start is called before the first frame update
        void Start()
        {
            combatController = gameObject.AddComponent<CombatController>();
        }

        public void On_StrikeFired(StrikeData strikeData)
        {
            OnStrikeFired?.Invoke(strikeData); 

        }
        public void On_CombatInit()
        {
            //CharService.Instance.Init();
            OnCombatInit?.Invoke(); 
        }

        public void On_CombatLoot(bool isVictory)
        {
            CombatService.Instance.isVictory = isVictory;
            CombatService.Instance.combatState = CombatState.INCombat_Loot; 
            OnCombatLoot?.Invoke(isVictory); 
        }

        public void On_CharOnTurnSet(CharController charController)
        {       
            CharController charCtrl = CombatService.Instance.currCharOnTurn;
            DynamicPosData dynaOnTurn = GridService.Instance.GetDyna4GO(charCtrl.gameObject);
            GridService.Instance.gridView.CharOnTurnHL(dynaOnTurn);
            charCtrl.RegenStamina();
            charCtrl.HPRegen(); 
            OnCharOnTurnSet?.Invoke(charCtrl);
        }
        public void On_CharDeath(CharController _charController)
        {
            Debug.Log("@@@@@@ON CHAR DEATH INVOKE");
            OnCharDeath?.Invoke(_charController);
        }
        public void On_SOTactics()
        {
            CombatService.Instance.combatState = CombatState.INTactics;
            OnSOTactics?.Invoke();                
        }
        public void On_SOC()
        {
            CombatService.Instance.combatState = CombatState.INCombat_normal;   
            OnSOC?.Invoke();   
            OnSOC1?.Invoke(CombatService.Instance.combatState);
            SkillService.Instance.InitSkillControllers(); 
        }

        public void On_EOC()
        {
            FortReset2FortOrg();
            OnEOC?.Invoke();
        }

        private void FortReset2FortOrg()
        {
            foreach (CharController c in CharService.Instance.charsInPlayControllers)
            {
                if(c.charModel.orgCharMode == CharMode.Ally)
                {
                    float fortOrg = c.GetAttrib(AttribName.fortOrg).currValue;                   

                    c.SetCurrStat(CauseType.CombatOver, -1, c.charModel.charID,AttribName.fortOrg,0); 
                    
                }
            }
        }
        public void On_SOR()
        {
            OnSOR?.Invoke();
        }
        public void On_EOR()
        {
            if(CombatService.Instance.combatState == CombatState.INCombat_normal)
            {
                CombatService.Instance.currentRound++;
                OnEOR?.Invoke();
            }       
        }

        public void On_EOT()
        {
            Debug.Log("EOT CALLED");
            OnEOT?.Invoke();
        }

        public void On_SOT()
        {           
            if (CheckEndOFRound())
            {
                On_EOR();
                On_SOR(); 
            }
            Debug.Log("@@@@@SOT");
            OnSOT?.Invoke(); 
        }

        bool CheckEndOFRound()
        {
            int currTurn = CombatService.Instance.currentTurn;
            int charCount = CharService.Instance.charsInPlay.Count;
            
            if (currTurn >= (charCount)) 
                return true;
            else 
               return false; 

        }
        public void On_DmgDelivered(DmgData _dmgData)
        {
            OnDmgDelivered.Invoke(_dmgData);
        }
        public void On_targetClicked(DynamicPosData _targetDyna)
        {
            if (CombatService.Instance.combatState == CombatState.INCombat_Pause) return; // patch fix as perk selpanel was not blocking raycast

            int currCharID = CombatService.Instance.currCharOnTurn.charModel.charID; 
            SkillModel skillModel = SkillService.Instance.GetSkillModel(currCharID
                             , SkillService.Instance.currSkillName);

            if (_targetDyna.charGO != null)  
            {
                Debug.Log("Target Dyna " + _targetDyna.charGO.GetComponent<CharController>().charModel.charName);
                CombatService.Instance.currTargetClicked = _targetDyna.charGO.GetComponent<CharController>();
                OnTargetClicked?.Invoke(_targetDyna);
            } else if(skillModel.skillType == SkillTypeCombat.Move)
            {
                GameObject charGO = CombatService.Instance.currCharOnTurn.gameObject;
                _targetDyna = GridService.Instance.GetDyna4GO(charGO);
                CombatService.Instance.currTargetClicked = CombatService.Instance.currCharOnTurn;
                SkillService.Instance.currentTargetDyna = _targetDyna;
                OnTargetClicked?.Invoke(_targetDyna);
            }
        }

        public void On_CharClicked(GameObject _charClickedGO)
        {
            CombatService.Instance.currCharClicked = _charClickedGO?.GetComponent<CharController>();  
            OnCharClicked.Invoke(); 
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
            foreach (Action del in OnSOR.GetInvocationList())
            {
                Debug.Log("SOR Subs" + del.Method.Name);
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                On_CombatInit();
                Debug.Log("ON COmbat Init");
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                On_SOTactics();
                Debug.Log("SOTactics");
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                On_SOC();
                Debug.Log("SOC");

            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("SOR");
                On_SOR();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("SOT");
                On_SOT();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("EOT");
                On_EOT();
            }

            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    IsActionSubcribed();
            //}



        }
    }
}

