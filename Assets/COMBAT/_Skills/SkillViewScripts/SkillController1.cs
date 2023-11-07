
using Common;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
            charName = charController.charModel.charName;
            
            CharService.Instance.OnCharInit += InitSkillList;
            CombatEventService.Instance.OnSOC1 += InitAllSkill_OnCombat;
            Debug.Log("ENABLED" + charName);
            // CharService.Instance.OnCharAddedToParty += InitSkillList;
            SceneManager.sceneLoaded += OnSceneLoaded;
            QuestEventService.Instance.OnEOQ += EOQTick;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            CharService.Instance.OnCharInit -= InitSkillList;
            CombatEventService.Instance.OnSOC1 -= InitAllSkill_OnCombat;
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
                CombatEventService.Instance.OnSOC1 += InitAllSkill_OnCombat;
                CombatEventService.Instance.OnEOR1 += RoundTick;
                CombatEventService.Instance.OnEOC += EOCTick;
            
            }
            
        }


        public void InitAllSkill_OnCombat(CombatState combatState)
        {
            CharMode charMode = charController.charModel.charMode;
            Debug.Log("COMBAT STATE" + charController.charModel.charID);
            if (charMode == CharMode.Enemy)
            {
                InitSkillList(charController);
            }
        
            foreach (SkillBase skillBase in allSkillBases)
            {
                skillBase.SkillInit(this); 
            }
        }
        public void InitSkillList(CharController charController)
        {
            if (this.charController.charModel.charID != charController.charModel.charID) return;
            // stop double run
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
            SkillService.Instance.skillModelHovered.descLines.Clear();
            Debug.Log(" all Skill bases count" + allSkillBases.Count +
                            "all perk bases count" + allPerkBases +" SKillname" + _skillName); 

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
                       // UpdatePerkRel(perk);
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
                                                                                          //
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

        public void UpdatePipeRel()
        {
            // go reverse // sort each perk using linq into separate lvl 
            // mark status as 1,2,3 ... handle display in  perk btn ptr events 

            List<PerkData> allHachet = allSkillPerkData.Where(t => t.skillName == SkillNames.HatchetSwing).ToList();
            foreach (PerkData perk1 in allHachet)
            {               
                if (perk1.perkLvl == SkillLvl.Level3) continue; 
                foreach (PerkData perk2 in allHachet)
                {
                   // if(perk2.perkLvl == perk1.perkLvl) continue;
                    if(perk2.perkLvl == perk1.perkLvl + 1)
                    {
                        switch (perk2.state)
                        {
                            case PerkSelectState.Clickable:
                                if (perk2.perkType == PerkType.A2)
                                    perk1.pipeRel[0] = 1;
                                if (perk2.perkType == PerkType.B2)
                                    perk1.pipeRel[1] = 1;
                                break;
                            case PerkSelectState.Clicked:
                                if (perk2.perkType == PerkType.A2)
                                    perk1.pipeRel[0] = 2;
                                if (perk2.perkType == PerkType.B2)
                                    perk1.pipeRel[1] = 2;
                                break;
                            case PerkSelectState.UnClickable:
                                if (perk2.perkType == PerkType.A2)
                                    perk1.pipeRel[0] = 3;
                                if (perk2.perkType == PerkType.B2)
                                    perk1.pipeRel[1] = 3;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        public void UpdatePerkRel(PerkData clickedPerkData)
        {
            SkillNames skillName = clickedPerkData.skillName;
            foreach (PerkData perk in allSkillPerkData)
            {
                if (perk.skillName != skillName) continue;
               
                for(int i = 1; i <= 2; i++)
                {
                    SkillLvl nextlvl = clickedPerkData.perkLvl + i;
                    if ((int)nextlvl > 3) continue;
                    //NEXT LVL
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

                Debug.Log("SELECTED SKILLS" + selectedSkillModel.skillName +"Enemy Skillbases" + allSkillBases.Count);                 

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
                //skillModel.SetSkillState(skillView.UpdateSkillState(skillModel));
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
        
        public int ApplySkillDmgModBuff(CauseType causeType, int causeName,  SkillInclination skillInclination, float dmgVal, TimeFrame timeFrame, int castTime)
        {
            int skillModId = allSkillDmgMod.Count + 1;
            int currRd = GameSupportService.Instance.currentRound;
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