using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.Linq;
using DG.Tweening; 
using Common;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Town;

namespace Combat
{
     [Serializable]
    public class SkillEventData
    {
        public CharController strikerController;
        public CharController targetController;
        public SkillNames skillName;
        public SkillModel skillModel;

        public SkillEventData(CharController strikerController, CharController targetController
            , SkillNames skillName, SkillModel skillModel)
        {
            this.strikerController = strikerController;
            this.targetController = targetController;
            this.skillName = skillName;
            this.skillModel = skillModel;
        }
    }

    public class SkillService : MonoSingletonGeneric<SkillService>
    {
        public event Action<PerkData> OnPerkStateChg;
        public event Action<SkillModel> OnSkillSelectInInv; 
        public event Action<PerkNames> OnPerkHovered;

       // ("ON SKILL APPLY")]
        private event Action _OnSkillApply = null;
        public event Action OnSkillApply
        {
            add
            {
                if (_OnSkillApply == null || !_OnSkillApply.GetInvocationList().Contains(value))
                {                    
                    _OnSkillApply += value;
                    Debug.Log("skill apply >>" + _OnSkillApply.GetInvocationList().Length);
                }
                else
                {
                    Debug.Log("Duplicate >>");
                }
            }
            remove
            {
                _OnSkillApply -= value;
            }
        }

        #region Initializers

        [Header("SKill Factory NTBR")]
        public SkillFactory skillFactory;

        //[Header("SkillCard Data")]
        //public SkillCardData skillCardData;
        [Header("CURR SKILLMODEL")]
        public SkillModel skillModelHovered;
      //  public SkillModel skillModelSelect; 
        public List<string> perkDescOnHover= new List<string>();

        [Header("ALL SO")]
        public List<SkillDataSO> allCharSkillSO = new List<SkillDataSO>();
        public SkillViewSO skillViewSO; 
        public SkillHexSO skillHexSO;   
        public GameObject skillCardGO;
        [SerializeField] GameObject skillCardPrefab; 

        [Header("ALL SKILL MANAGER")]
        public List<SkillController1> allSkillControllers = new List<SkillController1>();   
        public SkillController1 currSkillController = new SkillController1();

        [Header(" Skill Model")]
        public SkillModel currSkillModel; 
        [Header("ALL CTRL AI RELATED")]
       // public List<SkillAIController> allSKillAIControllers = new List<SkillAIController>();

        [Header("SKILL MOVE AND FX RELATED")]
        public SkillFxMoveController skillFXMoveController;

        [Header(" Ignore Haste Chk")]
        public bool ignoreHasteChk = false; 

        // ALL ACTIONS// 
        public event Action SkillInit;
        public event Action <CharNames, SkillNames>SkillSelect;
        public event Action SkillDeSelect; 
        public event Action PreSkillApply; // target is selected and but dodge acc etc to be chked N fixed 
       // public event Action SkillApply;
        public event Action SkillApplyMoveFx; 
        public event Action PostSkillApply; 
        public event Action SkillHovered;
        public event Action SkillWipe;
        public event Action SkillFXRemove; 
        public event Action SkillTick;// no use for now... 
        public event Action SkillEnd;// no use for now...
        public event Action<SkillEventData> OnSkillUsed; 
        [Header("curr Char UPDATES")]
        public CharMode currCharMode;

        [Header(" ALL SKILLS DATA")]
        public SkillNames currSkillName = SkillNames.None;
        public DynamicPosData currentTargetDyna = new DynamicPosData();
        public DynamicPosData currStrikerDyna = new DynamicPosData(); 
        public SkillNames defaultSkillName;

        public SkillNames currSkillHovered;        
        public SkillView skillView;

        [Header(" PASSIVE SKILLS")]    
        public PassiveSkillFactory passiveSkillFactory; 


        public int currSkillPts =10;

        public float combatSpeed = 1f;
     
        void Start()
        {
            // InitSkillControllers();
            // Cn be later Set to the start of Combat Event
            skillFactory = GetComponent<SkillFactory>();
            skillView = GetComponent<SkillView>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameEventService.Instance.OnGameStateChg += OnStartOfCombat;
            skillFactory =GetComponent<SkillFactory>();
            skillFactory.SkillsInit();
            passiveSkillFactory = GetComponent<PassiveSkillFactory>();
            passiveSkillFactory.PassiveSkillsInit();
            CreateSkillCardGO();
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
           // OnSkillApply -= SkillEventtest;
            GameEventService.Instance.OnGameStateChg -= OnStartOfCombat;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                skillView = FindObjectOfType<SkillView>();
                _OnSkillApply += SkillEventtest;
                skillFXMoveController = gameObject.GetComponent<SkillFxMoveController>();
                if(skillFXMoveController == null)
                    skillFXMoveController = gameObject.AddComponent<SkillFxMoveController>();

                CreateSkillCardGO(); 
            }           
        }

        void CreateSkillCardGO()
        {
            GameObject canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (skillCardGO == null)
            {
                skillCardGO = Instantiate(skillCardPrefab);
            }
            skillCardGO.transform.SetParent(canvasGO.transform);
            skillCardGO.transform.SetAsLastSibling();
            skillCardGO.transform.localScale = Vector3.one;
            skillCardGO.SetActive(false);
        }

        void OnStartOfCombat(GameState gameState)
        {
            if (gameState != GameState.InCombat) return;
            CombatEventService.Instance.OnSOT += SetDefaultSkillForChar;
            CombatEventService.Instance.OnCharOnTurnSet += InitEnemySkillSelection; 
            CombatEventService.Instance.OnTargetClicked += TargetIsSelected;
            PostSkillApply += GridService.Instance.ClearOldTargets;// to be decided later to DEL or MOVE 
            CombatEventService.Instance.OnCharOnTurnSet += PopulateSkillTargets;

        }
        public void On_PerkStateChg(PerkData perkData)
        {
            OnPerkStateChg?.Invoke(perkData);
        }
        public void On_SkillSelectedInInv(SkillModel skillModel)
        {
            currSkillModel = skillModel;
            OnSkillSelectInInv?.Invoke(skillModel); 
        }
        //public void On_SkillSelectInCombat(SkillModel skillModel)
        //{
        //    skillModelSelect = skillModel;
        //    On_SkillSelected(skillModel.charName, skillModel.skillName);
        //}
        void SkillDisplay()  // some reference is there for SKILL DISPLAY ON TOP
        {
            
          //  temptxt.text = "SkillName" + currSkillName; 
        }

        #endregion
    
        #region SKILL_INIT related

        public void SetDefaultSkillForChar()
        {
            // get skillmodel and skillcontroller 
            // loop thru all the skills set default skill to first clickable skill
            // if no skills in the loop => pass the turn 

            CharController charOnTurn = CombatService.Instance.currCharOnTurn; 
            SkillController1 skillController = charOnTurn.GetComponent<SkillController1>();

            SkillModel defaultSkillModel = null; 
            foreach (SkillModel skillModel in skillController.allSkillModels)
            {
                if(skillModel.GetSkillState() == SkillSelectState.Clickable)
                {
                    defaultSkillModel = skillModel;
                    defaultSkillName = defaultSkillModel.skillName; 
                    break; 
                }
            }
            //if(defaultSkillModel == null)
            //{
            //    if(charOnTurn.charModel.charMode == CharMode.Ally)
            //     On_PostSkill(defaultSkillModel); // pass the turn
            //}
            //SkillDataSO skillDataSo = GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);
            // defaultSkillName =  skillDataSo.allSkills[0].skillName; 
        }

        public void PopulateSkillTargets(CharController charController)
        {
            //if (charController.charModel.charMode == CharMode.Enemy) return; 
           SkillController1 skillController = charController.gameObject.GetComponent<SkillController1>();
            //skillController.allSkillBases.ForEach(t => Debug.Log("SKILL BASES ARE HEALTHY" +t.charName + t.skillName));
            //skillController.allSkillModels.ForEach(t => Debug.Log(t.skillName +"SkillBase" + t.targetPos.Count));
            
             skillController.allSkillBases.ForEach(t => t.PopulateTargetPos());
            if(currCharMode == CharMode.Ally)
             skillController.allPerkBases.ForEach(t => t.AddTargetPos());
            // skillController.CheckNUpdateSkillState();  // checked in view not needed here
        }
        // ON SOC 
        public void InitSkillControllers()
        {
            foreach (GameObject charGO in CharService.Instance.charsInPlay)
            {
                SkillController1 skillController = charGO.GetComponent<SkillController1>(); 

                //if (skillController == null)
                //{
                //    skillController = charGO.gameObject.AddComponent<SkillController1>();
                //    allSkillControllers.Add(skillController);                    
                //   skillController.InitSkillList(skillController.charController); 

                //    //SkillAIController skillAIController = character.gameObject.AddComponent<SkillAIController>();
                //    //allSKillAIControllers.Add(skillAIController);
                //}
                if(skillController!= null)
                {
                    if (skillController.charController.charModel.orgCharMode == CharMode.Enemy)
                    {
                        skillController.InitSkillList(skillController.charController);
                        skillController.InitPassiveSkillController();
                    }
                }
                else
                {
                    Debug.LogError("SkillController is Null" + charGO.name); 
                }

                
                    
                CharNames charName = charGO.GetComponent<CharController>().charModel.charName; 
                //   skillController.InitSkillList(charName);
            }
        }
        #endregion

        #region AI

        public void InitEnemySkillSelection(CharController charController)
        {

            if (charController.charModel.charMode == CharMode.Ally)
            {
                Debug.Log("ALLY TURN");
                return;
            }
            ClearPrevData();

            Debug.Log("INIT SKILL CONTROLLER >>>>>>>>>>>>");
            currSkillController = CombatService.Instance.currCharOnTurn
                                    .gameObject.GetComponent<SkillController1>();
     
            if (currSkillController != null)
            {
                currSkillController.StartAISkillInController();              
            }
            else
                Debug.Log("SkillController NULL");
        }

        #endregion

        #region Actions 

        public void OnTargetReached()
        {
            Sequence FXHitTargetSeq = DOTween.Sequence();

            FXHitTargetSeq.AppendCallback(() => SkillApplyMoveFx?.Invoke())
                .AppendInterval(0.2f)
               // .AppendCallback(() => skillFXMoveController.ApplyImpactFXOnAllTarget())
                ;
            FXHitTargetSeq.Play(); 
            
        }

        public void TargetIsSelected(DynamicPosData target, CellPosData cellPosData = null)
        {
            // FOCUS CHECK TO BE INCORPORPORATED HERE 
            
            if (CombatService.Instance.combatState != CombatState.INCombat_InSkillSelected)
            {
                Debug.Log("Combat State not in skill selected"); return;
            }

            int currCharID = CombatService.Instance.currCharOnTurn.charModel.charID;
            currSkillModel = GetSkillModel(currCharID, currSkillName);

            CharController targetController = null;
         
            if (currSkillModel.skillType != SkillTypeCombat.Move && currSkillModel.attackType != AttackType.Remote)
            {
              
                currentTargetDyna = target;
                StrikeController strikeController =
                            CombatService.Instance.currCharOnTurn.GetComponent<StrikeController>();
                 targetController = target.charGO.GetComponent<CharController>();
                List<DamageType> dmgTypes = currSkillModel.dmgType;
                bool isPhysical = dmgTypes.Any(t => t == DamageType.Physical);
                bool isMagical = dmgTypes.Any(t => t == DamageType.Air || t == DamageType.Dark
                || t == DamageType.Earth || t == DamageType.Fire || t == DamageType.Light
                || t == DamageType.Water);
                if (!currSkillModel.targetPos.Any(t => t.pos == target.currentPos))
                    //&& t.charMode == target.charMode))
                    return;

               
                Debug.Log("TARGET SELECT" + target.charGO.name);
            }
      

            if(currSkillModel.skillType == SkillTypeCombat.Move && cellPosData  == null)
            {
                return;
            }
            if (currSkillModel.attackType == AttackType.Remote && cellPosData == null)
            {
                return;
            }

            On_SkillUsed(new SkillEventData(CombatService.Instance.currCharOnTurn
                            , targetController, currSkillName, currSkillModel));
            PreSkillApply?.Invoke();
            SkillFXRemove?.Invoke();
            _OnSkillApply.Invoke();
            On_PostSkillApply();
        }

        public void On_SkillUsed(SkillEventData skillEventData)
        {
            Debug.Log("SKILL USED >>>>>" + skillEventData.skillModel.skillName); 
            OnSkillUsed?.Invoke(skillEventData);
        }

        void On_PostSkillApply()
        {
            // char Death update here 
           // CharService.Instance.UpdateOnDeath();// EOt to Manage
            GridService.Instance.ClearOldTargets();
            CombatService.Instance.combatState = CombatState.INCombat_normal;
            PostSkillApply?.Invoke();
        }

        public void OnAITargetSelected(SkillModel skillModel)
        {
            if (currentTargetDyna == null)
            {
                Debug.LogError("TARGET Dyna IS NULL");
            }
            CharController targetController = currentTargetDyna.charGO.GetComponent<CharController>();
          
            StrikeController strikeController =
                        CombatService.Instance.currCharOnTurn.GetComponent<StrikeController>();

            On_SkillUsed(new SkillEventData(CombatService.Instance.currCharOnTurn
                                    , targetController, currSkillName, skillModel));

            if (_OnSkillApply == null) return;
            PreSkillApply?.Invoke();
            SkillFXRemove?.Invoke();

            Sequence eventSeq = DOTween.Sequence();

            eventSeq
                .AppendCallback(() => _OnSkillApply?.Invoke())
                .AppendInterval(2)
                .AppendCallback(On_PostSkillApply)
                ;
            eventSeq.Play(); 
        }

        public void On_SkillSelected(CharNames _charName, SkillNames skillName)  // Ally Skill and perk "Skill Select" 
        {
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                CombatService.Instance.combatState = CombatState.INCombat_InSkillSelected;
                ClearPrevData();
                SkillSelect?.Invoke(_charName, skillName);  // message broadcaster 

                int currCharID = CombatService.Instance.currCharOnTurn.charModel.charID;
                currSkillModel = GetSkillModel(currCharID, currSkillName);

                if (currSkillModel.skillType == SkillTypeCombat.Move)
                    CombatService.Instance.ToggleColliders(false);
                else
                    CombatService.Instance.ToggleColliders(true);
            }
            int index = allSkillControllers.FindIndex(t => t.charName == _charName);
            if(index != -1)
                currSkillController = allSkillControllers[index];
            else
            Debug.Log("skill controller not found!" + _charName+ "SkillName" + skillName);
            currSkillController.SkillSelect(skillName);

        }

        public void ClearPrevSkillData()
        {
            ClearPrevData();
            GridService.Instance.ClearOldTargets();          
        }
        public void DeSelectSkill()
        {
            CombatService.Instance.combatState = CombatState.INCombat_normal;
            ClearPrevSkillData();
            int charID = CombatService.Instance.currCharClicked.charModel.charID;
            SkillModel skillModel = GetSkillModel(charID, currSkillName);
            if (skillModel == null)
                return;
            skillModel.SetSkillState(SkillSelectState.Clickable);
            currSkillName = SkillNames.None;
            skillView.SetSkillsPanel(CombatService.Instance.currCharClicked);
            skillView.FillSkillClickedState(-1);
        }
        public void On_SkillHovered(CharNames _charName, SkillNames skillName)
        {
            SkillHovered = null; SkillWipe = null;
            currSkillHovered = skillName; 
            currSkillController = allSkillControllers.FirstOrDefault(t => t.charName == _charName);
            currSkillController.SkillHovered(currSkillHovered);
            

            if(SkillWipe != null)
            {
                foreach (Action del in SkillWipe.GetInvocationList())
                {
                    Debug.Log("Action" + del.Method.Name);
                }
            }
            else
            {
                // Debug.Log("SkillWipe is null"); 
            }            
            SkillWipe?.Invoke(); 
            SkillHovered?.Invoke(); 
        }

        bool HasteChk(CharController charController)
        {
            AttribData hasteData = charController.GetAttrib(AttribName.haste);
            float chance = (float)CharService.Instance.statChanceSO.GetChanceForStatValue(AttribName.haste, (int)hasteData.currValue);

            if (chance.GetChance())
            {
                CombatEventService.Instance.On_HasteCheck(charController); 
                return true;
            }                
            return false; 
        }


        public void On_PostSkill(SkillModel skillModel)
        {
            // ClearPrevData();  // redundant safety .. causing only one FX to play as it clears mainTargetDyna
        
            CharController currCharOnturn = CombatService.Instance.currCharOnTurn;
            CombatController combatController = currCharOnturn.GetComponent<CombatController>();
            // if ally reduce action pts
            if (skillModel != null)
            { // HASTE CHECK      
                bool hasteChk = false;
                if (skillModel.skillInclination == SkillInclination.Move && !ignoreHasteChk)
                    hasteChk = HasteChk(currCharOnturn);
               
                // AP UPDATES 
                if (hasteChk) // if haste check allies get a extra AP
                    combatController.actionPts++;

                combatController.SubtractActionPtOnSkilluse(skillModel, currCharOnturn.charModel.charMode);
                currSkillController.UpdateAllSkillState(currCharOnturn);
            }else if (currCharOnturn.charModel.charMode == CharMode.Enemy) 
            {
                Move2Nextturn();
                return; 
            }

            if (combatController.actionPts > 0)// allies 
            {
                CombatService.Instance.roundController.SetSameCharOnTurn();
                if (currCharOnturn.charModel.charMode == CharMode.Enemy)
                    InitEnemySkillSelection(CombatService.Instance.currCharOnTurn);   // to be called 
            }
            else
            {
                if (currCharOnturn.charModel.charMode == CharMode.Enemy)
                    Move2Nextturn();
            }
        }
        

        public void Move2Nextturn()
        {
            CombatEventService.Instance.On_EOT();


            Sequence PauseSeq = DOTween.Sequence();

            PauseSeq.AppendInterval(1f)
                 .AppendCallback(ClearPrevData)
                .AppendCallback(CombatEventService.Instance.On_SOT)
                .AppendInterval(1f)
                ;
            PauseSeq.Play();
           
        }
        public void PerkUnLock(PerkNames _perkName, GameObject btn)
        {       
           // shifted to the skillController    
                         
               
        }   

        #endregion
        #region GETTERS and SETTERS

        public SkillController1 GetSkillController(CharController _charController)
        {
            SkillController1 skillController = allSkillControllers.Find(t => t.charController == _charController);
            if (skillController != null)
                return skillController; 
            else
            {
                Debug.Log("SkillController not found");
            }
            return null; 
        }

        public Sprite GetCurrSkillSprite()
        {
            SkillDataSO skillDataSO = GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);

            return skillDataSO.allSkills.Find(t => t.skillName == currSkillName).skillPose;             
        }

        public SkillPerkFXData GetSkillFXDataOnSkillSelect(PerkType perkType)
        {
            SkillDataSO skillDataSO = GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);
            List<SkillPerkFXData> allSkillPerkFXData =
                    skillDataSO.allSkills.Find(t => t.skillName == currSkillName).allSkillFXs;
            CharMode currCharMode = CombatService.Instance.currCharOnTurn.charModel.charMode;
            SkillPerkFXData skillPerkFXData =
                       allSkillPerkFXData.Find(t => t.charMode == currCharMode && t.perkType == perkType);

            return skillPerkFXData; 
        }
        public SkillPerkFXData GetSkillPerkFXData(PerkType perkType)
        {
            // to be converted to max perk type after testing
            // .. current for lvl0 to be extended to the perks
            SkillDataSO skillDataSO = GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);
            List<SkillPerkFXData> allSkillPerkFXData =
                        skillDataSO.allSkills.Find(t => t.skillName == currSkillName).allSkillFXs;

            CharMode targetCharMode = currentTargetDyna.charMode;


            int index = allSkillPerkFXData.FindIndex(t => t.charMode == targetCharMode);
            //&& t.perkType == perkType); 

            if (index != -1)
                return allSkillPerkFXData[index];
            else
                Debug.Log(" FX Not found!!!!!" + currSkillName); 
            return null;
        }
        public SkillDataSO GetSkillSO(CharNames _charName)
        {
            SkillDataSO skillDataSO = allCharSkillSO.Find(x => x.charName == _charName);
            if (skillDataSO != null)
            {
                return skillDataSO;
            }
            else
            {
                Debug.Log("skill Data SO  Not FOUND");
                return null;
            }
        }
        public CharNames GetChar4Skill(SkillNames _skillName)
        {
            foreach (SkillDataSO skillDataSO in allCharSkillSO)
            {
                if (skillDataSO.allSkills.Any(t => t.skillName == _skillName))
                {
                    return skillDataSO.charName;
                }
            }
            return 0;
        }

        public SkillModel GetSkillModel(int _charID, SkillNames _skillName)
        {

            // find char Controller with charID
            // find skill Controller with the charID 
            // find skillBase in that skillController
            // 
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(_charID);
            if (charController == null) return null; 
            SkillController1 skillController = charController.GetComponent<SkillController1>(); 
          //  Debug.Log("skillcontroller found" + skillController.allSkillBases.Count);
            foreach (SkillModel skillModel in skillController.allSkillModels)
            {
                if (skillModel.skillName == _skillName)
                {
                    return skillModel; 
                }
            }
            Debug.Log("SkillModel Not found" + _skillName  +" CHAR ID " + _charID); 
            return null; 
        }
        public GameObject GetGO4SkillCtrller(CharNames _charName)
        {
            GameObject go = allSkillControllers.Find(t => t.charName == _charName).gameObject;
            return go;
        }
        public List<PerkBaseData> GetAllPerkdata(SkillNames _skillName)
        {
            // List<PerkBaseData> perks = .Where(t => t.skillName == _skillName).ToList();
            // Debug.Log("INSIDE GET PERK DATA "); 
            // Dictionary<PerkType, PerkBaseData> perkDataMap = new Dictionary<PerkType, PerkBaseData>();

            // for(int i = 1; i < (perks.Count+1); i++)  //Enum.GetNames(typeof(PerkType)).Length
            // {
            //     perkDataMap.Add((PerkType)i, perks.Find(t => t.perkType == (PerkType)i));
            //    // Debug.Log(perkDataMap); 
            //     //Debug.Log("PERK TYPES" + (PerkType)i + "PERK NAME" + 
            //     //    perks.Find(t => t.perkType == (PerkType)i).perkName); 
            // }
            //// perkDataMap.Values.ToList().ForEach(t => Debug.Log("List" + t.perkName)); 
            // return   perkDataMap.Values.ToList();           
            return null; 
        }

        //public PerkBaseData GetPerkData(PerkNames _perkName)
        //{
        //   // return allSkillPerksData.Find(t => t.perkName == _perkName); 
        //}
        //public void SetPerkState(PerkNames _perkName, PerkSelectState state)
        //{
        //  //  allSkillPerksData.Find(t => t.perkName == _perkName).state = state; 
        //}

        #endregion

        #region APPLY REMOTE SKILLS
        
        public void SetRemoteSkills(SkillModel skillModel, CellPosData cellPosData)
        {
            
            CharController charController = CombatService.Instance.currCharOnTurn; 
            if(charController.combatController.actionPts >0)
                GridService.Instance.gridView.SetRemoteSkill(skillModel, cellPosData); 
            ClearPrevData();
            currSkillController.UpdateAllSkillState(charController);
            On_PostSkillApply(); // move to the next turn
        }

        #endregion

        #region Helpers


        public AttackType GetSkillAttackType(SkillNames _skillName)
        {


            return AttackType.None; 
        }

        void ClearPrevData()
        {
            _OnSkillApply = null; SkillFXRemove = null; SkillApplyMoveFx = null;
            PostSkillApply = null;
            PreSkillApply = null;
            CombatService.Instance.mainTargetDynas.Clear();
            CombatService.Instance.colTargetDynas.Clear();
        }
        public List<DynamicPosData> GetTargetInRange(SkillModel _skillModel)
        {            
            List<DynamicPosData> dynas = new List<DynamicPosData>(); 

            foreach(CellPosData c in _skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(c.pos, c.charMode); 
                if (dyna != null)
                {                 
                    dynas.Add(dyna); 
                }         
            }
           //  dynas.ForEach(t=> Debug.Log("TTTT DYNA " + t.charGO.name));

            return dynas; 
        }


        public void SkillEventtest()
        {
            Debug.Log("SkillApplied");

            for (int i = SkillApplyMoveFx.GetInvocationList().Length - 1; i >= 0; i--)
            {
                var outputMsg = SkillApplyMoveFx.GetInvocationList()[i];

                Debug.Log("INVOKE  MOVE FX" + outputMsg.Method);
            }
        }
        #endregion
        private void Update()
        {
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                if (CombatService.Instance.combatState == CombatState.INTactics)
                    return;
                if (Input.GetMouseButtonDown(1))
                {
                    DeSelectSkill();
                }
            }
        }

    }


}
//else 
//{
//    //if (skillModel != null) // skillmodel is null when no skill can be selected 
//    //    skillView.UpdateSkillState(skillModel);

//    //if (hasteChk)  // if haste chk true enemies get an extra strike 
//    //{
//    //    CombatService.Instance.roundController.SetSameCharOnTurn();
//    //    return;
//    //}
//    if (combatController.actionPts > 0)// allies 
//    {
//        CombatService.Instance.roundController.SetSameCharOnTurn();
//    }
//    else
//    {
//        Move2Nextturn();
//    }
//}
// }
