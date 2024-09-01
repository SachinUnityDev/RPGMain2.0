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
using System.IO;

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

    public class SkillService : MonoSingletonGeneric<SkillService>, ISaveable
    {
        #region All ACTION 
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
                   // Debug.Log("skill apply >>" + _OnSkillApply.GetInvocationList().Length);
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

        public event Action SkillInit;
        public event Action<CharNames, SkillNames> SkillSelect;
        public event Action SkillDeSelect;
        public event Action PreSkillApply; // target is selected and but dodge acc etc to be chked N fixed 
                                           // public event Action SkillApply;
        public event Action OnSkillApplyMoveFx;
        public event Action PostSkillApply;
        public event Action OnSkillHovered;
        public event Action SkillWipe;
        public event Action SkillFXRemove;
        public event Action SkillTick;// no use for now... 
        public event Action SkillEnd;// no use for now...
        public event Action<SkillEventData> OnSkillUsed;

        # endregion

        #region DECLARATIONS

        [Header("SKill Factory NTBR")]
        public SkillFactory skillFactory;

        [Header("CURR SKILLMODEL")]
        public SkillModel skillModelHovered;
        
        public List<string> perkDescOnHover= new List<string>();

        [Header("ALL SO")]
        public List<SkillDataSO> allSkillDataSO = new List<SkillDataSO>();
        public SkillViewSO skillViewSO; 
        public SkillHexSO skillHexSO;

        [Header("Skill Card")]
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
        public List<CharSkillModel> allCharSkillModel = new List<CharSkillModel>();


        public int currSkillPts =10;

        public float combatSpeed = 1f;

        public ServicePath servicePath => ServicePath.SkillService;

        public Scene currentScene; 
        protected override void Awake()
        {

            base.Awake();
            // InitSkillControllers();
            // Cn be later Set to the start of Combat Event
            
           
        }
        private void OnEnable()
        {
            skillFactory = GetComponent<SkillFactory>();
            skillView = GetComponent<SkillView>();
            SceneManager.activeSceneChanged += OnSceneLoaded;
            GameEventService.Instance.OnGameSceneChg += OnStartOfCombat;
            skillFactory = GetComponent<SkillFactory>();
            skillFactory.SkillsInit();
            passiveSkillFactory = GetComponent<PassiveSkillFactory>();
            passiveSkillFactory.PassiveSkillsInit();
            CreateSkillCardGO();
        }
        private void Start()
        {
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoaded;
           // OnSkillApply -= SkillEventtest;
            GameEventService.Instance.OnGameSceneChg -= OnStartOfCombat;
        }
        void OnSceneLoaded(Scene scene, Scene newScene)
        {   
            CreateSkillCardGO(); // set the skill card to the skill Service

            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                skillView = FindObjectOfType<SkillView>();
                _OnSkillApply += SkillEventtest;
                skillFXMoveController = gameObject.GetComponent<SkillFxMoveController>();
                if(skillFXMoveController == null)
                    skillFXMoveController = gameObject.AddComponent<SkillFxMoveController>();
                OnSOC();
            }             
        }
        void CreateSkillCardGO()
        {
            GameObject canvasGo = null; 
            currentScene = SceneManager.GetActiveScene();   
            GameObject[] rootObjects = currentScene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                if (obj.name == "Canvas")
                {
                    canvasGo = obj;
                }
            }
            if (skillCardGO == null)
            {
                skillCardGO = Instantiate(skillCardPrefab);
            }
            skillCardGO.transform.SetParent(canvasGo.transform);
            skillCardGO.transform.SetAsLastSibling();
            skillCardGO.transform.localScale = Vector3.one;
            skillCardGO.SetActive(false);
        }

        void OnSOC()
        {
            CombatEventService.Instance.OnSOT += SetDefaultSkillForChar;
            CombatEventService.Instance.OnCharOnTurnSet += InitEnemySkillSelection;
            CombatEventService.Instance.OnTargetClicked += TargetIsSelected;
            PostSkillApply += GridService.Instance.ClearOldTargetsOnGrid;// to be decided later to DEL or MOVE 
            CombatEventService.Instance.OnCharOnTurnSet += PopulateSkillTargets;
        }

        void OnStartOfCombat(GameScene gameScene)
        {
            //if (gameScene != GameScene.InCombat) return;
            //CombatEventService.Instance.OnSOT += SetDefaultSkillForChar;
            //CombatEventService.Instance.OnCharOnTurnSet += InitEnemySkillSelection; 
            //CombatEventService.Instance.OnTargetClicked += TargetIsSelected;
            //PostSkillApply += GridService.Instance.ClearOldTargetsOnGrid;// to be decided later to DEL or MOVE 
            //CombatEventService.Instance.OnCharOnTurnSet += PopulateSkillTargets;

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
       
        #endregion
    
        #region SKILL_INIT related
        public void SetDefaultSkillForChar()
        {
            CharController charOnTurn = CombatService.Instance.currCharOnTurn; 
            SkillController1 skillController = charOnTurn.GetComponent<SkillController1>();
            SkillModel defaultSkillModel = null; 
            foreach (SkillModel skillModel in skillController.charSkillModel.allSkillModels)
            {
                if(skillModel.GetSkillState() == SkillSelectState.Clickable)
                {
                    defaultSkillModel = skillModel;
                    defaultSkillName = defaultSkillModel.skillName; 
                    break; 
                }
            }           
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
        
        public void InitSkillControllers()// ON SOC 
        {
            foreach (CharController charCtrl in CharService.Instance.charsInPlayControllers)
            {
                SkillController1 skillController = charCtrl.GetComponent<SkillController1>(); 

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
                    Debug.LogError("SkillController is Null" + charCtrl.name); 
                }
                CharNames charName = charCtrl.GetComponent<CharController>().charModel.charName; 
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

            //Debug.Log("INIT SKILL CONTROLLER >>>>>>>>>>>>");
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

        #region EVENTS  

        public void On_SkillApplyFXMove()
        {
            OnSkillApplyMoveFx?.Invoke();
        }

        public void TargetIsSelected(DynamicPosData target, CellPosData cellPosData = null)
        {            
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
            PostSkillApply?.Invoke();
            Cleartargets();
        }

        public void On_SkillUsed(SkillEventData skillEventData)
        {
            Debug.Log("SKILL USED >>>>>" + skillEventData.skillModel.skillName); 
            OnSkillUsed?.Invoke(skillEventData);
        }
        void Cleartargets()
        {
            // char Death update here 
           // CharService.Instance.UpdateOnDeath();// EOt to Manage
            GridService.Instance.ClearOldTargetsOnGrid();
            CombatService.Instance.combatState = CombatState.INCombat_normal;
            
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
                .AppendCallback(()=>PostSkillApply?.Invoke())
                ;
            eventSeq.Play(); 
        }
        public void On_SkillSelected(CharNames _charName, SkillNames skillName)  // Ally Skill and perk "Skill Select" 
        {
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
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
            GridService.Instance.ClearOldTargetsOnGrid();          
        }
        public void DeSelectSkill()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charMode == CharMode.Enemy) return; // On Enemy turn u cannot deselect
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
            OnSkillHovered = null; SkillWipe = null;
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
            OnSkillHovered?.Invoke(); 
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

            CharController charController = CombatService.Instance.currCharOnTurn;  
            CombatController combatController = charController.GetComponent<CombatController>();
            // if ally reduce action pts
            if (skillModel != null)
            { // HASTE CHECK      
                bool hasteChk = false;
                if (skillModel.skillInclination == SkillInclination.Move && !ignoreHasteChk)
                    hasteChk = HasteChk(charController);

                // AP UPDATES 
                if (hasteChk) // if haste check /Enemies get a extra AP
                    combatController.actionPts++;

                combatController.SubtractActionPtOnSkilluse(skillModel, charController.charModel.charMode);

               currSkillController.UpdateAllSkillState();
            }else if (charController.charModel.charMode == CharMode.Enemy) // no SkillAvailable
            {
               // Debug.LogError("TURN MISSED" + charController.charModel.charName); 
                Move2Nextturn();
                return; 
            }

            if (combatController.actionPts > 0)// allies 
            {
                CombatService.Instance.roundController.SetSameCharOnTurn();
                if (charController.charModel.charMode == CharMode.Enemy)
                    InitEnemySkillSelection(CombatService.Instance.currCharOnTurn);   // to be called 
            }
            else
            {
                if (charController.charModel.charMode == CharMode.Enemy)
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

        public Sprite GetDefPoseSprite(CharController target)
        {
            SkillDataSO skillDataSO = GetSkillSO(target.charModel.charName);
            return skillDataSO.defendPose;

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
        public SkillPerkFXData GetSkillPerkFXData(PerkType perkType, CharMode targetCharMode)
        {
            // to be converted to max perk type after testing
            // .. current for lvl0 to be extended to the perks
            SkillDataSO skillDataSO = GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);
            List<SkillPerkFXData> allSkillPerkFXData =
                        skillDataSO.allSkills.Find(t => t.skillName == currSkillName).allSkillFXs;


            int index = allSkillPerkFXData.FindIndex(t => t.charMode == targetCharMode && t.perkType == perkType); 

            if (index != -1)
                return allSkillPerkFXData[index];
            else
                Debug.Log(" FX Not found!!!!!" + currSkillName); 
            return null;
        }
        public SkillDataSO GetSkillSO(CharNames _charName)
        {
            SkillDataSO skillDataSO = allSkillDataSO.Find(x => x.charName == _charName);
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
            foreach (SkillDataSO skillDataSO in allSkillDataSO)
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
            foreach (SkillModel skillModel in skillController.charSkillModel.allSkillModels)
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

        #endregion

        #region APPLY REMOTE SKILLS
        
        public void SetRemoteSkills(SkillModel skillModel, CellPosData cellPosData)
        {
            
            CharController charController = CombatService.Instance.currCharOnTurn; 
            if(charController.combatController.actionPts > 0)
            {
                SkillBase skillBase = currSkillController.GetSkillBase(skillModel.skillName);
                IRemoteSkill iRemote = skillBase as IRemoteSkill; 
                if(iRemote != null)
                {
                    iRemote.Init(cellPosData);
                    skillBase.OnRemoteUse(); 
                }
            }
           
            ClearPrevData();
            Cleartargets(); // move to the next turn
            DeSelectSkill();
        }

        #endregion

        #region Helpers
        public AttackType GetSkillAttackType(SkillNames skillName)
        {
            SkillModel skillModel = currSkillController.GetSkillModel(skillName);
            if (skillModel != null)
                return skillModel.attackType; 
            Debug.Log("SKILLMODEL Not found!!" + skillName);
            return AttackType.None; 
        }
        void ClearPrevData()
        {
            _OnSkillApply = null; SkillFXRemove = null; OnSkillApplyMoveFx = null;
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

            for (int i = OnSkillApplyMoveFx.GetInvocationList().Length - 1; i >= 0; i--)
            {
                var outputMsg = OnSkillApplyMoveFx.GetInvocationList()[i];

                Debug.Log("INVOKE  MOVE FX" + outputMsg.Method);
            }
        }
        #endregion
        private void Update()
        {
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                if (CombatService.Instance.combatState == CombatState.INTactics)
                    return;
                if (Input.GetMouseButtonDown(1))
                {
                    DeSelectSkill();
                }
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveState();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadState();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                ClearState();
            }
        }

        #region SAVE and LOAD

        public void Init()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    allCharSkillModel.Clear(); 
                }
                else
                {
                    LoadState();
                }
            }
            else
            {
                Debug.LogError("Service Directory missing" + path);
            }
        }
        public void LoadState()
        {    
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            // SKILL DATA LOAD
            allCharSkillModel.Clear(); 
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);  
                    CharSkillModel charSkillModel = JsonUtility.FromJson<CharSkillModel>(contents);
                    
                    allCharSkillModel.Add(charSkillModel);
                }
                foreach (CharController charCtrl in CharService.Instance.charsInPlayControllers)
                {
                    SkillController1 skillController = charCtrl.skillController;
                    
                    CharSkillModel charSkillModel = allCharSkillModel
                                .Find(t => t.allSkillModels[0].charID == charCtrl.charModel.charID);
                    skillController.charSkillModel = charSkillModel.DeepClone();  // inits the models 
                    skillController.LoadSkillList(charCtrl);// also Loads perk list                 
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }
        public void ClearState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
           
            DeleteAllFilesInDirectory(path);
        }
        public void SaveState()
        {
            if (CharService.Instance.charsInPlayControllers.Count <= 0)
            {
                Debug.LogError("no chars in play"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
         
            ClearState();

            foreach (CharController charCtrl in CharService.Instance.charsInPlayControllers)
            {
                SkillController1 skillController = charCtrl.skillController;
                CharSkillModel charSkillModel = skillController.charSkillModel; 
                string charSkillModelJSON = JsonUtility.ToJson(charSkillModel);
                string fileName = path 
                    + charCtrl.charModel.charName.ToString()+ ".txt";
                File.WriteAllText(fileName, charSkillModelJSON);                          
            }
        }
        #endregion   

    }
}

