using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<PerkModelData> allSkillPerkData = new List<PerkModelData>(); // contains perkType

        [Header("All Skill and UnLocked Skill list")]
        public List<SkillNames> allSkillInChar = new List<SkillNames>();
        public List<SkillNames> unLockedSkills = new List<SkillNames>();

        [Header("Skill Model")]
        public List<SkillModel> allSkillModels = new List<SkillModel>();
        public List<SkillModel> ClickableSkills = new List<SkillModel>();

        [Header("Skill and Perk Bases")]
        public List<SkillBase> allSkillBases = new List<SkillBase>();
        public List<PerkBase> allPerkBases = new List<PerkBase>();

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
        }
        public void InitPerkDataList()
        {
            foreach (SkillNames _skillName in unLockedSkills)
            {
                List<PerkModelData> skillPerkData =   
                         SkillService.Instance.skillFactory.GetSkillPerkData(_skillName);
                
                foreach (PerkModelData perkData in skillPerkData)
                {

                    PerkBase P1 = SkillService.Instance.skillFactory
                                .GetPerkBase(perkData.skillName, perkData.perkName); 
                    allPerkBases.Add(P1);
                    P1.SkillInit(this);
                }
                allSkillPerkData.AddRange(skillPerkData);
            }

        }

        #region On_Hover, On_SkillSelect and Check coolDown

        public void SkillHovered(SkillNames _skillName)
        {
            SkillServiceView.Instance.skillCardData.perkChain.Clear();
            SkillServiceView.Instance.skillCardData.descLines.Clear();

            allSkillBases.Find(t => t.skillName == _skillName).SkillHovered();

            List<PerkModelData> clickedPerkList = allSkillPerkData
                .Where(t => t.skillName == _skillName && t.state == PerkSelectState.Clicked).ToList();

            clickedPerkList.ForEach(t => SkillServiceView.Instance.skillCardData.perkChain.Add(t.perkType));

            clickedPerkList.ForEach(t => allPerkBases.Find(x => x.perkName == t.perkName).SkillInit(this));
            clickedPerkList.ForEach(t => allPerkBases.Find(x => x.perkName == t.perkName).SkillHovered());
        }

        public void SkillSelect(SkillNames _skillName)
        {
            allSkillBases.Find(t => t.skillName == _skillName).SkillSelected();

            List<PerkModelData> clickedPerkList = allSkillPerkData
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

        public void OnPerkUnlock(PerkNames _perkName)
        {
            if (currSkillPts > 0)
                currSkillPts--;
            else return;
            Debug.Log("inside perk unlocked" + btn.name);
            PerkModelData snp = allSkillPerkData.Find(t => t.perkName == _perkName);
          //  CharNames charName = GetChar4Skill(snp.skillName);
            //allSkillControllers.Find(t => t.charName == charName).UpdatePerkState(_perkName, PerkSelectState.Clicked);

            snp.state = PerkSelectState.Clicked;

            UIControlServiceCombat.Instance.TogglePerkSelectState(UIElementType.PerkButton,
                                                        btn, PerkSelectState.Clicked);
            // update skill pts and UI 
            // perk view Controller
            //perkSelectionController.UpdateSkillPts();
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