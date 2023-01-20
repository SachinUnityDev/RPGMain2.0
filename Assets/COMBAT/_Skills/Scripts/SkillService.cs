using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.Linq;
using DG.Tweening; 
using Common;
using UnityEngine.UI;
using TMPro;
using System.Security.Policy;

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
        public float combatSpeed = 1f;

        public event Action<PerkData> OnPerkStateChg;
        public event Action<SkillModel> OnSkillSelectInInv; 
        #region Initializers

        [Header("SKill Factory NTBR")]
        public SkillFactory skillFactory;

        //[Header("SkillCard Data")]
        //public SkillCardData skillCardData;
        [Header("CURR SKILLMODEL")]
        public SkillModel skillModelHovered;
        public SkillModel skillModelSelect; 

        [Header("ALL SO")]
        public List<SkillDataSO> allCharSkillSO = new List<SkillDataSO>();
        public SkillViewSO skillViewSO; 
        public SkillHexSO skillHexSO;   
        public GameObject skillCardGO; 

        [Header("ALL SKILL MANAGER")]
        public List<SkillController1> allSkillControllers = new List<SkillController1>();   
        public SkillController1 currSkillController = new SkillController1();

        [Header("ALL CTRL AI RELATED")]
       // public List<SkillAIController> allSKillAIControllers = new List<SkillAIController>();

        [Header("SKILL MOVE AND FX RELATED")]
        public SkillFxMoveController skillFXMoveController;
      //  public SkillFXController skillFXController; 

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
       // public CharNames currCharName;
        public SkillNames currSkillName = SkillNames.None;
        public DynamicPosData currentTargetDyna = new DynamicPosData();
        public DynamicPosData currStrikerDyna = new DynamicPosData(); 
        public SkillNames defaultSkillName;

        public SkillNames currSkillHovered;

        SkillServiceView skillServiceView;


      
      
        public int currSkillPts =10;

        [Header("Perk Selection Controller")]
        [SerializeField] PerkSelectionController perkSelectionController;



        //[Header("TEMP text display")]
        //public TextMeshProUGUI temptxt;
        private event Action _SkillApply = null;
        public event Action SkillApply
        {
            add
            {
                if (_SkillApply == null ||!_SkillApply.GetInvocationList().Contains(value))
                {
                    _SkillApply += value;
                }
                else
                {
                    Debug.Log("Duplicate >>");
                }
            }
            remove
            {
                _SkillApply -= value;
            }
        }


        protected override void Awake()
        {
            base.Awake();
     
            skillFactory = GetComponent<SkillFactory>();
            skillServiceView = GetComponent<SkillServiceView>();
         
            //move and FX controller 
            skillFXMoveController = gameObject.GetComponent<SkillFxMoveController>();
            // skillFXController = gameObject.GetComponent<SkillFXController>();
   
           
           // CombatEventService.Instance.OnCombatInit += skillFactory.SkillsInit;
           // CombatEventService.Instance.OnCombatInit += skillFactory.InitPerks; 
           //  CombatEventService.Instance.OnCombatInit += InitSkillControllers;

           

            //CombatEventService.Instance.OnSOR += InitSkillManagers; // for enemies
        }
        void Start()
        {
            InitSkillControllers();
            // Cn be later Set to the start of Combat Event
          
            SkillApply += SkillEventtest;
            GameEventService.Instance.OnCombatBegin += OnStartOfCombat; 
            // CombatService.Instance.GetComponent<RoundController>().OnCharOnTurnSet += PopulateSkillTargets; 
        }

        void OnStartOfCombat()
        {
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
            skillModelSelect = skillModel;
            OnSkillSelectInInv?.Invoke(skillModel); 
        }
        void SkillDisplay()  // some reference is there for SKILL DISPLAY ON TOP
        {
            
          //  temptxt.text = "SkillName" + currSkillName; 
        }

        #endregion
    
        #region SKILL_INIT related

        public void SetDefaultSkillForChar()
        {
            SkillDataSO skillDataSo = GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);
             defaultSkillName =  skillDataSo.allSkills[0].skillName; 
        }

        public void PopulateSkillTargets(CharController charController)
        {
            //if (charController.charModel.charMode == CharMode.Enemy) return; 
           SkillController skillController = charController.gameObject.GetComponent<SkillController>();
            //skillController.allSkillBases.ForEach(t => Debug.Log("SKILL BASES ARE HEALTHY" +t.charName + t.skillName));
            //skillController.allSkillModels.ForEach(t => Debug.Log(t.skillName +"SkillBase" + t.targetPos.Count));
            
             skillController.allSkillBases.ForEach(t => t.PopulateTargetPos());
            if(currCharMode == CharMode.Ally)
             skillController.allPerkBases.ForEach(t => t.AddTargetPos());
             skillController.CheckNUpdateSkillState(); 
        }

        public void InitSkillControllers()
        {
            // CombatService.Instance.AddCombatControllers();
            //foreach (var character in CharService.Instance.charsInPlay)
            //{
            //    if (character.GetComponent<SkillController1>() == null)
            //    {
            //        SkillController1 skillController = character.gameObject.AddComponent<SkillController1>();
            //        allSkillControllers.Add(skillController);

            //        skillController.InitPerkDataList();


            //        //SkillAIController skillAIController = character.gameObject.AddComponent<SkillAIController>();
            //        //allSKillAIControllers.Add(skillAIController); 
            //    }
            //}
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

        public void TargetIsSelected(DynamicPosData target)
        {

            // FOCUS CHECK TO BE INCORPORPORATED HERE 

            if (CombatService.Instance.combatState != CombatState.INCombat_InSkillSelected)
            {
                Debug.Log("Combat State not in skill selected"); return;
            }

            int currCharID = CombatService.Instance.currCharOnTurn.charModel.charID; 
            SkillModel skillModel = GetSkillModel(currCharID, currSkillName);
            CharController targetController = null; 
            if(skillModel.skillType != SkillTypeCombat.Move)
            {
                currentTargetDyna = target;
                StrikeController strikeController =
                            CombatService.Instance.currCharOnTurn.GetComponent<StrikeController>();
                 targetController = target.charGO.GetComponent<CharController>();
                List<DamageType> dmgTypes = skillModel.dmgType;
                bool isPhysical = dmgTypes.Any(t => t == DamageType.Physical);
                bool isMagical = dmgTypes.Any(t => t == DamageType.Air || t == DamageType.Dark
                || t == DamageType.Earth || t == DamageType.Fire || t == DamageType.Light
                || t == DamageType.Water);
                //if (isMagical)
                //{
                //    if (strikeController.FocusCheck())
                //    { // MISFIRE 
                //        CombatService.Instance.isFocusChecked = true;
                //        strikeController.MisFireApply();

                //    }
                //        //####### modify MAIN TARGET and CURR TARGET DYNAS ....... ensure no dyna set in skill APPLY ########
                //}
                //if (isPhysical)
                //{
                //    if (strikeController.AccuracyCheck())
                //    {
                //        CombatService.Instance.isAccChecked = true;
                //    }
                //    bool isDodged = targetController.damageController.HitChance();
                //   // IF SKILL HAS TRUE HIT CANNOT i.e.isDodge = false;

                //    if (isDodged)
                //        Debug.Log("FX is Dodged");
                //    return;   // CAN INSERT DODGE FX HERE
                //    // get skill type DamageType.. Acc and Focus Check 
                //}

                // only target pos/tile can be clicked
                if (!skillModel.targetPos.Any(t => t.pos == target.currentPos))
                    //&& t.charMode == target.charMode))
                    return;
                Debug.Log("TARGET SELECT" + target.charGO.name);
            }

          
            OnSkillUsed?.Invoke(new SkillEventData(CombatService.Instance.currCharOnTurn
                                    , targetController, currSkillName, skillModel));

            PreSkillApply?.Invoke();
            SkillFXRemove?.Invoke();
           // Sequence SkillSeq = DOTween.Sequence();
            _SkillApply.Invoke();
            On_PostSkillApply(); 
        }

        void On_PostSkillApply()
        {
            // char Death update here 
           // CharacterService.Instance.UpdateOnDeath(); 
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

            OnSkillUsed?.Invoke(new SkillEventData(CombatService.Instance.currCharOnTurn
                                    , targetController, currSkillName, skillModel));

            if (_SkillApply == null) return;


            PreSkillApply?.Invoke();
            SkillFXRemove?.Invoke();

            Sequence eventSeq = DOTween.Sequence();

            eventSeq
                .AppendCallback(() => _SkillApply?.Invoke())
                .AppendInterval(2)
                .AppendCallback(On_PostSkillApply)
                ;
            eventSeq.Play(); 
        


        }

        public void On_SkillSelected(CharNames _charName, SkillNames skillName)  // Ally Skill and perk "Skill Select" 
        {

            currSkillController = allSkillControllers.FirstOrDefault(t => t.charName == _charName);
            Debug.Log("SKILL IS SELECTED" + currSkillController.name);
            currSkillController.SkillSelect(skillName);

            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                ClearPrevData();

                SkillSelect?.Invoke(_charName, skillName);  // message broadcaster 
            }
          
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
            skillModel.SetSkillState(SkillSelectState.Clickable);
            currSkillName = SkillNames.None;
            skillServiceView.SetSkillsPanel(charID);
            skillServiceView.PopulateSkillClickedState(-1);
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

        public void On_PostSkill()
        {
           // ClearPrevData();  // redundant safety .. causing only one FX to play as it clears mainTargetDyna



            CombatEventService.Instance.On_EOT();
            Sequence PauseSeq = DOTween.Sequence();

            PauseSeq.AppendInterval(1f)
            //.AppendCallback(ClearPrevData)
                .AppendCallback(CombatEventService.Instance.On_SOT)
                .AppendInterval(1f)
                ;
            PauseSeq.Play();

            // CombatEventService.Instance.On_SOT();
            Debug.Log("AUTO MOVE FORWARDS >>>>>>>>>>>>" + currSkillName);
        }
        public void PerkUnLock(PerkNames _perkName, GameObject btn)
        {       
           // shifted to the skillController    
                         
               
        }
        //public void ChangePerkState(PerkNames _perkName, PerkSelectState _state)
        //{
        //    allSkillPerksData.Find(t => t.perkName == _perkName).state = _state;
        //}

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

            //if()
            //SkillPerkFXData skillPerkFXData = 
            //            allSkillPerkFXData.Find(t => t.charMode == targetCharMode && t.perkType == perkType); 

            //return skillPerkFXData; 
            return allSkillPerkFXData[0];
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
            SkillController skillController = charController.GetComponent<SkillController>(); 
          //  Debug.Log("skillcontroller found" + skillController.allSkillBases.Count);
            foreach (SkillBase skill in skillController.allSkillBases)
            {
                if (skill.skillName == _skillName)
                {
                    return skill.skillModel; 
                }
            }
           Debug.Log("SkillModel Not found" + _skillName); 
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

        #region Helpers
      

        public AttackType GetSkillAttackType(SkillNames _skillName)
        {


            return AttackType.None; 
        }

        void ClearPrevData()
        {
           
            _SkillApply = null; SkillFXRemove = null; SkillApplyMoveFx = null;
            PostSkillApply = null;
            PreSkillApply = null;
            CombatService.Instance.mainTargetDynas.Clear();
            CombatService.Instance.colTargetDynas.Clear();

            PostSkillApply += GridService.Instance.ClearOldTargets;
            PostSkillApply += ()=> CombatService.Instance.combatState = CombatState.INCombat_normal;

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
          //  temptxt.text =  currSkillName.ToString();
            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    foreach (SkillController1 skillController in allSkillControllers)
            //    {
            //        CharModel charModel = skillController.gameObject.GetComponent<CharController>().charModel; 

            //        int charID  = charModel.charID;
            //        Debug.Log("CHAR ID " + charID  +"Char NAME " + charModel.charName); 

            //    }
            //}

            // NOT TO BE ERASED 
          //  if (CombatService.Instance.combatState == CombatState.INCombat_InSkillSelected)
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    DeSelectSkill();
                }
            }
        }
    }


}


// SkillApply?.Invoke();
//// Debug.Log("Target selected" + currentTargetDyna.charGO.name);
// PostSkillApply?.Invoke();
//OnSkillUsed?.Invoke(new SkillEventData(CombatService.Instance.currCharOnTurn
//                           , targetController, currSkillName, skillModel));
//CharController charController = CharacterService.Instance.GetGO4CharID(skillModel.charID).GetComponent<CharController>();
//if(charController.charModel.charMode == CharMode.Ally)
//{
//    Debug.Log("ALLY in auto mode attempted");
//    return;
//}

//public bool IsSkillApplySubcribed()
//{
//    foreach (Action del in PostSkillApply.GetInvocationList())
//    {
//        Debug.Log("SKILL APPLY SUBS" + del.Method.Name);
//        //if(name == del.Method.Name)
//        //    return true; 
//    }
//    return false; 

//}

// @@@@@@@@@@@@@@@@@ SUBSCRIPTION TESTING@@@@@@@@@@@@@@@@@@@@@@@
//  for (int j = _SkillApply.GetInvocationList().Length - 1; j >= 0; j--)
//  {
//      var outputMsg = _SkillApply.GetInvocationList()[j];

//      Debug.Log(outputMsg.GetType().Name + "INVOKE " + outputMsg.Method.Name);
//  }


////  SkillApply?.Invoke();
//   //SkillEventtest();


//PerkModelData GetMaxPerk(SkillNames _skillName)
//{
//    // highest lvl of clicked
//    List<PerkModelData> myList = GetAllPerkdata(_skillName); 
//    int count = myList.Count; 
//    for (int i = count-1; i >= 0 ; i--)
//    {
//        if (myList[i].state == PerkSelectState.Clicked)
//        {
//            return myList[i]; 
//        }
//    }
//    return null;             
//}


//public class SkillModelData
//{
//    public SkillNames skillName;
//    public Type skillBase;
//    public SkillSelectState skillstate;
//    public SkillModelData(Type _skillBase, SkillNames _skillName, SkillSelectState _skillState)
//    {
//        skillBase = _skillBase;
//        skillName = _skillName;
//        skillstate = _skillState; 
//    }
//}