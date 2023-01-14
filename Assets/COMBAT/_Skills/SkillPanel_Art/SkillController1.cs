using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using UnityEngine;


namespace Common
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
        public List<SkillNames> allSkillInChar = new List<SkillNames>();
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
        private void Start()
        {
            charController = gameObject.GetComponent<CharController>();
            charName = charController.charModel.charName;

            CharService.Instance.OnCharAddedToParty += InitSkillList;
        }
        public void InitSkillList(CharNames _charName)
        {
            if (charName != _charName) return;
            // stop double run
            if (allSkillInChar.Count > 0) return;    
            skillDataSO = SkillService.Instance.GetSkillSO(charName);
            foreach (SkillData skill in skillDataSO.allSkills)
            {
                allSkillInChar.Add(skill.skillName);

                if (skill.skillUnLockStatus == 1) // 1 = unlock, 0 locked, -1 NA
                {
                    unLockedSkills.Add(skill.skillName);
                }
            }
            foreach (var skillSO in skillDataSO.allSkills)
            {
                SkillBase skillbase = SkillService.Instance.skillFactory.GetSkill(skillSO.skillName);

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
            SkillService.Instance.skillCardData.perkChain.Clear();
            SkillService.Instance.skillCardData.descLines.Clear();

            allSkillBases.Find(t => t.skillName == _skillName).SkillHovered();

            List<PerkData> clickedPerkList = allSkillPerkData
                .Where(t => t.skillName == _skillName && t.state == PerkSelectState.Clicked).ToList();

            clickedPerkList.ForEach(t => SkillService.Instance.skillCardData.perkChain.Add(t.perkType));

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

        public void CheckNUpdateSkillState()
        {

            //cd .. 0 cast this even this round double strike
            //cd... 1 cast this only next round .... 
            //cd .. so on and so forth

            int cdGap = 0;
            foreach (var skill in allSkillBases)
            {
                cdGap = CombatService.Instance.currentRound - skill.skillModel.lastUsedInRound;
                //if (cdGap < 0)
                //{
                //  //  Debug.Log("Cool Calc Error");
                //}
                if (cdGap < skill.skillModel.cd)
                {
                    skill.skillModel.SetSkillState(SkillSelectState.UnClickable_InCd);
                    // Debug.Log("SkillUsed" + cdGap);
                }
                else
                {
                    //SkillServiceView.Instance.UpdateSkillIconTxt(skill.skillName, cdGap);
                    skill.skillModel.SetSkillState(SkillSelectState.Clickable);

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
           // List<PerkData> list = GetClickedPerks(perkData.skillName);
            foreach (PerkData perk in allSkillPerkData)
            {
                if(perk.perkName != perkData.perkName)
                {
                    if(perk.perkLvl == perkData.perkLvl)
                    {
                        UpdateDataPerkState(perk.perkName, PerkSelectState.UnClickable);
                    }
                }
            }
        }
        void SetPerkState(PerkData clickedPerkData) //  on perk clicked extn
        {
            if (clickedPerkData.state == PerkSelectState.Clicked)
            {
                SetSameLvlPerkUnClickable(clickedPerkData);
            }
            
            foreach (PerkData perk in allSkillPerkData)
            {
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

                        }
                    }
                }
            }
            foreach (PerkData perk in allSkillPerkData)
            {
                SkillLvl nextlvl = clickedPerkData.perkLvl + 2;
                if ((int)nextlvl > 3) continue;
                if (perk.perkLvl == nextlvl)
                {
                    foreach (PerkNames perkName in perk.preReqList)
                    {
                        if (perkName == PerkNames.None 
                            || GetPerkData(perkName).state == PerkSelectState.Clickable
                            || GetPerkData(perkName).state == PerkSelectState.Clicked)
                        {
                            UpdateDataPerkState(perk.perkName, PerkSelectState.Clickable);//update
                        }
                        else
                        {
                            UpdateDataPerkState(perk.perkName, PerkSelectState.UnClickable);

                        }
                    }
                }
            }
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
        bool IsPrevLvlClicked(PerkData perkData)
        {

            SkillLvl perkLvl = perkData.perkLvl;
            if (perkLvl == SkillLvl.Level1) return true; 
            List<PerkData> clickedPerks = GetClickedPerks(perkData.skillName);
            if (clickedPerks.Any(t => t.perkLvl < perkLvl))
                return true; 
            else return false;  
        }
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

                Debug.Log("SELECTED SKILLS" + selectedSkillModel.skillName);

                allSkillBases.Find(t => t.skillName == selectedSkillModel.skillName).PopulateAITarget();
                // Set the target ..i.e currTargetDyna .. etc 
                Debug.Log("TARGETS" + SkillService.Instance.currentTargetDyna.charGO.name);
                SkillService.Instance.OnAITargetSelected(selectedSkillModel);
            }
            else
            {
                SkillService.Instance.On_PostSkill();
            }
        }
        public SkillModel SkillSelectByAI()
        {
            float netBaseWt = 0f; ClickableSkills.Clear();
            foreach (SkillNames skillName in unLockedSkills)
            {
                SkillModel skillModel = allSkillModels.Find(t => t.skillName == skillName);
                skillModel.SetSkillState(SkillServiceView.Instance.UpdateSkillState(skillModel));
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

    }
}