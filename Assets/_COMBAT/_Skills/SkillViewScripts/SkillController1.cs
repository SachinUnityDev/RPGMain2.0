
using Common;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Combat
{
    public class SkillController1 : MonoBehaviour
    {
        [SerializeField] CharMode charMode;
        public CharController charController;
        public CharNames charName;
        
        public int currSkillID;        

        [Header("Skill Perk Data")]
        public List<PerkData> allSkillPerkData = new List<PerkData>();
 

        [Header("All Skill and UnLocked Skill list")]
        public List<SkillNames> unLockedSkills = new List<SkillNames>();

        [Header("Skill Model")]
        public List<SkillModel> allSkillModels = new List<SkillModel>();
        public List<SkillModel> ClickableSkills = new List<SkillModel>();

        [Header("Skill and Perk Bases")]
        public List<SkillBase> allSkillBases = new List<SkillBase>();
        public List<PerkBase> allPerkBases = new List<PerkBase>();
        [SerializeField] int skillbaseCount =-1;
        [SerializeField] int perkBaseCount = -1;

        SkillDataSO skillDataSO;
        SkillView skillView;

        [Header("Skill DmgMod Buffs")]
        public List<SkillDmgModData> allSkillDmgMod = new List<SkillDmgModData>();

        [Header(" Passive Skill controller")]
        public PassiveSkillsController passiveSkillcontroller;


        private void OnEnable()
        {
            charController = gameObject.GetComponent<CharController>();
            charController.skillController = this; 
            charName = charController.charModel.charName;
           
        }

        private void Start()
        {
            
            charController = GetComponent<CharController>();
            InitSkillList(charController); 
            //CombatEventService.Instance.OnSOC1 += InitAllSkill_OnCombat;

         //   CombatEventService.Instance.OnCombatInit += () => InitAllSkill_OnCombat(CombatState.INCombat_normal);

            CombatEventService.Instance.OnEOC += OnEOCReset;
           // CombatEventService.Instance.OnCharOnTurnSet += UpdateAllSkillState;            
            // CharService.Instance.OnCharAddedToParty += InitSkillList;
            SceneManager.sceneLoaded += OnSceneLoaded;
            QuestEventService.Instance.OnEOQ += EOQTick;
            CombatEventService.Instance.OnEOC -= OnEOCReset;
            if (skillView == null)
                skillView = FindObjectOfType<SkillView>();
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            CharService.Instance.OnCharSpawn -= InitSkillList;
            
            CombatEventService.Instance.OnEOR1 -= RoundTick;
            CombatEventService.Instance.OnEOC -= EOCTick;
            QuestEventService.Instance.OnEOQ -= EOQTick;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                if (skillView == null)
                    skillView = FindObjectOfType<SkillView>();

                //  InitAllSkill_OnCombat(CombatState.INCombat_normal);
                CombatEventService.Instance.OnSOTactics += InitAllSkill_OnCombat;
                CombatEventService.Instance.OnSOC += InitAllSkill_OnCombat;

                CombatEventService.Instance.OnEOR1 += RoundTick;
                CombatEventService.Instance.OnEOC += EOCTick;
            }
        }
  

        public void InitAllSkill_OnCombat()
        {
            if (!CharService.Instance.allCharInCombat.Any(t => t.charModel.charID == charController.charModel.charID))
                return; // if not in combat List return
            CharMode charMode = charController.charModel.charMode;
            Debug.Log("COMBAT STATE" + charController.charModel.charID);
            if (charMode == CharMode.Enemy)
            {
                InitSkillList(charController);
            }
            AllSkillInit();
        }

        void AllSkillInit()
        {
            foreach (SkillBase skillBase in allSkillBases)
            {
                skillBase.SkillInit(this);
            }
        }
        public void InitSkillList(CharController charController) 
        { 
            if (this.charController.charModel.charID != charController.charModel.charID) return;
            // stop double run// overrides 
            if (allSkillModels.Count > 0) return; 
            skillDataSO = SkillService.Instance.GetSkillSO(this.charName);
            foreach (SkillData skill in skillDataSO.allSkills)
            {
                
                if (skill.skillUnLockStatus == 1) // 1 = unlock, 0 locked, -1 NA
                {
                    unLockedSkills.Add(skill.skillName);
                }
            }
            foreach (SkillData skillSO in skillDataSO.allSkills)
            {
                SkillBase skillbase = SkillService.Instance.skillFactory.GetSkill(skillSO.skillName);
                Debug.Log("skill base" + skillSO.skillName);
                skillbase.charName = skillDataSO.charName;
              
                allSkillBases.Add(skillbase);
              
                skillbase.SkillInit(this); // pass in all the params when all skills are coded
            }
            skillbaseCount = allSkillBases.Count; 
            InitPerkDataList();
        }
        public void InitPerkDataList()
        {
            foreach (SkillNames _skillName in unLockedSkills)
            {
                List<PerkBaseData> skillPerkData =   
                         SkillService.Instance.skillFactory.GetSkillPerkData(_skillName);
                if (skillPerkData != null)
                {
                    foreach (PerkBaseData perkData in skillPerkData)
                    {
                        PerkBase P1 = SkillService.Instance.skillFactory
                                            .GetPerkBase(perkData.skillName, perkData.perkName);

                        Debug.Log("PERKANME..." + P1.perkName);
                        allPerkBases.Add(P1);// perk bases
                        P1.SkillInit(this);

                        PerkData perkModel = new PerkData(P1.skillName, P1.perkName, P1.state,
                            P1.perkType, P1.skillLvl, P1.preReqList);
                        allSkillPerkData.Add(perkModel);  // model data captures state and lvl
                      //  SetPerkState(perkModel);
                    }
                }
            }
           
            perkBaseCount = allPerkBases.Count;
        }

  
        #region On_Hover, On_SkillSelect and Check coolDown

        public void SkillHovered(SkillNames _skillName)
        {
            if (_skillName == SkillNames.None) return;
            SkillService.Instance.skillModelHovered.perkChain.Clear();
            //  SkillService.Instance.skillModelHovered.descLines.Clear();
            Debug.Log(" all Skill bases count" + allSkillBases.Count +
                            "all perk bases count" + allPerkBases + " SKillname" + _skillName);

            allSkillBases.Find(t => t.skillName == _skillName).SkillHovered();

            List<PerkData> clickedPerkList = allSkillPerkData
                .Where(t => t.skillName == _skillName && t.state == PerkSelectState.Clicked).ToList();

            clickedPerkList.ForEach(t => SkillService.Instance.skillModelHovered.perkChain.Add(t.perkType));

            clickedPerkList.ForEach(t => allPerkBases.Find(x => x.perkName == t.perkName).SkillInit(this));
            clickedPerkList.ForEach(t => allPerkBases.Find(x => x.perkName == t.perkName).SkillHovered());
        }
        public void SkillSelect(SkillNames _skillName)
        {
            allSkillBases.Find(t => t.skillName == _skillName).SkillSelected();

            List<PerkData> clickedPerkList = allSkillPerkData
                .Where(t => t.skillName == _skillName && t.state == PerkSelectState.Clicked).ToList();


            foreach (var skillPerkdata in allSkillPerkData)
            {
                if ((skillPerkdata.skillName == _skillName) && (skillPerkdata.state == PerkSelectState.Clicked))
                {
                    foreach (var perkbase in allPerkBases)
                    {
                        if (perkbase.perkName == skillPerkdata.perkName)
                        {
                            clickedPerkList.ForEach(t => Debug.Log(t.perkName + "PERK BASE CLICKED"));                            
                            perkbase.SkillSelected();
                        }
                    }
                }
            }
        }

        #endregion

        #region GETTERS skillmodel, skillbase, skillPerkData, perkBase
        public List<PerkData> GetSkillPerkData(SkillNames _skillName)
        {
            List<PerkData> allPerkData = 
                    allSkillPerkData.Where(t => t.skillName == _skillName).ToList();
            if(allPerkData.Count >  0)
                return allPerkData;
            else
            {
                Debug.Log("Skill Perk data not found"+ _skillName);
            }
            return null; 
        }
        public SkillModel GetSkillModel(SkillNames skillName)
        {
            SkillModel skillModel = allSkillModels.Find(t => t.skillName == skillName); 
            if(skillModel != null)
            {
                return skillModel;
            }
            else
            {
                Debug.Log("SkillModel No found !" + skillName);
                return null;
            }
        }
        public SkillBase GetSkillBase(SkillNames skillName)
        {
            SkillBase skillBase = allSkillBases.Find(t => t.skillName == skillName);
            if (skillBase != null)
            {
                return skillBase;
            }
            else
            {
                Debug.Log("SkillModel No found !" + skillName);
                return null;
            }
        }
        public PerkBase GetPerkBase(SkillNames skillName, PerkNames perkName)
        {
            PerkBase perkBase = allPerkBases.Find(t => t.skillName == skillName 
                                                  && t.perkName == perkName);
            if (perkBase != null)
            {
                return perkBase;
            }
            else
            {
                Debug.Log("SkillModel No found !" + skillName);
                return null;
            }
        }
        #endregion

        #region PERK CLICK AND PERK STATE CHANGE
        public void OnPerkClicked(PerkData ClickedPerkData)
        {   
            if(ClickedPerkData != null)
            {
               if(ClickedPerkData.state == PerkSelectState.Clickable)
                {
                    if (IsPrevLvlClicked(ClickedPerkData))
                        UpdateDataPerkState(ClickedPerkData.perkName, PerkSelectState.Clicked);
                    else return;

                    //if (charController.charModel.skillPts > 0)
                    //    charController.charModel.skillPts--;
                    //else return;
                    SetPerkState(ClickedPerkData);
                    
                }
            }
            //SkillViewService.. Update skillBtn State.. skill points in view 
         

        }        
        void SetSameLvlPerkUnClickable(PerkData perkData)
        {
            SkillNames skillName = perkData.skillName;
            foreach (PerkData perk in allSkillPerkData)
            {
                if (perk.skillName != skillName) continue;
                if(perk.perkName != perkData.perkName)
                {
                    if(perk.perkLvl == perkData.perkLvl)
                    {
                        UpdateDataPerkState(perk.perkName, PerkSelectState.UnClickable);
                        //UpdatePerkRel(perk);
                    }
                }
            }
        }
        void SetPerkState(PerkData clickedPerkData) //  on perk clicked extn
        {
            SkillNames skillName = clickedPerkData.skillName;
            if (clickedPerkData.state == PerkSelectState.Clicked)
            {
                SetSameLvlPerkUnClickable(clickedPerkData);
            }
            
            foreach (PerkData perk in allSkillPerkData)
            {
                if (perk.skillName != skillName) continue; 
                SkillLvl nextlvl = clickedPerkData.perkLvl + 1;
                if ((int)nextlvl > 3) continue;
                if (perk.perkLvl == nextlvl)
                {
                    foreach (PerkNames perkName in perk.preReqList)
                    {
                        if (perkName == PerkNames.None || perkName == clickedPerkData.perkName
                            || GetPerkData(perkName).state == PerkSelectState.Clicked)
                        {
                            UpdateDataPerkState(perk.perkName, PerkSelectState.Clickable);//update
                        }
                        else 
                        {
                            UpdateDataPerkState(perk.perkName, PerkSelectState.UnClickable);
                            break; // if either of pre reqs not there perk is unclickable
                        }
                    }
                }
            }
            foreach (PerkData perk in allSkillPerkData)
            {
                if (perk.skillName != skillName) continue;
                SkillLvl nextlvl = clickedPerkData.perkLvl + 2;
                if ((int)nextlvl > 3) continue;
                bool isClickable = false; 
                if (perk.perkLvl == nextlvl)
                {
                    foreach (PerkNames perkName in perk.preReqList)
                    {
                        if (perkName == PerkNames.None 
                            || GetPerkData(perkName).state == PerkSelectState.Clickable
                            || GetPerkData(perkName).state == PerkSelectState.Clicked)
                        {
                            isClickable = true;
                        }
                        else
                        {
                            isClickable = false; 
                            UpdateDataPerkState(perk.perkName, PerkSelectState.UnClickable);
                           
                        }
                    }
                    if (isClickable)
                    {
                        UpdateDataPerkState(perk.perkName, PerkSelectState.Clickable);//update
                    }
                }
            }
            foreach (PerkData perk in allSkillPerkData)
            {
                UpdatePerkRel(perk);
            }
            //UpdatePipeRel();
        }
        void UpdateDataPerkState(PerkNames _perkName, PerkSelectState _state)
        {
            if (_perkName == PerkNames.None) return;
            foreach (var perkbase in allPerkBases)
            {
                if (perkbase.perkName == _perkName)
                {
                    perkbase.state = _state;
                }
            }
            PerkData perkData = GetPerkData(_perkName);
            perkData.state = _state;
         
            SkillService.Instance.On_PerkStateChg(perkData);
        }

        #endregion 




   
        public bool IsPerkClickable(SkillNames skillName, PerkNames perkName)
        {
            int lvl = 0; 
            foreach (PerkData pData in allSkillPerkData)
            {
                if(pData.state == PerkSelectState.Clicked && pData.skillName == skillName)
                {
                    if (lvl <= (int)pData.perkLvl)
                        lvl = (int)pData.perkLvl; 
                }
            }
            PerkData perkData = GetPerkData(perkName);
            if ((int)perkData.perkLvl == (lvl + 1)) return true; 
            return false; 
        }

        public void OnPerkHovered(PerkNames perkName)
        {

        }
   
        public void UpdatePerkRel(PerkData clickedPerkData)
        {
            SkillNames skillName = clickedPerkData.skillName;
            foreach (PerkData perk in allSkillPerkData)
            {
                if (perk.skillName != skillName) continue;
               
           
                    SkillLvl nextlvl = clickedPerkData.perkLvl + 1;
                    if ((int)nextlvl > 3) continue;
                    //NEXT LVL
                    if(clickedPerkData.perkName == PerkNames.HuntingSeason)
                    {
                        Debug.Log("XX"); 
                    }
                    if (perk.perkLvl == nextlvl)
                    {
                        if (perk.perkType == PerkType.A2 || perk.perkType == PerkType.B2)
                        {
                            if (clickedPerkData.state == PerkSelectState.Clicked)
                            {
                                switch (perk.state)
                                {
                                    case PerkSelectState.Clickable:
                                        if (perk.perkType == PerkType.A2)
                                            clickedPerkData.pipeRel[0] = 1;
                                        if (perk.perkType == PerkType.B2)
                                            clickedPerkData.pipeRel[1] = 1;
                                        break;
                                    case PerkSelectState.Clicked:
                                        if (perk.perkType == PerkType.A2)
                                            clickedPerkData.pipeRel[0] = 2;
                                        if (perk.perkType == PerkType.B2)
                                            clickedPerkData.pipeRel[1] = 2;
                                        break;
                                    case PerkSelectState.UnClickable:
                                        if (perk.perkType == PerkType.A2)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B2)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (clickedPerkData.state == PerkSelectState.Clickable)
                            {
                                switch (perk.state)
                                {
                                    case PerkSelectState.Clickable:
                                        if (perk.perkType == PerkType.A2)
                                            clickedPerkData.pipeRel[0] = 1;
                                        if (perk.perkType == PerkType.B2)
                                            clickedPerkData.pipeRel[1] = 1;
                                        break;
                                    //case PerkSelectState.Clicked:
                                    //    if (perk.perkType == PerkType.A2)
                                    //        clickedPerkData.pipeRel[0] = 2;
                                    //    if (perk.perkType == PerkType.B2)
                                    //        clickedPerkData.pipeRel[1] = 2;
                                    //    break;
                                    case PerkSelectState.UnClickable:
                                        if (perk.perkType == PerkType.A2)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B2)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (clickedPerkData.state == PerkSelectState.UnClickable)
                            {
                                switch (perk.state)
                                {
                                    case PerkSelectState.Clickable:
                                        if (perk.perkType == PerkType.A2)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B2)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    //case PerkSelectState.Clicked:
                                    //    if (perk.perkType == PerkType.A2)
                                    //        clickedPerkData.pipeRel[0] = 2;
                                    //    if (perk.perkType == PerkType.B2)
                                    //        clickedPerkData.pipeRel[1] = 2;
                                    //    break;
                                    case PerkSelectState.UnClickable:
                                        if (perk.perkType == PerkType.A2)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B2)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (perk.perkType == PerkType.A3 || perk.perkType == PerkType.B3)
                        {
                            if (clickedPerkData.state == PerkSelectState.Clicked)
                            {
                                switch (perk.state)
                                {
                                    case PerkSelectState.Clickable:
                                        if (perk.perkType == PerkType.A3)
                                            clickedPerkData.pipeRel[0] = 1;
                                        if (perk.perkType == PerkType.B3)
                                            clickedPerkData.pipeRel[1] = 1;
                                        break;
                                    case PerkSelectState.Clicked:
                                        if (perk.perkType == PerkType.A3)
                                            clickedPerkData.pipeRel[0] = 2;
                                        if (perk.perkType == PerkType.B3)
                                            clickedPerkData.pipeRel[1] = 2;
                                        break;
                                    case PerkSelectState.UnClickable:
                                        if (perk.perkType == PerkType.A3)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B3)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (clickedPerkData.state == PerkSelectState.Clickable)
                            {
                                switch (perk.state)
                                {
                                    case PerkSelectState.Clickable:
                                        if (perk.perkType == PerkType.A3)
                                            clickedPerkData.pipeRel[0] = 1;
                                        if (perk.perkType == PerkType.B3)
                                            clickedPerkData.pipeRel[1] = 1;
                                        break;
                                    //case PerkSelectState.Clicked:
                                    //    if (perk.perkType == PerkType.A2)
                                    //        clickedPerkData.pipeRel[0] = 2;
                                    //    if (perk.perkType == PerkType.B2)
                                    //        clickedPerkData.pipeRel[1] = 2;
                                    //    break;
                                    case PerkSelectState.UnClickable:
                                        if (perk.perkType == PerkType.A3)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B3)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (clickedPerkData.state == PerkSelectState.UnClickable)
                            {
                                switch (perk.state)
                                {
                                    case PerkSelectState.Clickable:
                                        if (perk.perkType == PerkType.A3)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B3)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    //case PerkSelectState.Clicked:
                                    //    if (perk.perkType == PerkType.A2)
                                    //        clickedPerkData.pipeRel[0] = 2;
                                    //    if (perk.perkType == PerkType.B2)
                                    //        clickedPerkData.pipeRel[1] = 2;
                                    //    break;
                                    case PerkSelectState.UnClickable:
                                        if (perk.perkType == PerkType.A3)
                                            clickedPerkData.pipeRel[0] = 3;
                                        if (perk.perkType == PerkType.B3)
                                            clickedPerkData.pipeRel[1] = 3;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }                
                }
           
            }

        }


        #region GET PerkData, clickedPerksList, isPreLvlClickedCheck, 

        public PerkData GetPerkData(PerkNames _perkName)
        {
            if (_perkName == PerkNames.None) return null;   
            PerkData perkData = 
                    allSkillPerkData.Find(t => t.perkName == _perkName);
            if (perkData != null)
                return perkData;
            else
                Debug.Log("perk data not found" + _perkName);
            return null; 
        }
        public List<PerkData> GetClickedPerks(SkillNames _skillName)
        {
            List<PerkBaseData> perks = new List<PerkBaseData>(); 
              
            List<PerkData> allPerks = allSkillPerkData.Where(t => t.skillName == _skillName 
                                                        && t.state ==PerkSelectState.Clicked).ToList();
            
            return allPerks;
        }

        public bool IsPerkClicked(SkillNames skillName, PerkNames perkName)
        {
            List<PerkData> perks = GetClickedPerks(skillName); 
            return perks.Any(t=>t.preReqList.Contains(perkName) && t.state == PerkSelectState.Clicked);
        }
        bool IsPrevLvlClicked(PerkData perkData)
        {

            SkillLvl perkLvl = perkData.perkLvl;
            if (perkLvl == SkillLvl.Level1) return true; 
            List<PerkData> clickedPerks = GetClickedPerks(perkData.skillName);
            if (clickedPerks.Any(t => t.perkLvl < perkLvl))
                return true; 
            else return false;  
        }
        #endregion

        #region  SKILL SELECTION AI 
        public void StartAISkillInController()
        {

            SkillModel selectedSkillModel = SkillSelectByAI();
            if (selectedSkillModel != null)
            {
                SkillService.Instance.currSkillName = selectedSkillModel.skillName;
                //SkillService.Instance.On_SkillSelected(selectedSkillModel.charName);// dependencies skillName
                // fixes skill select call  and currSKill controller to skill Service


                allSkillBases.Find(t => t.skillName == selectedSkillModel.skillName).SkillSelected();
                // SkillSelect?.Invoke(selectedSkillModel.skillName);  // message broadcaster 

                Debug.Log("SELECTED SKILLS: " + selectedSkillModel.skillName +"Enemy Skillbases: " + allSkillBases.Count);                 

                allSkillBases.Find(t => t.skillName == selectedSkillModel.skillName).PopulateAITarget();
                // Set the target ..i.e currTargetDyna .. etc 
                Debug.Log("TARGETS" + SkillService.Instance.currentTargetDyna.charGO.name);
                SkillService.Instance.OnAITargetSelected(selectedSkillModel);
            }
            else
            {
                  SkillService.Instance.On_PostSkill(selectedSkillModel); // model is null here             
            }
        }
        public SkillModel SkillSelectByAI()
        {
            float netBaseWt = 0f; ClickableSkills.Clear();
            foreach (SkillModel skillModel in allSkillModels)
            {   
                UpdateSkillState(skillModel);
                if (skillModel.GetSkillState() == SkillSelectState.Clickable)
                {
                    Debug.Log("SKILL MODEL" + skillModel.skillName);
                    netBaseWt += skillModel.baseWeight;
                    ClickableSkills.Add(skillModel);
                }
            }
            if (ClickableSkills.Count < 1) return null;
            //int index = UnityEngine.Random.Range(0, ClickableSkills.Count);
            for (int i = 0; i < ClickableSkills.Count; i++)
            {
                if (GetSkillModelByBaseWtChance(netBaseWt, i))
                    return ClickableSkills[i];
            }
            int random = UnityEngine.Random.Range(0, ClickableSkills.Count);
            return ClickableSkills[random];// to remove error 
        }
        bool GetSkillModelByBaseWtChance(float NetWt, int i)
        {
            float skillchance = (ClickableSkills[i].baseWeight / NetWt) * 100f;

            return skillchance.GetChance();
        }

        #endregion

        #region SKILL MOD BUFF
        
        public int ApplySkillDmgModBuff(CauseType causeType, int causeName,  SkillInclination skillInclination
                                                                    , float dmgVal, TimeFrame timeFrame, int castTime)
        {
            int skillModId = allSkillDmgMod.Count + 1;
            int currRd = CombatEventService.Instance.currentRound;
            SkillDmgModData skillBuffModVal = new SkillDmgModData(skillModId, causeType, causeName, skillInclination
                                                                                    , dmgVal, timeFrame, castTime, currRd);
            foreach (SkillModel skillModel in allSkillModels)
            {
                if(skillModel.skillInclination == skillInclination)
                        skillModel.damageMod += dmgVal; 
            }
            allSkillDmgMod.Add(skillBuffModVal);
            return skillModId;
        }
        public void RemoveSkillDmgModBuff(int skillModId)
        {
            int index = allSkillDmgMod.FindIndex(t=>t.skillModID== skillModId);
            if (index != -1)
            {
                foreach (SkillModel skillModel in allSkillModels)
                {
                    if (skillModel.skillInclination == allSkillDmgMod[index].skillInclination)
                            skillModel.damageMod -= allSkillDmgMod[index].dmgVal;
                }
                allSkillDmgMod.RemoveAt(index);
            }
        }

        public void RoundTick(int roundNo)
        {
            foreach (SkillDmgModData skillDmgModData in allSkillDmgMod.ToList())
            {
                if (skillDmgModData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (skillDmgModData.currentTime >= skillDmgModData.castTime)
                    {
                        RemoveSkillDmgModBuff(skillDmgModData.skillModID);
                    }
                    skillDmgModData.currentTime++;
                }
            }
        }
        public void EOCTick()
        {
            foreach (SkillDmgModData buffData in allSkillDmgMod.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveSkillDmgModBuff(buffData.skillModID);
                }
            }
            ResetNoOfTimeUsedOnEOC();
        }
        public void EOQTick()
        {
            foreach (SkillDmgModData skillModBuffData in allSkillDmgMod.ToList())
            {
                if (skillModBuffData.timeFrame == TimeFrame.EndOfQuest)
                {
                    RemoveSkillDmgModBuff(skillModBuffData.skillModID);
                }
            }
        }

        void ResetNoOfTimeUsedOnEOC()
        {
            foreach (SkillModel skillModel in allSkillModels)
            {
                skillModel.noOfTimesUsed = 0; 
            }
        }

        #endregion

        #region Passive Skill Controllers
        // init on Combat
        public void InitPassiveSkillController()
        {
            if (charController.charModel.orgCharMode != CharMode.Enemy) return;
           
            passiveSkillcontroller = GetComponent<PassiveSkillsController>();
            if (passiveSkillcontroller == null)
                passiveSkillcontroller= gameObject.AddComponent<PassiveSkillsController>();

            passiveSkillcontroller.InitPassive(charController);
        }
        #endregion

        #region HELPERS

        public void UnClickableSkillsByIncli(SkillInclination skillInclination)
        {
            foreach (SkillBase skillbase in allSkillBases)
            {
                if(skillbase.skillModel.skillInclination == skillInclination)
                {
                    skillbase.skillModel.SetSkillState(SkillSelectState.UnClickable_Misc); 
                }
            }
        }
        public void UnClickableSkillsByAttackType(AttackType attackType, int pos)
        {
            foreach (SkillBase skillbase in allSkillBases)
            {
                if (skillbase.skillModel.attackType == attackType)
                {
                    if(skillbase.skillModel.castPos.Any(t => t != pos))
                        skillbase.skillModel.SetSkillState(SkillSelectState.UnClickable_Misc);
                    
                }
            }
        }
        #endregion

        #region UPDATE SKILL STATE

        public void UpdateAllSkillState()
        {
            if (GameService.Instance.gameModel.gameState != GameState.InCombat) return;
            if (!CharService.Instance.allCharInCombat.Any(t => t.charModel.charID == charController.charModel.charID)) return;
            Debug.Log(" CHAR SKILL UPDATE" + charController.charModel.charName);
            foreach (SkillModel skillModel in allSkillModels)
            {
                UpdateSkillState(skillModel);
            }
        }
        public SkillSelectState UpdateSkillState(SkillModel skillModel)
        {

            //  skillModel.SetSkillState(SkillSelectState.Clickable);
            SkillSelectState skillState = SkillSelectState.None;
            bool isRooted = charController.charStateController.HasCharState(CharStateName.Rooted); 

            if (CombatService.Instance.combatState == CombatState.INTactics)
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_InTactics);
                skillState = SkillSelectState.UnClickable_InTactics;
            }
            else if (CombatService.Instance.currCharClicked != CombatService.Instance.currCharOnTurn)
            {
                skillModel.SetSkillState(SkillSelectState.Unclickable_notCharsTurn);
                skillState = SkillSelectState.Unclickable_notCharsTurn;
            }
            else if (IfInCoolDown(skillModel))      // only char on turn will get here 
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_InCd);
                skillState = SkillSelectState.UnClickable_InCd;
            }
            else if (IfNoUseLeft(skillModel))      // only char on turn will get here 
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_NoUseLeft);
                skillState = SkillSelectState.UnClickable_NoUseLeft;
            }
            else if (HasNoChkActionPts())
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_NoActionPts);
                skillState = SkillSelectState.UnClickable_NoActionPts;
            }
            else if(skillModel.skillInclination == SkillInclination.Move && isRooted)
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_2Move);
                skillState = SkillSelectState.UnClickable_2Move;
            }
           
            else if (IsNotOnCastPos(skillModel))     // not on cast pos 
            {
                skillModel.SetSkillState(SkillSelectState.Unclickable_notOnCastPos);
                skillState = SkillSelectState.Unclickable_notOnCastPos;
            }
        
            else if (NoTargetsInRange(skillModel))
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_NoTargets);
                skillState = SkillSelectState.UnClickable_NoTargets;
            }
            else if (HasNoStamina(skillModel))
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_NoStamina);
                skillState = SkillSelectState.UnClickable_NoStamina;
            }
            else if (skillModel.skillInclination == SkillInclination.Passive)  // as enemies only // more like traits
            {
                skillModel.SetSkillState(SkillSelectState.Unclickable_passiveSkills);
                skillState = SkillSelectState.Unclickable_passiveSkills;
            }
            else
            {
                skillModel.SetSkillState(SkillSelectState.Clickable);
                skillState = SkillSelectState.Clickable;
            }
            skillModel.prevSkillSelState =skillState; //prev state set to updated state
            Debug.Log("SKILL NAME " + skillModel.skillName + "State" + skillState +
         "TARGETS" + skillModel.targetPos.Count);
            return skillState; 
        }

        bool HasNoChkActionPts()
        {
            
            CharController charController = CombatService.Instance.currCharOnTurn;
            //if (charController.charModel.charMode == CharMode.Enemy)
            //    return false; 
            CombatController combatController = charController?.GetComponent<CombatController>();
            if (combatController == null)
                return false; // case: Combat controller is null in tactics and therefore
            Debug.Log("Checked on action pts" + combatController.actionPts + " charName"+ charController.gameObject.name);
            if (combatController.actionPts > 0)
                return false;
            
            return true;
        }

        public bool HasNoStamina(SkillModel _skillModel)
        {
            StatData staminaData = CharService.Instance.GetCharCtrlWithCharID(_skillModel.charID).GetStat(StatName.stamina);
            float stamina = staminaData.currValue;

            if (stamina < _skillModel.staminaReq)
            {
                return true;
            }
            return false;
        }

        bool NoTargetsInRange(SkillModel _skillModel)
        {

            if (_skillModel.skillType == SkillTypeCombat.Move || _skillModel.attackType == AttackType.Remote)
            {
                // Checks only target Pos as skill is used on empty tile 
                if (_skillModel.targetPos.Count != 0) return false;
                else return true;

            }
            else
            {  // get dyna from target pos
                if (SkillService.Instance.GetTargetInRange(_skillModel) == null)
                {
                    // Debug.Log("return null targets due to no DYNA");     
                    return true;
                }
                if (SkillService.Instance.GetTargetInRange(_skillModel).Count == 0)
                {
                    /// Debug.Log("return ZERO targets due to no DYNA");
                    return true;
                }
                else
                {
                    // Debug.Log("return HAS targets");
                    return false;
                }
            }
        }

        bool IsNotOnCastPos(SkillModel _skillModel)
        {
            GameObject charGO = CharService.Instance.GetCharCtrlWithCharID(_skillModel.charID).gameObject;
            Debug.Log("charName"+ _skillModel.charName + _skillModel.charID);    
            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            // Debug.Log("Position in" + pos);

            return !(_skillModel.castPos.Any(t => t == pos));
        }
        bool IfNoUseLeft(SkillModel _skillModel)
        {
            if (_skillModel.cd >= 0) return false; // governed by cd not use 
            if (_skillModel.maxUsagePerCombat - _skillModel.noOfTimesUsed <= 0)
            {
                return true; 
            }
            return false; 
        }

        bool IfInCoolDown(SkillModel _skillModel)
        {
            if (_skillModel.cd == -5) return false;
            if(_skillModel.cd == 0) return false; // 0 cd case
            if (_skillModel.lastUsedInRound == -5) return false;
            _skillModel.cdRemaining = _skillModel.cd - (CombatEventService.Instance.currentRound - _skillModel.lastUsedInRound);
            if (_skillModel.cdRemaining > 0)
            {               
                return true;
            }
            else
            {
                _skillModel.cdRemaining = 0; 
                _skillModel.lastUsedInRound= 0;
                return false;
            }
        }

        #endregion

        #region EOC RESET

        void OnEOCReset()
        {
            foreach (SkillModel skillModel  in allSkillModels)
            {
                skillModel.lastUsedInRound = 0; 
            }
        }
        #endregion

    }

    public class SkillDmgModData
    {
        public int skillModID; 
        public CauseType causeType;
        public int causeName;
        public SkillInclination skillInclination;
        public float dmgVal;
        public TimeFrame timeFrame;
        public int castTime;
        public int currentTime; 

        public SkillDmgModData(int skillModID, CauseType causeType, int causeName, SkillInclination skillInclination, float dmgVal
                                    , TimeFrame timeFrame, int castTime, int currentTime)
        {
            this.skillModID = skillModID;
            this.causeType = causeType;
            this.causeName = causeName;
            this.skillInclination = skillInclination;
            this.dmgVal = dmgVal;
            this.timeFrame = timeFrame;
            this.castTime = castTime;
            this.currentTime = currentTime; 
        }
    }
}